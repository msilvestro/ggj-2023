using Cinemachine;
using GGJ23.Audio;
using UnityEngine;

namespace GGJ23
{
    public class SceneStager : MonoBehaviour
    {
        private enum SceneType
        {
            Menu,
            Play
        }

        private SceneType currentScene = SceneType.Menu;

        [SerializeField]
        private CinemachineVirtualCamera menuVirtualCamera;

        [SerializeField]
        private GameObject[] objectsToActivate;

        [SerializeField]
        private GameObject[] objectsToDeactivate;

        [SerializeField]
        private float deactivateAfterSeconds = 6f;

        [SerializeField]
        private PlaySoundEffects clickSfx;

        private void Update()
        {
            if (currentScene != SceneType.Menu)
            {
                return;
            }
            if (Input.anyKeyDown)
            {
                clickSfx.Play();
                menuVirtualCamera.Priority = -10;
                foreach (GameObject objectToActivate in objectsToActivate)
                {
                    objectToActivate.SetActive(true);
                }
                Invoke(nameof(Deactivate), deactivateAfterSeconds);
                currentScene = SceneType.Play;
            }
        }

        private void Deactivate()
        {
            foreach (GameObject objectToDeactivate in objectsToDeactivate)
            {
                objectToDeactivate.SetActive(false);
            }
        }
    }
}
