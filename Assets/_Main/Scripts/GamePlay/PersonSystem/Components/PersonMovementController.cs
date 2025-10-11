using System.Collections.Generic;
using _Main.Patterns.ModuleSystem;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    public class PersonMovementController : ComponentModule<Person>
    {
        [SerializeField] private PersonMovementData _movementData;
        private Tween pathTween;

        public void MovePath(List<Vector3> path)
        {
            var pathArray = path.ToArray();
            pathTween = transform.DOPath(pathArray, _movementData.PathMovementData.Duration, _movementData.PathMovementData.PathType, _movementData.PathMovementData.PathMode)
                .SetEase(_movementData.PathMovementData.Ease)
                .SetLink(gameObject);
                // .SetLookAt(0.01f);
              
        }

        public void KillPath()
        {
            pathTween.Kill();
        }


        internal override void Reset()
        {
            base.Reset();
            KillPath();
        }
    }
}