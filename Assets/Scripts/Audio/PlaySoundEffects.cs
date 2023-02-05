using UnityEngine;

namespace GGJ23.Audio
{
    public class PlaySoundEffects : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] clips;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        public void Play()
        {
            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
        }
    }
}
