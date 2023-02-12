using UnityEngine;
using UnityEngine.Pool;

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
        private IObjectPool<GameObject> segmentsPool;

        private void Awake()
        {
            segmentsContainer = new GameObject("Segments Container");
            despawningSegmentsContainer = new GameObject("Despawning Segments Container");
            rb = GetComponent<Rigidbody>();
            lastSpawnPosition = rb.position;
            segmentsPool = new LinkedPool<GameObject>(
                createFunc: CreateSegment,
                actionOnGet: OnTakeSegment,
                actionOnRelease: OnReturnedSegment,
                actionOnDestroy: OnDestroySegment,
                collectionCheck: true,
                maxSize: 10_000
            );
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
            GameObject newSegment = segmentsPool.Get();
            newSegment.transform.position = rb.position;
            newSegment.transform.rotation = rb.rotation;
            newSegment.transform.parent = segmentsContainer.transform;
            lastSpawnPosition = rb.position;
            if (segmentsContainer.transform.childCount > segmentsCount)
            {
                StartLastSegmentDespawn();
            }
            UpdateSegmentNumbers();
        }

        private GameObject CreateSegment()
        {
            GameObject newSegment = GameObject.Instantiate(GetNextSegment().gameObject);
            newSegment.GetComponent<Segment>().OnDespawnEnd += () =>
                segmentsPool.Release(newSegment);
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

#region Extra pool functions
        private void OnTakeSegment(GameObject segment)
        {
            segment.SetActive(true);
        }

        private void OnReturnedSegment(GameObject segment)
        {
            segment.SetActive(false);
        }

        private void OnDestroySegment(GameObject segment)
        {
            Destroy(segment);
        }
    }
#endregion
}
