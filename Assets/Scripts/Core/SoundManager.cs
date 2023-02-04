using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ23.Core
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        private Dictionary<string, float> soundEffectsLastPlayed;
        private Dictionary<int, AudioSource> soundEffectSourcesCollection;

        [SerializeField]
        private AudioSource musicAudioSource;

        [SerializeField]
        private AudioSource sfxAudioSource;

        [Tooltip(
            "Every sound effect will be played only after this amount of seconds has passed since the last time it was played."
        )]
        [SerializeField]
        private float minTimeBetweenSfxs = 0.1f;

        [Header("Background music")]
        [SerializeField]
        private AudioClip backgroundMusic;

        [Range(0, 2)]
        [SerializeField]
        private float musicVolume = 1f;

        [SerializeField]
        private bool hasIntro = false;

        [SerializeField]
        private float startLoopTime = 0f;

        [SerializeField]
        private float endLoopTime = 0f;

        private Coroutine playMusicWithIntroCoroutine;

        private void Awake()
        {
            if (FindObjectsOfType(GetType()).Length > 1)
            {
                instance.PlayMusic(
                    backgroundMusic,
                    musicVolume,
                    hasIntro,
                    startLoopTime,
                    endLoopTime
                );
                instance.ShouldMusicLoop(musicAudioSource.loop);
                gameObject.SetActive(false);
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);

            instance = this;
            PlayMusic(backgroundMusic, musicVolume, hasIntro, startLoopTime, endLoopTime);
            soundEffectsLastPlayed = new Dictionary<string, float>();
            soundEffectSourcesCollection = new Dictionary<int, AudioSource>();
        }

        public void PlayMusic(
            AudioClip music,
            float volume,
            bool hasIntro,
            float startLoopTime,
            float endLoopTime
        )
        {
            if (musicAudioSource.clip == music && musicAudioSource.isPlaying)
            {
                return;
            }
            if (playMusicWithIntroCoroutine != null)
            {
                StopCoroutine(playMusicWithIntroCoroutine);
                playMusicWithIntroCoroutine = null;
            }

            musicAudioSource.clip = music;
            musicAudioSource.volume = volume;

            if (!hasIntro)
            {
                musicAudioSource.Play();
            }
            else
            {
                playMusicWithIntroCoroutine = StartCoroutine(
                    PlayMusicWithIntro(startLoopTime, endLoopTime)
                );
            }
        }

        private IEnumerator PlayMusicWithIntro(float startLoopTime, float endLoopTime)
        {
            // wait for intro clip to be loaded before starting
            // this is useful to avoid sync problems
            musicAudioSource.clip.LoadAudioData();
            while (musicAudioSource.clip.loadState != AudioDataLoadState.Loaded)
            {
                yield return null;
            }
            musicAudioSource.Play();
            while (true)
            {
                if (musicAudioSource.time > endLoopTime)
                {
                    musicAudioSource.time = startLoopTime + (musicAudioSource.time - endLoopTime);
                }
                yield return null;
            }
        }

        public void ShouldMusicLoop(bool loop)
        {
            musicAudioSource.loop = loop;
        }

        public float GetMusicTime()
        {
            return musicAudioSource.time;
        }

        private bool CanPlaySoundEffect(AudioClip audioClip)
        {
            if (
                soundEffectsLastPlayed.ContainsKey(audioClip.name)
                && Time.time - soundEffectsLastPlayed[audioClip.name] < minTimeBetweenSfxs
            )
            {
                return false;
            }
            soundEffectsLastPlayed[audioClip.name] = Time.time;
            return true;
        }

        public void PlayOneShotSoundEffect(AudioClip[] audioClips, float volume = 1f)
        {
            if (audioClips.Length == 0)
            {
                return;
            }

            int clipIndex = Random.Range(0, audioClips.Length);
            PlayOneShotSoundEffect(audioClips[clipIndex], volume);
        }

        public void PlayOneShotSoundEffect(AudioClip audioClip, float volume = 1f)
        {
            if (audioClip == null)
            {
                return;
            }
            if (!CanPlaySoundEffect(audioClip))
            {
                return;
            }

            sfxAudioSource.PlayOneShot(audioClip, volume);
        }

        public int PlaySoundEffect(AudioClip audioClip, float volume = 1f, bool loop = false)
        {
            if (audioClip == null)
            {
                Debug.LogWarning("No audio clip to play");
                return -1;
            }

            GameObject newSfxObject = new GameObject(audioClip.name);
            AudioSource newSfxAudioSource = newSfxObject.AddComponent<AudioSource>();
            newSfxAudioSource.outputAudioMixerGroup = sfxAudioSource.outputAudioMixerGroup;
            newSfxAudioSource.loop = loop;
            newSfxAudioSource.clip = audioClip;
            newSfxAudioSource.volume = volume;
            newSfxAudioSource.transform.parent = transform;
            newSfxAudioSource.Play();

            int audioSourceId = newSfxObject.GetInstanceID();
            soundEffectSourcesCollection[audioSourceId] = newSfxAudioSource;
            return audioSourceId;
        }

        public void StopSoundEffect(int audioSourceId)
        {
            if (!soundEffectSourcesCollection.ContainsKey(audioSourceId))
            {
                Debug.LogWarning($"No audio source with key {audioSourceId}");
                return;
            }
            if (soundEffectSourcesCollection[audioSourceId] == null)
            {
                return;
            }

            soundEffectSourcesCollection[audioSourceId].Stop();
            Destroy(soundEffectSourcesCollection[audioSourceId].gameObject);
            soundEffectSourcesCollection.Remove(audioSourceId);
        }

        public void FadeAndStopSoundEffect(int audioSourceId, float timeToFade)
        {
            if (!soundEffectSourcesCollection.ContainsKey(audioSourceId))
            {
                Debug.LogWarning($"No audio source with key {audioSourceId}");
                return;
            }
            if (soundEffectSourcesCollection[audioSourceId] == null)
            {
                return;
            }

            StartCoroutine(FadeAndStop(audioSourceId, timeToFade));
        }

        private IEnumerator FadeAndStop(int audioSourceId, float timeToFade)
        {
            float fadeTimer = 0;
            AudioSource audioSource = soundEffectSourcesCollection[audioSourceId];
            float initialVolume = soundEffectSourcesCollection[audioSourceId].volume;
            while (fadeTimer < timeToFade)
            {
                fadeTimer += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(initialVolume, 0, fadeTimer / timeToFade);
                yield return null;
            }
            StopSoundEffect(audioSourceId);
        }
    }
}
