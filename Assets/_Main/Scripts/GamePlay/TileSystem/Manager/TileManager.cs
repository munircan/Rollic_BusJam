using System.Collections.Generic;
using _Main.GamePlay.GridSystem;
using _Main.Patterns.ObjectPooling;
using _Main.Scripts.GamePlay.LevelSystem;
using _Main.Scripts.GamePlay.PathFinding;
using _Main.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.GamePlay.TileSystem.Manager
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private Transform _gridParent;
        public List<Tile> Tiles { get; private set; }

        private Grid<Tile> _grid;
        private const float CELL_SIZE = 1.25f;
        private int _width;
        private int _height;

        public Tile[,] TilesMatrix;

   
        public void CreateTiles(LevelData levelData)
        {
            levelData.Initialize();
            _width = levelData.TileWidth;
            _height = levelData.TileHeight;
            Tiles = new List<Tile>(_width * _height);
            TilesMatrix = new Tile[_height,_width];
            _grid = new Grid<Tile>(_height, _width, CELL_SIZE,_gridParent.position);
            for (int i = 0; i < _height; i++)
            {
                var isExitTile = i == 0;
                for (int j = 0; j < _width; j++)
                {
                    var tileData = levelData.GetTile(i, j);
                    Vector3 pos = _grid.GetWorldPosition(i, j);
                    var tile = ObjectPooler.Instance.SpawnSc<Tile>(Keys.TILE_POOL_TAG, pos, Quaternion.identity, _gridParent);
                    tile.Initialize(tileData,i,j,isExitTile);
                    tile.SetIndexes(i,j);
                    Tiles.Add(tile);
                    TilesMatrix[i, j] = tile;
                }
            }
        }

        public void ReleaseTiles()
        {
            foreach (var tile in Tiles)
            {
                tile.Reset();
                ObjectPooler.Instance.ReleasePooledObject(Keys.TILE_POOL_TAG, tile);
            }
        }

        [Button]
        public bool GetPath(Tile tile, out List<Tile> path)
        {
            path = PathFinding.FindPathToClosestExit(tile, TilesMatrix,_height,_width);

            if (path != null)
            {
                Debug.Log($"Path found! Tile count: {path.Count}");
                foreach (var t in path)
                {
                    Debug.Log("Path element indexes x : "+ t.X +  " y: " + t.Y , t.transform);
                }

                return true;
            }

            Debug.Log("No path to exit found!");
            return false;
        }
        
    }
}