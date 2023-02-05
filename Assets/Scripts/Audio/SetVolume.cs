using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GGJ23.Audio
{
    public class SetVolume : MonoBehaviour
    {
        private enum VolumeType
        {
            MasterVolume,
            MusicVolume,
            SoundEffectsVolume
        }

        [SerializeField]
        private AudioMixer mixer;

        [SerializeField]
        private VolumeType volumeType = VolumeType.MasterVolume;

        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        private void Start()
        {
            float volume;
            mixer.GetFloat(volumeType.ToString(), out volume);
            slider.value = Mathf.Pow(10, volume / 20);
        }

        public void SetLevel(float sliderValue)
        {
            float volume;
            if (sliderValue == 0)
            {
                volume = -80;
            }
            else
            {
                volume = Mathf.Log10(sliderValue) * 20;
            }
            mixer.SetFloat(volumeType.ToString(), volume);
        }
    }
}
