using System;
using System.Collections.Generic;
using GGJ23.Audio;
using UnityEngine;
using Random = UnityEngine.Random;

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

        [SerializeField]
        private PlaySoundEffects delaySfx;

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
            FeatureCreepData featureCreepData = featureCreepCollection.GetRandomFeatureCreepData();
            Delay(featureCreepData);
        }

        public void Delay(string[] titles)
        {
            string title = titles[Random.Range(0, titles.Length)];
            Delay(featureCreepCollection.GetFromTitle(title));
        }

        public void Delay(FeatureCreepData featureCreepData)
        {
            AddYearsOfDelay(yearsToAdd);
            string title = featureCreepData.title;
            string description = featureCreepData.GetRandomDescription();
            OnDelay.Invoke(title, description);
            featureCreepToYears[title] += yearsToAdd;
            delaySfx.Play();
        }

        public int GetYearsOfDelay()
        {
            return yearsOfDelay;
        }

        public string GetFullReport()
        {
            string report = "";
            foreach (string title in featureCreepToYears.Keys)
            {
                report += $"{title}: {featureCreepToYears[title]} hours\n";
            }
            return report;
        }

        public string GetReportTitles()
        {
            string reportTitles = "";
            foreach (string title in featureCreepToYears.Keys)
            {
                reportTitles += $"{title}:\n";
            }
            return reportTitles;
        }

        public string GetReportYears()
        {
            string reportYears = "";
            foreach (string title in featureCreepToYears.Keys)
            {
                reportYears += $"{featureCreepToYears[title]} hours\n";
            }
            return reportYears;
        }
    }
}
