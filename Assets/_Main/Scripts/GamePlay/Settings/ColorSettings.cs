using System;
using _Main.Patterns.Singleton;
using UnityEngine;

namespace _Main.Scripts.GamePlay.Settings
{
    [CreateAssetMenu(fileName = "ColorSettings", menuName = "Settings/Color Settings", order = 0)]
    public class ColorSettings : SingletonScriptableObject<ColorSettings>
    {
        public ColorData[] Data;


        public Material GetMaterial(PersonColor color)
        {
            for (var i = 0; i < Data.Length; i++)
            {
                var colorData = Data[i];
                if (colorData.Color == color)
                {
                    return colorData.Material;
                }
            }

            return null;
        }
    }

    [Serializable]
    public struct ColorData
    {
        public PersonColor Color;
        public Material Material;
    }

    public enum PersonColor
    {
        White,
        Blue,
        Red,
        Green
    }
}