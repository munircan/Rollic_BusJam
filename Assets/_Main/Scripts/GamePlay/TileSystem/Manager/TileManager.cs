using System.Collections.Generic;
using _Main.GamePlay.GridSystem;
using _Main.Patterns.ObjectPooling;
using UnityEngine;

namespace _Main.GamePlay.TileSystem.Manager
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private Transform _gridParent;
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        public List<Tile> Tiles { get; private set; }

        private Grid<Tile> _grid;
        private const float CELL_SIZE = 1.25f;
        private const string POOL_TAG = "Tile";

        private void OnEnable()
        {
            CreateTiles();
        }

        public void CreateTiles()
        {
            Tiles = new List<Tile>();
           
            _grid = new Grid<Tile>(_width, _height, CELL_SIZE,_gridParent.position);
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    var tileData = new TileData();
                    tileData.Type = TileType.Default;
                    Vector3 pos = _grid.GetWorldPosition(i, j);
                    var tile = ObjectPooler.Instance.SpawnSc<Tile>(POOL_TAG, pos, Quaternion.identity, _gridParent);
                    tile.Initialize(tileData);
                    Tiles.Add(tile);
                }
            }
        }

    }
}