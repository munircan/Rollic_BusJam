using UnityEngine;

namespace _Main.GamePlay.GridSystem
{
    public class Grid<T> where T : IGridObject
    {
        private int _width;
        private int _height;
        private float _cellSize;
        private Vector3 _originPosition;
        private T[,] _gridArray;


        public Grid(int width, int height, float cellSize, Vector3 originPosition)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;
            _gridArray = new T[height, width];
        }   

        public Vector3 GetWorldPosition(int x, int y)
        {
            float totalWidth = (_width - 1) * _cellSize;
            float offsetX = -totalWidth / 2f;
            return _originPosition + new Vector3(y * _cellSize + offsetX, _originPosition.y, -x * _cellSize);
        }

        public void SetValue(int x, int y, T value)
        {
            _gridArray[x, y] = value;
        }

        public T GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
                return _gridArray[x, y];
            Debug.Log("Can't get value for x: " + x + " y: " + y);
            return default;
        }
    }
}