using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GGJ23.UI
{
    public class FeatureCreepDisplayer : MonoBehaviour
    {
        private DelayScore delayScore;

        [SerializeField]
        private TMP_Text featureCreepTitle;

        [SerializeField]
        private TMP_Text featureCreepDescription;

        [SerializeField]
        private float timeVisibleInSeconds = 5f;

        private void Awake()
        {
            delayScore = gameObject.GetGameController<DelayScore>();
        }

        private void OnEnable()
        {
            delayScore.OnDelay += OnDelay;
        }

        private void OnDisable()
        {
            delayScore.OnDelay -= OnDelay;
        }

        private void OnDelay(string title, string description)
        {
            StopAllCoroutines();
            StartCoroutine(ShowAndHideTexts(title, description));
        }

        private IEnumerator ShowAndHideTexts(string title, string description)
        {
            featureCreepTitle.text = title;
            featureCreepDescription.text = description;
            featureCreepTitle.gameObject.SetActive(true);
            featureCreepDescription.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeVisibleInSeconds);
            featureCreepTitle.gameObject.SetActive(false);
            featureCreepDescription.gameObject.SetActive(false);
        }
    }
}
