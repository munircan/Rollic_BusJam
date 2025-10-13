using System.Collections.Generic;
using System.Linq;
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
                    var tile = ObjectPooler.Instance.SpawnSc<Tile>(Keys.PoolTags.TILE, pos, Quaternion.identity, _gridParent);
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
                ObjectPooler.Instance.ReleasePooledObject(Keys.PoolTags.TILE, tile);
            }
            Tiles.Clear();
        }
        
        public bool GetPath(Tile tile, out List<Vector3> path)
        {
            var tilePath = PathFinding.FindPathToClosestExit(tile, TilesMatrix,_height,_width);

            path = new List<Vector3>();
            if (tilePath != null)
            {
                path = tilePath.GetPathAsVector3List().ToList();
                return true;
            }
            
            return false;
        }
        
    }
}