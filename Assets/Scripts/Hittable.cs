using System;
using System.Collections;
using GGJ23.Audio;
using GGJ23.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GGJ23
{
    public class Hittable : MonoBehaviour
    {
        [SerializeField]
        private int segmentsToAddOnHit = 5;

        [SerializeField]
        private float hitForceMagnitude = 10f;

        [SerializeField]
        private bool destroyAfterHit = false;

        [SerializeField]
        private float destroyDelayInSeconds = 10f;

        [SerializeField]
        private TrailRenderer trailRenderer;

        [SerializeField]
        private float minimumVelocityToDisplayTrailRenderer = 0.5f;

        [SerializeField]
        private PlaySoundEffects voiceSfx;

        [SerializeField]
        private string[] titlesToShow;

        [Space]
        [SerializeField]
        private bool onlyThrowForward = false;

        private int timesHasBeenHit = 0;
        private Rigidbody rb;
        private Animator animator;

        private DelayScore delayScore;

        public event Action<int> OnHit;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
            trailRenderer.enabled = false;
        }

        private void Start()
        {
            delayScore = gameObject.GetGameController<DelayScore>();
        }

        private void FixedUpdate()
        {
            trailRenderer.enabled = rb.velocity.magnitude > minimumVelocityToDisplayTrailRenderer;
        }

        public void Hit(Quaternion hitterRotation, out bool isFirstTime)
        {
            timesHasBeenHit += 1;
            isFirstTime = timesHasBeenHit == 1;
            animator.SetTrigger("hit");
            trailRenderer.enabled = true;
            if (onlyThrowForward)
            {
                ThrowInTheAir(hitterRotation);
            }
            else
            {
                ThrowInTheAir();
            }
            OnHit.Invoke(timesHasBeenHit);
            if (isFirstTime && destroyAfterHit)
            {
                StartCoroutine(DelayDespawn(destroyDelayInSeconds));
            }
            if (isFirstTime)
            {
                voiceSfx.Play();
                delayScore.Delay(titlesToShow);
            }
        }

        public int GetSegmentsToAddOnHit()
        {
            return segmentsToAddOnHit;
        }

        private void ThrowInTheAir(Quaternion hitterRotation)
        {
            rb.constraints = RigidbodyConstraints.None;
            Vector3 resultDirection =
                hitterRotation * (new Vector3(Random.Range(-1f, 1f), 1, Random.Range(0, 1f)));
            rb.AddForce(resultDirection.normalized * hitForceMagnitude, ForceMode.Impulse);
        }

        private void ThrowInTheAir()
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(
                new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f)).normalized
                    * hitForceMagnitude,
                ForceMode.Impulse
            );
        }

        private IEnumerator DelayDespawn(float delay)
        {
            yield return new WaitForSeconds(delay);
            animator.SetTrigger("destroy");
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
