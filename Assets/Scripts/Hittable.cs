using System;
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

        private int timesHasBeenHit = 0;
        private Rigidbody rb;
        private Animator animator;

        public event Action<int> OnHit;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
        }

        public void Hit(out bool isFirstTime)
        {
            timesHasBeenHit += 1;
            isFirstTime = timesHasBeenHit == 1;
            animator.SetTrigger("hit");
            ThrowInTheAir();
            OnHit.Invoke(timesHasBeenHit);
        }

        public int GetSegmentsToAddOnHit()
        {
            return segmentsToAddOnHit;
        }

        private void ThrowInTheAir()
        {
            rb.AddForce(
                new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f)).normalized
                    * hitForceMagnitude,
                ForceMode.Impulse
            );
        }
    }
}
