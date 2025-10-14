using System.Collections.Generic;
using _Main.GamePlay.TileSystem;
using _Main.Scripts.GamePlay.TileSystem.Components;
using Vector3 = UnityEngine.Vector3;

namespace _Main.Scripts.GamePlay.Helpers
{
    public static class PathHelpers
    {
        public static IEnumerable<Vector3> GetPathAsVector3List(this List<Tile> tiles)
        {
            for (var i = 0; i < tiles.Count; i++)
            {
                var tilePosition = tiles[i].transform.position;
                yield return tilePosition;
            }
        }
    }
}