using UnityEngine;

namespace GGJ23
{
    public class Segment : MonoBehaviour
    {
        [SerializeField]
        private int segmentNumber = 0;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetSegmentNumber(int segmentNumber)
        {
            this.segmentNumber = segmentNumber;
        }

        public int GetSegmentNumber()
        {
            return segmentNumber;
        }

        public void Despawn()
        {
            animator.SetTrigger("destroy");
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
