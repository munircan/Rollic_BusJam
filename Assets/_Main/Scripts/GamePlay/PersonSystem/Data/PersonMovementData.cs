using System;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    [CreateAssetMenu(fileName = "PersonMovementData", menuName = "PersonData/MovementData", order = 0)]
    public class PersonMovementData : ScriptableObject
    {
        public PersonPathMovementData PathMovementData;
    }

    [Serializable]
    public struct PersonPathMovementData
    {
        public float Duration;
        public PathType PathType;
        public PathMode PathMode;
        public Ease Ease;
    }
    
    
}