using System;
using System.Collections.Generic;

namespace _Main.Scripts.GamePlay.SaveSystem
{
    [Serializable]
    public class TileIndexData
    {
        public List<int> TileIndexes = new();
        public int Level;
        
        public void AddTileIndex(int i)
        {
            TileIndexes.Add(i);
        }

        public void ClearList()
        {
            TileIndexes.Clear();
        }
    }
}