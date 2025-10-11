using System.Collections.Generic;

namespace _Main.Scripts.Utilities
{
    public static class ListHelpers
    {
        public static T GetElementWithMod<T>(this List<T> list, int index)
        {
            var count = list.Count;
            return list[index % count];
        }
        
        public static T GetElementWithMod<T>(this T[] array, int index)
        {
            var lenght = array.Length;
            return array[index % lenght];
        }

    }
}