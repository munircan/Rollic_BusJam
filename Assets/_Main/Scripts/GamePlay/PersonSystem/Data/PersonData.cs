using System;
using System.Collections.Generic;
using _Main.Scripts.GamePlay.Settings;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem.Data
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
    
    public struct PersonPathData
    {
        public bool HasPath;
        public List<Vector3> PathPositions;
    }
}