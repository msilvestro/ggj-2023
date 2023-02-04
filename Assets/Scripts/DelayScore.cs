using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GGJ23
{
    public class DelayScore : MonoBehaviour
    {
        [SerializeField]
        private int yearsOfDelay = 0;

        [SerializeField]
        private FeatureCreepCollection featureCreepCollection;

        public event Action<int> OnYearsOfDelayChange;
        public event Action<string, string> OnDelay;

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
            AddYearsOfDelay(100);
            FeatureCreepData featureCreepData = featureCreepCollection.GetRandomFeatureCreepData();
            OnDelay.Invoke(featureCreepData.title, featureCreepData.GetRandomDescription());
        }
    }
}
