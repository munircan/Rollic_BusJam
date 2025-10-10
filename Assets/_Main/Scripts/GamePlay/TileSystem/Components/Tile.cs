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


        // TODO: WE SHOULD SELECT FROM LEVEL DATA IF WE WANT
        public bool IsExitTile { get; set; }

        public int X { get; set; }
        public int Y { get; set; }


        #region Encapsulated Variables

        public bool IsOccupied
        {
            get
            {
                return Data.Type == TileType.None || TileObject != null;
            }
        }

        public ITileObject TileObject { get; set; }
        public TileData Data { get; set; }

        #endregion

        #region Unity Event Methods

        private void OnDisable()
        {
            Reset();
        }

        #endregion

        #region Init-Reset Methods

        public void Initialize(TileData data,int x,int y, bool isExitTile)
        {
            Data = data;
            SetIndexes(x,y);
            IsExitTile = isExitTile;
            
            _modelController.Initialize();


            _modelController.SetModelsActive(Data.Type);
        }

        public void SetIndexes(int x, int y)
        {
            X = x;
            Y = y;
        }


        public void Reset()
        {
            _modelController.Reset();
        }

        #endregion

        #region Helpers

        public Vector3 GetPersonPosition()
        {
            return _modelController.PersonPosition;
        }

        #endregion
    }
}