using UnityEngine;

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

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Hit(out bool isFirstTime)
        {
            timesHasBeenHit += 1;
            isFirstTime = timesHasBeenHit == 1;
            ThrowInTheAir();
        }

        public int GetSegmentsToAddOnHit()
        {
            return segmentsToAddOnHit;
        }

        private void ThrowInTheAir()
        {
            rb.AddForce(
                new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f)) * hitForceMagnitude,
                ForceMode.Impulse
            );
        }
    }
}
