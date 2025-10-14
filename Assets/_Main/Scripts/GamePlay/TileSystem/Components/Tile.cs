using _Main.GamePlay.TileSystem;
using _Main.Scripts.GamePlay.CustomEvents.InGameEvents;
using _Main.Scripts.GamePlay.GridSystem;
using _Main.Scripts.GamePlay.TileSystem.Data;
using _Main.Scripts.Patterns.EventSystem;
using _Main.Scripts.Patterns.ModuleSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.TileSystem.Components
{
    public class Tile : BaseComponent, IGridObject
    {
        #region SerializeField Variables

        [SerializeField] private TileModelController _modelController;
        [SerializeField] private TileInputController _inputController;

        #endregion

        #region Encapsulated Variables

        public bool IsOccupied
        {
            get { return Data.Type == TileType.None || Data.Type == TileType.Obstacle || TileObject != null; }
        }

        public ITileObject TileObject { get; set; }
        public TileData Data { get; set; }


        // TODO: WE SHOULD SELECT FROM LEVEL DATA IF WE WANT
        public bool IsExitTile { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        #endregion

        #region Init-Reset Methods

        public void Initialize(TileData data, int x, int y, bool isExitTile)
        {
            Data = data;
            SetIndexes(x, y);
            IsExitTile = isExitTile;

            _modelController.Initialize();
            _inputController.Initialize();
        }

        public void SetIndexes(int x, int y)
        {
            X = x;
            Y = y;
        }


        public void Reset()
        {
            TileObject = null;
            _modelController.Reset();
            _inputController.Reset();
        }

        #endregion

        #region Setter Methods

        public void SetTileObject(ITileObject tileObject, bool initialValue = false)
        {
            TileObject = tileObject;
            if (!initialValue)
            {
                EventManager.Publish(EventTileChanged.Create(this));
            }
        }

        #endregion

        #region Get Methods

        public Vector3 GetPersonPosition()
        {
            return _modelController.PersonPosition;
        }

        #endregion
    }
}