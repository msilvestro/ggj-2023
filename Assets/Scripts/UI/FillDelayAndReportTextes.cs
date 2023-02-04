using TMPro;
using UnityEngine;

namespace GGJ23
{
    public class FillDelayAndReportTextes : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text delayText;

        [SerializeField]
        private TMP_Text reportText;

        private void Start()
        {
            DelayScore delayScore = gameObject.GetGameController<DelayScore>();
            int yearsOfDelay = delayScore.GetYearsOfDelay();
            delayText.text = $"{yearsOfDelay} hours";
            reportText.text = delayScore.GetReport();
        }
    }
}
