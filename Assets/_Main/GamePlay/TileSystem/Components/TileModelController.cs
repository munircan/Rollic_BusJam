using System;
using _Main.Patterns.ModuleSystem;
using UnityEngine;

namespace _Main.GamePlay.TileSystem
{
    public class TileModelController : ComponentModule<Tile>
    {
        [SerializeField] private Transform _personTransform;

        // REMEMBER TO CLOSE MODELS IN PREFAB
        [SerializeField] private GameObject _tileModel;
        [SerializeField] private GameObject _obstacleModel;
        

        #region Override Methods

        internal override void Initialize()
        {
            base.Initialize();
            SetModelsActive(TileType.None);
        }
        
        internal override void Reset()
        {
            base.Reset();
            SetModelsActive(TileType.None);
        }

        #endregion


        public void SetModelsActive(TileType type)
        {
            switch (type)
            {
                case TileType.None:
                    _tileModel.SetActive(false);
                    _obstacleModel.SetActive(false);
                    break;
                case TileType.Default:
                    _tileModel.SetActive(true);
                    break;
                case TileType.Person:
                    _tileModel.SetActive(true);
                    break;
                case TileType.Obstacle:
                    _obstacleModel.SetActive(true);
                    break;
            }
        }

       
    }
}