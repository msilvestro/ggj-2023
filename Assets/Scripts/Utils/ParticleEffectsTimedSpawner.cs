using System.Collections;
using UnityEngine;

namespace GGJ23.Utils
{
    public class ParticleEffectsTimedSpawner : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particleEffect;

        [SerializeField]
        private float secondsBetweenSpawns;

        private IEnumerator Start()
        {
            while (true)
            {
                particleEffect.Play();
                yield return new WaitForSeconds(secondsBetweenSpawns);
            }
        }
    }
}
