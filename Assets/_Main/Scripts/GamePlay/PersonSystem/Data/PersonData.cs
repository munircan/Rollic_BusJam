using System;
using _Main.Scripts.GamePlay.Settings;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    [Serializable]
    public struct PersonData
    {
        public ColorType colorType;
        public Appearance Appearance;

        public PersonData(PersonData data)
        {
            colorType = data.colorType;
            Appearance = data.Appearance;
        }
    }
}