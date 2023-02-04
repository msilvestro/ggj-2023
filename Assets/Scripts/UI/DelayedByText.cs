using System;
using TMPro;
using UnityEngine;

namespace GGJ23.UI
{
    public class DelayedByText : MonoBehaviour
    {
        private DelayScore delayScore;

        private TMP_Text textBox;

        private void Awake()
        {
            textBox = GetComponent<TMP_Text>();
            delayScore = gameObject.GetGameController<DelayScore>();
        }

        private void OnEnable()
        {
            delayScore.OnYearsOfDelayChange += UpdateDelayScore;
        }

        private void OnDisable()
        {
            delayScore.OnYearsOfDelayChange -= UpdateDelayScore;
        }

        private void UpdateDelayScore(int yearsOfDelay)
        {
            textBox.text = $"Delayed by\n{yearsOfDelay} years";
        }
    }
}
