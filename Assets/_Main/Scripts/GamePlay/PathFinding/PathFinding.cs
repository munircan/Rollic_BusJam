using System.Collections.Generic;
using _Main.GamePlay.TileSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PathFinding
{
    public static class PathFinding
    {
        /// <summary>
        /// Finds the shortest path from a start tile to the nearest exit tile.
        /// </summary>
        public static List<Tile> FindPathToClosestExit(Tile startTile, Tile[,] tilesMatrix)
        {
            if (startTile == null || tilesMatrix == null)
            {
                Debug.LogWarning("Pathfinding failed: invalid input.");
                return null;
            }

            int width = tilesMatrix.GetLength(0);
            int height = tilesMatrix.GetLength(1);

            Queue<Tile> queue = new Queue<Tile>();
            Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();
            HashSet<Tile> visited = new HashSet<Tile>();

            queue.Enqueue(startTile);
            visited.Add(startTile);

            while (queue.Count > 0)
            {
                Tile current = queue.Dequeue();
                
                if (current.IsExitTile)
                    return ReconstructPath(cameFrom, current, startTile);

                var neighbours = GetNeighbors(current, tilesMatrix, width, height);
                foreach (Tile neighbor in neighbours)
                {
                    if (!visited.Contains(neighbor) && !neighbor.IsOccupied)
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        cameFrom[neighbor] = current;
                    }
                }
            }

            // No path found
            return null;
        }

        private static List<Tile> GetNeighbors(Tile tile, Tile[,] matrix, int width, int height)
        {
            List<Tile> neighbors = new List<Tile>();
            int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };

            for (int i = 0; i < 4; i++)
            {
                int nx = tile.X + directions[i, 0];
                int ny = tile.Y + directions[i, 1];

                if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                    neighbors.Add(matrix[nx, ny]);
            }

            return neighbors;
        }

        private static List<Tile> ReconstructPath(Dictionary<Tile, Tile> cameFrom, Tile endTile, Tile startTile)
        {
            List<Tile> path = new List<Tile>();
            Tile current = endTile;

            while (current != startTile)
            {
                path.Add(current);
                current = cameFrom[current];
            }

            path.Add(startTile);
            path.Reverse();

            return path;
        }
    }
}