using System;
using _Main.GamePlay.TileSystem;
using _Main.Patterns.ModuleSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    public class Person : BaseComponent, ITileObject
    {
        #region SerializeField Variables

        [SerializeField] private PersonModelController _modelController;

        #endregion


        #region Encapsulated Variables

        public Tile Tile { get; set; }

        public PersonData Data { get; private set; }

        #endregion


        #region Unity Event Methods

        private void OnDisable()
        {
            Reset();
        }

        #endregion

        #region Init-Reset Methods

        public void Initialize(PersonData data)
        {
            Data = data;
            _modelController.Initialize();


            SetColor();
        }

        public void Reset()
        {
            _modelController.Reset();
        }

        #endregion

        #region Set Methods

        public void SetTile(Tile tile)
        {
            Tile = tile;
            Tile.TileObject = this;
        }

        private void SetColor()
        {
            _modelController.SetColor(Data.Color);
        }

        #endregion


        #region Implemented Methods

        public void ClickAction()
        {
            Debug.Log("I have been clicked!");
        }

        #endregion
    }
}