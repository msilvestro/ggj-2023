using UnityEngine;

namespace GGJ23
{
    public class SegmentSpawner : MonoBehaviour
    {
        [SerializeField]
        private Segment[] segmentsPrefab;

        [SerializeField]
        private float spawnDistance = 1f;

        [SerializeField]
        private int segmentsCount = 10;

        private GameObject segmentsContainer;
        private GameObject despawningSegmentsContainer;
        private Rigidbody rb;
        private Vector3 lastSpawnPosition;

        private int lastSegmentIndex = -1;

        private void Awake()
        {
            segmentsContainer = new GameObject("Segments Container");
            despawningSegmentsContainer = new GameObject("Despawning Segments Container");
            rb = GetComponent<Rigidbody>();
            lastSpawnPosition = rb.position;
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(lastSpawnPosition, rb.position) >= spawnDistance)
            {
                AddHeadSegment();
            }
        }

        private Segment GetNextSegment()
        {
            lastSegmentIndex = (lastSegmentIndex + 1) % segmentsPrefab.Length;
            return segmentsPrefab[lastSegmentIndex];
        }

        private void AddHeadSegment()
        {
            GameObject newSegment = CreateSegment();
            if (segmentsContainer.transform.childCount > segmentsCount)
            {
                StartLastSegmentDespawn();
            }
            UpdateSegmentNumbers();
        }

        private GameObject CreateSegment()
        {
            GameObject newSegment = GameObject.Instantiate(
                GetNextSegment().gameObject,
                rb.position,
                rb.rotation
            );
            newSegment.transform.parent = segmentsContainer.transform;
            lastSpawnPosition = rb.position;
            newSegment.GetComponent<Segment>().OnDespawnEnd += () => Destroy(newSegment);
            return newSegment;
        }

        private void StartLastSegmentDespawn()
        {
            Transform segmentToDespawn = segmentsContainer.transform.GetChild(0);
            Segment segment = segmentToDespawn.GetComponent<Segment>();
            segment.Despawn();
            segmentToDespawn.parent = despawningSegmentsContainer.transform;
        }

        private void UpdateSegmentNumbers()
        {
            for (int i = 0; i < segmentsContainer.transform.childCount; i++)
            {
                segmentsContainer.transform
                    .GetChild(i)
                    .GetComponent<Segment>()
                    .SetSegmentNumber(segmentsContainer.transform.childCount - i);
            }
        }

        public void IncreaseSegmentsCount(int newSegments)
        {
            segmentsCount += newSegments;
        }
    }
}
