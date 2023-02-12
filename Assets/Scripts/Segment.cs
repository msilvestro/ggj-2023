using System;
using UnityEngine;

namespace GGJ23
{
    public class Segment : MonoBehaviour
    {
        [SerializeField]
        private int segmentNumber = 0;

        [SerializeField]
        private bool destroyOnDespawnEnd = false;

        private Animator animator;

        public event Action OnDespawnEnd;

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
            animator.SetTrigger("despawn");
        }

        public void EndDespawn()
        {
            OnDespawnEnd?.Invoke();
            if (destroyOnDespawnEnd)
            {
                Destroy(gameObject);
            }
        }
    }
}
