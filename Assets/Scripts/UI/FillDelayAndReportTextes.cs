using TMPro;
using UnityEngine;

namespace GGJ23
{
    public class FillDelayAndReportTextes : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text delayText;

        [SerializeField]
        private TMP_Text reportTitleText;

        [SerializeField]
        private TMP_Text reportYearsText;

        private void Start()
        {
            DelayScore delayScore = gameObject.GetGameController<DelayScore>();
            int yearsOfDelay = delayScore.GetYearsOfDelay();
            delayText.text = $"your game\nby {yearsOfDelay} years";
            reportTitleText.text = delayScore.GetReportTitles();
            reportYearsText.text = delayScore.GetReportYears();
        }
    }
}
