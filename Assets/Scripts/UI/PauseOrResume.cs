using UnityEngine;

namespace GGJ23
{
    public class PauseOrResume : MonoBehaviour
    {
        [SerializeField]
        private Canvas pauseMenuCanvas;
        private bool isPaused = false;

        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseMenuCanvas.gameObject.SetActive(true);
        }

        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseMenuCanvas.gameObject.SetActive(false);
        }

        public void Toggle()
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Toggle();
            }
        }
    }
}
