using System;
using UnityEngine;

namespace GGJ23
{
    public class EndCanvas : MonoBehaviour
    {
        [SerializeField]
        private RectTransform canvasBackground;

        [SerializeField]
        private Canvas mainCanvas;

        private Root playerRoot;

        private void Awake()
        {
            playerRoot = gameObject.GetPlayer<Root>();
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
            canvasBackground.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
