using System;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.GamePlay.PersonSystem
{
    [CreateAssetMenu(fileName = "PersonMovementData", menuName = "PersonData/MovementData", order = 0)]
    public class PersonMovementData : ScriptableObject
    {
        public PersonPathMovementData PathMovementData;
        public PersonSlotMovementData SlotMovementData;
        public PersonBusMovementData BusMovementData;
    }

    [Serializable]
    public struct PersonPathMovementData
    {
        public float Duration;
        public PathType PathType;
        public PathMode PathMode;
        public Ease Ease;
    }

    [Serializable]
    public struct PersonSlotMovementData
    {
        public float Duration;
        public Ease Ease;
    }

    [Serializable]
    public struct PersonBusMovementData
    {
        public float Duration;
        public float JumpPower;
        public int JumpCount;
        public Ease Ease;
    }
}