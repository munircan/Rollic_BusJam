using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.GamePlay.GridSystem;
using _Main.Scripts.GamePlay.Helpers;
using _Main.Scripts.GamePlay.LevelSystem.Data;
using _Main.Scripts.GamePlay.LevelSystem.Helpers;
using _Main.Scripts.GamePlay.TileSystem.Components;
using _Main.Scripts.GamePlay.Utilities;
using _Main.Scripts.Patterns.ObjectPooling;
using UnityEngine;

namespace _Main.Scripts.GamePlay.TileSystem.Manager
{
    public class TileManager : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private Transform _gridParent;

        #endregion

        #region Encapsulation

        public List<Tile> Tiles { get; private set; }

        #endregion

        #region Private Variables

        private Grid<Tile> _grid;
        private Tile[,] _tilesMatrix;
        private int _width;
        private int _height;

        private const float CELL_SIZE = 1.25f;

        #endregion

        #region Create-Release

        public void CreateTiles(LevelData levelData)
        {
            _width = levelData.TileWidth;
            _height = levelData.TileHeight;
            Tiles = new List<Tile>(_width * _height);
            _tilesMatrix = new Tile[_height, _width];
            _grid = new Grid<Tile>(_height, _width, CELL_SIZE, _gridParent.position);
            for (int i = 0; i < _height; i++)
            {
                var isExitTile = i == 0;
                for (int j = 0; j < _width; j++)
                {
                    var tileData = levelData.GetTile(i, j);
                    Vector3 pos = _grid.GetWorldPosition(i, j);
                    var tile = ObjectPooler.Instance.SpawnSc<Tile>(Keys.PoolTags.TILE, pos, Quaternion.identity,
                        _gridParent);
                    tile.Initialize(tileData, i, j, isExitTile);
                    tile.SetIndexes(i, j);
                    Tiles.Add(tile);
                    _tilesMatrix[i, j] = tile;
                }
            }
        }

        public void ReleaseTiles()
        {
            foreach (var tile in Tiles)
            {
                tile.Reset();
                ObjectPooler.Instance.ReleasePooledObject(Keys.PoolTags.TILE, tile);
            }

            Tiles.Clear();
        }

        #endregion

        #region Path Methods

        public bool GetPath(Tile tile, out List<Vector3> path)
        {
            var tilePath = PathFinding.PathFinding.FindPathToClosestExit(tile, _tilesMatrix, _height, _width);

            path = new List<Vector3>();
            if (tilePath != null)
            {
                path = tilePath.GetPathAsVector3List().ToList();
                return true;
            }

            return false;
        }

        #endregion
    }
}