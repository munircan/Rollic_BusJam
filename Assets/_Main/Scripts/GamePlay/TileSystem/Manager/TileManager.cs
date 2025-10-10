using System.Collections.Generic;
using _Main.GamePlay.GridSystem;
using _Main.Patterns.ObjectPooling;
using _Main.Scripts.GamePlay.LevelSystem;
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

   
        public void CreateTiles(LevelData levelData)
        {
            Tiles = new List<Tile>();
            levelData.Initialize();
            var width = levelData.TileWidth;
            var height = levelData.TileHeight;
            _grid = new Grid<Tile>(width, height, CELL_SIZE,_gridParent.position);
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

    }
}