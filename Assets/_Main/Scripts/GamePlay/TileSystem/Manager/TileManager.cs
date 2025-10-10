using System.Collections.Generic;
using _Main.GamePlay.GridSystem;
using _Main.Patterns.ObjectPooling;
using _Main.Scripts.GamePlay.LevelSystem;
using _Main.Scripts.GamePlay.PathFinding;
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
        private const string POOL_TAG = "Tile";

        public Tile[,] TilesMatrix;

   
        public void CreateTiles(LevelData levelData)
        {
            levelData.Initialize();
            var width = levelData.TileWidth;
            var height = levelData.TileHeight;
            Tiles = new List<Tile>(width * height);
            TilesMatrix = new Tile[height,width];
            _grid = new Grid<Tile>(height, width, CELL_SIZE,_gridParent.position);
            var isExitTile = false;
            for (int i = 0; i < height; i++)
            {
                isExitTile = i == 0;
                for (int j = 0; j < width; j++)
                {
                    var tileData = levelData.GetTile(i, j);
                    Vector3 pos = _grid.GetWorldPosition(i, j);
                    var tile = ObjectPooler.Instance.SpawnSc<Tile>(POOL_TAG, pos, Quaternion.identity, _gridParent);
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
                ObjectPooler.Instance.ReleasePooledObject(POOL_TAG, tile);
            }
        }

        [Button]
        public void GetPath(Tile tile)
        {
            List<Tile> path = PathFinding.FindPathToClosestExit(tile, TilesMatrix);

            if (path != null)
            {
                Debug.Log($"Path found! Tile count: {path.Count}");
                for (var i = 0; i < path.Count; i++)
                {
                    var t = path[i];
                    Debug.Log("Path element indexes x : "+ t.X +  " y: " + t.Y , t.transform);
                }
            }
            else
            {
                Debug.Log("No path to exit found!");
            }
        }

    }
}