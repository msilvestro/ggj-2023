using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ23
{
    public class DelayScore : MonoBehaviour
    {
        private int yearsOfDelay = 0;

        [SerializeField]
        private FeatureCreepCollection featureCreepCollection;

        [SerializeField]
        private int yearsToAdd;

        public event Action<int> OnYearsOfDelayChange;
        public event Action<string, string> OnDelay;

        [SerializeField]
        private Dictionary<string, int> featureCreepToYears = new();

        private void Awake()
        {
            foreach (FeatureCreepData featureCreepData in featureCreepCollection.featureCreepData)
            {
                featureCreepToYears.Add(featureCreepData.title, 0);
            }
        }

        private void Start()
        {
            OnYearsOfDelayChange.Invoke(yearsOfDelay);
        }

        private void AddYearsOfDelay(int yearsToAdd)
        {
            yearsOfDelay += yearsToAdd;
            OnYearsOfDelayChange.Invoke(yearsOfDelay);
        }

        public void Delay()
        {
            AddYearsOfDelay(yearsToAdd);
            FeatureCreepData featureCreepData = featureCreepCollection.GetRandomFeatureCreepData();
            string title = featureCreepData.title;
            string description = featureCreepData.GetRandomDescription();
            OnDelay.Invoke(title, description);
            featureCreepToYears[title] += yearsToAdd;
        }
    }
}
