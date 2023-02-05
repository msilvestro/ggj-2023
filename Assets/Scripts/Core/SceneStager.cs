using Cinemachine;
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

        private void Update()
        {
            if (currentScene != SceneType.Menu)
            {
                return;
            }
            if (Input.anyKeyDown)
            {
                menuVirtualCamera.Priority = -10;
                foreach (GameObject objectToActivate in objectsToActivate)
                {
                    objectToActivate.SetActive(true);
                }
                foreach (GameObject objectToDeactivate in objectsToDeactivate)
                {
                    objectToDeactivate.SetActive(false);
                }
            }
        }
    }
}
