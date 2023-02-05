using Cinemachine;
using UnityEngine;

namespace GGJ23.Utils
{
    public class CameraShake : MonoBehaviour
    {
        private CinemachineBrain cinemachineBrain;

        [SerializeField]
        private AnimationCurve defaultAmplitudeGainCurve;

        [SerializeField]
        private AnimationCurve frequencyGainCurve;

        private float shakeTimer = 0;
        private bool isShaking = false;
        private float maxShakeTime;
        private AnimationCurve amplitudeGainCurve;

        private void Awake()
        {
            cinemachineBrain = GetComponent<CinemachineBrain>();
        }

        public void ShakeCamera()
        {
            ShakeCamera(defaultAmplitudeGainCurve);
        }

        public void ShakeCamera(AnimationCurve customAmplitudeGainCurve)
        {
            amplitudeGainCurve = customAmplitudeGainCurve;
            maxShakeTime = Mathf.Max(
                amplitudeGainCurve[amplitudeGainCurve.length - 1].time,
                frequencyGainCurve[frequencyGainCurve.length - 1].time
            );

            GameObject currentVCam = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject;
            if (currentVCam.GetComponent<CinemachineNoise>() == null)
            {
                return;
            }

            shakeTimer = 0;
            isShaking = true;
        }

        private void Update()
        {
            if (!isShaking)
            {
                return;
            }

            shakeTimer += Time.deltaTime;
            float amplitudeGain = amplitudeGainCurve.Evaluate(shakeTimer);
            float frequencyGain = frequencyGainCurve.Evaluate(shakeTimer);
            GameObject currentVCam = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject;
            CinemachineNoise cinemachineNoise = currentVCam.GetComponent<CinemachineNoise>();
            if (cinemachineNoise)
            {
                currentVCam
                    .GetComponent<CinemachineNoise>()
                    .UpdateNoise(amplitudeGain, frequencyGain);
            }
            if (shakeTimer >= maxShakeTime)
            {
                shakeTimer = 0;
                isShaking = false;
            }
        }
    }
}
