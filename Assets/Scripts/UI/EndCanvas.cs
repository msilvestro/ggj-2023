using System;
using UnityEngine;

namespace GGJ23
{
    public class EndCanvas : MonoBehaviour
    {
        [SerializeField]
        private RectTransform youDelayedPanel;

        [SerializeField]
        private RectTransform reportPanel;

        [SerializeField]
        private Canvas mainCanvas;

        private Root playerRoot;

        private void Awake()
        {
            playerRoot = gameObject.GetPlayer<Root>();
            youDelayedPanel.gameObject.SetActive(false);
            reportPanel.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            playerRoot.OnGameEnd += ShowEndCanvas;
        }

        private void OnDisable()
        {
            playerRoot.OnGameEnd -= ShowEndCanvas;
        }

        private void ShowEndCanvas()
        {
            mainCanvas.gameObject.SetActive(false);
            youDelayedPanel.gameObject.SetActive(true);
        }
    }
}
