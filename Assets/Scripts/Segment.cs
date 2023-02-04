using UnityEngine;

namespace GGJ23
{
    public class Segment : MonoBehaviour
    {
        private int segmentNumber = 0;

        public void SetSegmentNumber(int segmentNumber)
        {
            this.segmentNumber = segmentNumber;
        }

        public int GetSegmentNumber()
        {
            return segmentNumber;
        }
    }
}
