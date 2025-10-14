using System.Collections.Generic;
using _Main.Scripts.GamePlay.TileSystem.Components;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PathFinding
{
    public static class PathFinding
    {
        private static readonly int[,] NeighbourDirections =
        {
            { 0, 1 }, 
            { 1, 0 },  
            { 0, -1 }, 
            { -1, 0 } 
        };
        
        public static List<Tile> FindPathToClosestExit(Tile startTile, Tile[,] tilesMatrix,int width,int height)
        {
            if (startTile == null || tilesMatrix == null)
            {
                Debug.LogWarning("Pathfinding failed: invalid input.");
                return null;
            }
            
            Queue<Tile> queue = new Queue<Tile>();
            Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();
            HashSet<Tile> visited = new HashSet<Tile>();

            queue.Enqueue(startTile);
            visited.Add(startTile);

            while (queue.Count > 0)
            {
                Tile current = queue.Dequeue();

                if (current.IsExitTile && (current == startTile || !current.IsOccupied))
                {
                    return ReconstructPath(cameFrom, current, startTile);
                }
                
                var neighbours = GetNeighbors(current, tilesMatrix, width, height);
                for (var i = 0; i < neighbours.Count; i++)
                {
                    var neighbor = neighbours[i];
                    if (visited.Contains(neighbor) || neighbor.IsOccupied)
                    {
                        continue;
                    }
                        
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    cameFrom[neighbor] = current;
                }
            }

            // No path found
            return null;
        }

        private static List<Tile> GetNeighbors(Tile tile, Tile[,] matrix, int width, int height)
        {
            List<Tile> neighbors = new List<Tile>();
            
            for (int i = 0; i < 4; i++)
            {
                int nx = tile.X + NeighbourDirections[i, 0];
                int ny = tile.Y + NeighbourDirections[i, 1];

                if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                {
                    neighbors.Add(matrix[nx, ny]);
                }
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