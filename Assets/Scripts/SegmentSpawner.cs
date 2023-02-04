using UnityEngine;

namespace GGJ23
{
    public class SegmentSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject segmentPrefab;

        [SerializeField]
        private float spawnDistance = 1f;

        [SerializeField]
        private int maxNumberOfSegments = 10;

        private GameObject segmentContainer;
        private Rigidbody rb;
        private Vector3 lastSpawnPosition;

        private void Awake()
        {
            segmentContainer = new GameObject("Segment Container");
            rb = GetComponent<Rigidbody>();
            foreach (Transform childTransform in transform)
            {
                childTransform.parent = segmentContainer.transform;
            }
            lastSpawnPosition = rb.position;
        }

        private void Update()
        {
            if (Vector3.Distance(lastSpawnPosition, rb.position) >= spawnDistance)
            {
                SpawnSegment();
            }
        }

        private void SpawnSegment()
        {
            GameObject newSegment = GameObject.Instantiate(
                segmentPrefab,
                rb.position,
                Quaternion.identity
            );
            newSegment.transform.parent = segmentContainer.transform;
            lastSpawnPosition = rb.position;
            if (segmentContainer.transform.childCount > maxNumberOfSegments)
            {
                Destroy(segmentContainer.transform.GetChild(0).gameObject);
            }
        }

        public void AddSegments(int newSegments)
        {
            maxNumberOfSegments += newSegments;
        }
    }
}
