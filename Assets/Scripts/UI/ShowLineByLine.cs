using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ23
{
    public class ShowLineByLine : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text leftText;

        [SerializeField]
        private TMP_Text rightText;
        private string[] leftTextLines;
        private string[] rightTextLines;

        private int lineIndex = 0;

        private void Awake()
        {
            leftTextLines = leftText.text.Split("\n");
            rightTextLines = rightText.text.Split("\n");
            ShowCurrentLines();
        }

        private void ShowCurrentLines()
        {
            string currentLeftText = "";
            string currentRightText = "";
            for (int i = 0; i <= lineIndex; i++)
            {
                currentLeftText += leftTextLines[i] + "\n";
                currentRightText += rightTextLines[i] + "\n";
            }
            leftText.text = currentLeftText;
            rightText.text = currentRightText;
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                lineIndex++;
                if (lineIndex >= leftTextLines.Length - 1)
                {
                    SceneManager.LoadScene("Main Scene");
                    return;
                }
                ShowCurrentLines();
            }
        }
    }
}
