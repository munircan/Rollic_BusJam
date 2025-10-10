using _Main.GamePlay.GridSystem;
using _Main.Patterns.ModuleSystem;
using UnityEngine;

namespace _Main.GamePlay.TileSystem
{
    public class Tile : BaseComponent, IGridObject
    {
        #region SerializeField Variables

        [SerializeField] private TileModelController _modelController;

        #endregion

        public bool IsOccupied { get; set; }

        public ITileObject TileObject { get; set; }
        public TileData Data { get; set; }


        #region Unity Event Methods

        private void OnDisable()
        {
            Reset();
        }

        #endregion

        #region Init-Reset Methods

        public void Initialize(TileData data)
        {
            Data = data;
            _modelController.Initialize();


            _modelController.SetModelsActive(Data.Type);
        }


        public void Reset()
        {
            _modelController.Reset();
        }

        #endregion
    }
}