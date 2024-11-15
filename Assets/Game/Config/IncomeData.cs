using System;
using System.Linq;
using Common;
using UnityEngine;

namespace Config
{
    [Serializable]
    public class IncomeData
    {
        [Header("Auto reward")]
        [SerializeField, Min(0.1f)] private float autoRewardFrequencySeconds = 3;
        [SerializeField] private float autoRewardCount = 3;

        [Header("1 variable")]
        [SerializeField] private int baseTapReward = 1;
        [Header("2 variable")]
        [SerializeField] private int incrementalClickModifier = 1;
        [Header("3 variable")]
        [SerializeField] private float incomeBufferMultiplier = 0.5f;
        [SerializeField, Min(0)] private float incomeBufferLifetimeSeconds = 5;

        [Header("Bonus")]
        [SerializeField, Range(0, 1)] private float AdditionalPercentPerClick = 0.1f;


        public float IncomeBufferLifetimeSeconds => incomeBufferLifetimeSeconds;
        public float AutoRewardFrequencySeconds => autoRewardFrequencySeconds;
        public float AutoRewardCount => autoRewardCount;


        public double CalculateClickReward(RuntimeData runtimeData)
        {
            double result = baseTapReward;

            result += AdditionalPercentPerClick * AutoRewardCount;
            result += incrementalClickModifier;
            double totalIncomeBuffer = runtimeData.IncomeBuffer.Sum();
            result += totalIncomeBuffer * incomeBufferMultiplier;

            return result;
        }
    }
}