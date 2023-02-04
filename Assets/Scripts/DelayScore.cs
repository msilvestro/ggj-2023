using System;
using UnityEngine;

namespace GGJ23
{
    public class DelayScore : MonoBehaviour
    {
        [SerializeField]
        private int yearsOfDelay = 0;

        public event Action<int> OnYearsOfDelayChange;

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
        }
    }
}
