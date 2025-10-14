using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.GamePlay.GridSystem;
using _Main.Scripts.GamePlay.Helpers;
using _Main.Scripts.GamePlay.LevelSystem.Data;
using _Main.Scripts.GamePlay.LevelSystem.Helpers;
using _Main.Scripts.GamePlay.SaveSystem;
using _Main.Scripts.GamePlay.TileSystem.Components;
using _Main.Scripts.GamePlay.Utilities;
using _Main.Scripts.Patterns.ObjectPooling;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.Scripts.GamePlay.TileSystem.Manager
{
    public class TileManager : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private Transform _gridParent;

        #endregion

        #region Private Variables

        private Grid<Tile> _grid;
        private Tile[,] _tilesMatrix;
        private int _width;
        private int _height;
        private List<Tile> _tiles;

        #endregion

        #region Create-Release

        public void CreateTiles(LevelData levelData)
        {
            _width = levelData.TileWidth;
            _height = levelData.TileHeight;
            _tiles = new List<Tile>(_width * _height);
            _tilesMatrix = new Tile[_height, _width];
            _grid = new Grid<Tile>(_height, _width, GameConfig.TILE_SIZE, _gridParent.position);
            for (int i = 0; i < _height; i++)
            {
                var isExitTile = i == 0;
                for (int j = 0; j < _width; j++)
                {
                    var tileData = levelData.GetTile(i, j);
                    Vector3 pos = _grid.GetWorldPosition(i, j);
                    var tile = ObjectPooler.Instance.SpawnSc<Tile>(Keys.PoolTags.TILE, pos, Quaternion.identity,
                        _gridParent);
                    var index = _tiles.Count;
                    tile.Initialize(tileData, i, j, isExitTile,index);
                    tile.SetIndexes(i, j);
                    _tiles.Add(tile);
                    _tilesMatrix[i, j] = tile;
                }
            }
        }

        public void ReleaseTiles()
        {
            foreach (var tile in _tiles)
            {
                tile.Reset();
                ObjectPooler.Instance.ReleasePooledObject(Keys.PoolTags.TILE, tile);
            }

            _tiles.Clear();
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

        public List<Tile> GetTiles()
        {
            return _tiles;
        }

        #endregion

        [Button(ButtonSizes.Gigantic)]
        public void StartLevel()
        {
            Timing.RunCoroutine(StartLevelFromSaveData());
        }

        public IEnumerator<float> StartLevelFromSaveData()
        {
            SaveManager.LoadData();
            foreach (var tileIndex in SaveManager.TileIndexes)
            {
                Debug.Log("Tıle index" +tileIndex);
                _tiles[tileIndex].InputController.ExecuteWithObjectManager(false);
                yield return Timing.WaitForOneFrame;
            }
            SaveManager.ClearList();
        }
    }
}