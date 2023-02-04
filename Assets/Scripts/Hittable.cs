using System;
using System.Collections;
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

        private int timesHasBeenHit = 0;
        private Rigidbody rb;
        private Animator animator;

        public event Action<int> OnHit;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
            trailRenderer.enabled = false;
        }

        private void FixedUpdate()
        {
            trailRenderer.enabled = rb.velocity.magnitude > minimumVelocityToDisplayTrailRenderer;
        }

        public void Hit(out bool isFirstTime)
        {
            timesHasBeenHit += 1;
            isFirstTime = timesHasBeenHit == 1;
            animator.SetTrigger("hit");
            trailRenderer.enabled = true;
            ThrowInTheAir();
            OnHit.Invoke(timesHasBeenHit);
            if (isFirstTime && destroyAfterHit)
            {
                StartCoroutine(DelayDespawn(destroyDelayInSeconds));
            }
        }

        public int GetSegmentsToAddOnHit()
        {
            return segmentsToAddOnHit;
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
