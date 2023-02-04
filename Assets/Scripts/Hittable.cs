using UnityEngine;

namespace GGJ23
{
    public class Hittable : MonoBehaviour
    {
        [SerializeField]
        private int segmentsToAddOnHit = 5;

        private int timesHasBeenHit = 0;

        public void Hit(out bool isFirstTime)
        {
            timesHasBeenHit += 1;
            isFirstTime = timesHasBeenHit == 1;
        }

        public int GetSegmentsToAddOnHit()
        {
            return segmentsToAddOnHit;
        }
    }
}
