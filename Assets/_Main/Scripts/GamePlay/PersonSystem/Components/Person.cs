using System;
using System.Collections.Generic;
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

        public bool CanWalk { get; set; }

        #endregion

        #region Private Variables

        private List<Tile> _path;

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
            Tile.SetTileObject(this, true);
        }

        private void SetColor()
        {
            _modelController.SetColor(Data.Color);
        }

        public void SetCanWalk(bool canWalk)
        {
            CanWalk = canWalk;
        }

        public void SetPath(List<Tile> path)
        {
            if (path == null)
            {
                return;
            }
            _path = new List<Tile>(path);
        }

        #endregion


        #region Implemented Methods

        public void Action()
        {
            Debug.Log("I have to MOVE!");
        }

        #endregion
    }
}