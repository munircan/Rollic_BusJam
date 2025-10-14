using System;
using _Main.Scripts.Patterns.Singleton;
using UnityEngine;

namespace _Main.Scripts.GamePlay.Settings
{
    [CreateAssetMenu(fileName = "ColorSettings", menuName = "Settings/Color Settings", order = 0)]
    public class ColorSettings : SingletonScriptableObject<ColorSettings>
    {
        public ColorData[] Data;
        public Material MysteriosPersonMaterial;


        public Material GetMaterial(ColorType colorType)
        {
            for (var i = 0; i < Data.Length; i++)
            {
                var colorData = Data[i];
                if (colorData.colorType == colorType)
                {
                    return colorData.Material;
                }
            }

            return null;
        }
        
        public Material GetPersonMaterial(ColorType colorType,Appearance type,bool canWalk)
        {
            if (!canWalk && type == Appearance.Mysterious)
            {
                return MysteriosPersonMaterial;
            }
            for (var i = 0; i < Data.Length; i++)
            {
                var colorData = Data[i];
                if (colorData.colorType == colorType)
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
        public ColorType colorType;
        public Material Material;
    }

    public enum ColorType
    {
        White,
        Blue,
        Red,
        Green
    }
    
    public enum Appearance
    {
        Default,
        Mysterious
    }
}