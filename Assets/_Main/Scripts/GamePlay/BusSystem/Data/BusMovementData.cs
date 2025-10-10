using System;
using DG.Tweening;
using UnityEngine;

namespace _Main.Scripts.GamePlay.BusSystem.Data
{
    [CreateAssetMenu(fileName = "Bus Movement Data", menuName = "Bus/Movement Data", order = 0)]
    public class BusMovementData : ScriptableObject
    {
        public MovementData[] Data;

        public MovementData GetMovementData(MovementType movementType)
        {
            for (var i = 0; i < Data.Length; i++)
            {
                var movementData = Data[i];
                if (movementData.MovementType == movementType)
                {
                    return movementData;
                }
            }

            Debug.Log("No movement data found for " + movementType);

            return Data[0];
        }
    }

    [Serializable]
    public struct MovementData
    {
        public MovementType MovementType;
        public float Duration;
        public Ease Ease;
    }

    public enum MovementType
    {
        In,
        Out
    }
}