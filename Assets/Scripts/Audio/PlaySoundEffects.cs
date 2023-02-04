using UnityEngine;

namespace GGJ23
{
    public class PlaySoundEffects : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] clip;

        [SerializeField]
        private int volume = 1;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        public void Play()
        {
            audioSource.clip = clip[Random.Range(0, clip.Length - 1)];
            audioSource.Play();
        }
    }
}
