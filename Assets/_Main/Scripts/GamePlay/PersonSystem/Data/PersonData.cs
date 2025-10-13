using System;
using _Main.Scripts.GamePlay.Settings;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    [Serializable]
    public struct PersonData
    {
        public PersonColor Color;
        public PersonType Type;

        public PersonData(PersonData data)
        {
            Color = data.Color;
            Type = data.Type;
        }
    }

    public enum PersonType
    {
        Default,
        Mysterious
    }

   
}