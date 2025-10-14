using System;
using _Main.Scripts.GamePlay.TileSystem.Components;
using _Main.Scripts.GamePlay.TileSystem.Data;
using _Main.Scripts.Patterns.ModuleSystem;
using UnityEngine;

namespace _Main.GamePlay.TileSystem
{
    public class TileModelController : ComponentModule<Tile>
    {
        #region SerializeField Variables

        [SerializeField] private Transform _personTransform;

        // REMEMBER TO CLOSE MODELS IN PREFAB
        [SerializeField] private GameObject _tileModel;
        [SerializeField] private GameObject _obstacleModel;


        #endregion

        #region Encapsulated Variables
    
        public Vector3 PersonPosition => _personTransform.position;

        #endregion
        

        #region Override Methods

        internal override void Initialize()
        {
            base.Initialize();
            SetModelsActive(BaseComp.Data.Type);
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