using UnityEngine;

namespace GGJ23.Utils
{
    public class ScreenshakeController : MonoBehaviour
    {
        [SerializeField]
        private bool startOnAwake = false;

        [SerializeField]
        private bool useCustomAmplitudeGainCurve = false;

        [SerializeField]
        private AnimationCurve amplitudeGainCurve;

        private void Start()
        {
            if (startOnAwake)
            {
                StartScreenshake();
            }
        }

        public void StartScreenshake()
        {
            if (useCustomAmplitudeGainCurve)
            {
                Camera.main.GetComponent<CameraShake>().ShakeCamera(amplitudeGainCurve);
            }
            else
            {
                Camera.main.GetComponent<CameraShake>().ShakeCamera();
            }
        }
    }
}
