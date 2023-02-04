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
                segmentPrefab.gameObject,
                rb.position,
                Quaternion.identity
            );
            newSegment.transform.parent = segmentContainer.transform;
            lastSpawnPosition = rb.position;
            if (segmentContainer.transform.childCount > maxNumberOfSegments)
            {
                Destroy(segmentContainer.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Segment>().SetSegmentNumber(i);
            }
        }

        public void AddSegments(int newSegments)
        {
            maxNumberOfSegments += newSegments;
        }
    }
}
