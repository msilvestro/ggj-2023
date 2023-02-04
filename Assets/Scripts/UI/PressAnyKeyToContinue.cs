using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ23
{
    public class PressAnyKeyToContinue : MonoBehaviour
    {
        [SerializeField]
        private string sceneName;

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
