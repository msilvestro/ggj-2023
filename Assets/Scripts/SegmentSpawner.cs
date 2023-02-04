using UnityEngine;

namespace GGJ23
{
    public class SegmentSpawner : MonoBehaviour
    {
        [SerializeField]
        private Segment segmentPrefab;

        [SerializeField]
        private float spawnDistance = 1f;

        [SerializeField]
        private int maxNumberOfSegments = 10;

        private GameObject segmentsContainer;
        private GameObject despawningSegmentsContainer;
        private Rigidbody rb;
        private Vector3 lastSpawnPosition;

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
                SpawnSegment();
            }
        }

        private void SpawnSegment()
        {
            GameObject newSegment = GameObject.Instantiate(
                segmentPrefab.gameObject,
                rb.position,
                rb.rotation
            );
            newSegment.transform.parent = segmentsContainer.transform;
            lastSpawnPosition = rb.position;
            if (segmentsContainer.transform.childCount > maxNumberOfSegments)
            {
                Transform segmentToDespawn = segmentsContainer.transform.GetChild(0);
                segmentToDespawn.GetComponent<Segment>().Despawn();
                segmentToDespawn.parent = despawningSegmentsContainer.transform;
            }
            for (int i = 0; i < segmentsContainer.transform.childCount; i++)
            {
                segmentsContainer.transform
                    .GetChild(i)
                    .GetComponent<Segment>()
                    .SetSegmentNumber(segmentsContainer.transform.childCount - i);
            }
        }

        public void AddSegments(int newSegments)
        {
            maxNumberOfSegments += newSegments;
        }
    }
}
