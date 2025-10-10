using System;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    [Serializable]
    public struct PersonData
    {
        public PersonColor Color;

        public PersonData(PersonData data)
        {
            Color = data.Color;
        }
    }

    public enum PersonColor
    {
        White,
        Blue,
        Red,
        Green
    }
}