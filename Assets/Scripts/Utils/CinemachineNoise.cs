using Cinemachine;
using UnityEngine;

namespace GGJ23.Utils
{
    public class CinemachineNoise : MonoBehaviour
    {
        private CinemachineBasicMultiChannelPerlin cinemachineNoise;

        private void Awake()
        {
            CinemachineVirtualCamera cinemachineVirtualCamera =
                GetComponent<CinemachineVirtualCamera>();
            cinemachineNoise =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            UpdateNoise(0, 0);
        }

        public void UpdateNoise(float amplitudeGain, float frequencyGain)
        {
            cinemachineNoise.m_AmplitudeGain = amplitudeGain;
            cinemachineNoise.m_FrequencyGain = frequencyGain;
        }
    }
}
