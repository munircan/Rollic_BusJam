using System;
using _Main.Patterns.Singleton;
using _Main.Scripts.GamePlay.PersonSystem;
using UnityEngine;

namespace _Main.Scripts.GamePlay.Settings
{
    [CreateAssetMenu(fileName = "ColorSettings", menuName = "Settings/Color Settings", order = 0)]
    public class ColorSettings : SingletonScriptableObject<ColorSettings>
    {
        public ColorData[] Data;
        public Material MysteriosPersonMaterial;


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
        
        public Material GetPersonMaterial(PersonColor color,PersonType type,bool canWalk)
        {
            if (type == PersonType.Mysterious)
            {
                return MysteriosPersonMaterial;
            }
            for (var i = 0; i < Data.Length; i++)
            {
                var colorData = Data[i];
                if (colorData.Color == color)
                {
                    if (canWalk)
                    {
                        return colorData.MaterialWithOutline;
                    }
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
        public Material MaterialWithOutline;
    }

    public enum PersonColor
    {
        White,
        Blue,
        Red,
        Green
    }
}