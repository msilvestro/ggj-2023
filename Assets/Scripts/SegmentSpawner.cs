using System.Collections;
using UnityEngine;

namespace GGJ23
{
    public class SegmentSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject segmentPrefab;

        [SerializeField]
        private float spawnFrequencyInSeconds = 1f;

        private GameObject segmentContainer;
        private Rigidbody rb;

        private void Awake()
        {
            segmentContainer = new GameObject();
            rb = GetComponent<Rigidbody>();
            foreach (Transform childTransform in transform)
            {
                childTransform.parent = segmentContainer.transform;
            }
        }

        private void Start()
        {
            StartCoroutine(SpawnSegment(spawnFrequencyInSeconds));
        }

        private IEnumerator SpawnSegment(float spawnFrequencyInSeconds)
        {
            while (true)
            {
                if (rb.velocity.magnitude > Mathf.Epsilon)
                {
                    GameObject newSegment = GameObject.Instantiate(
                        segmentPrefab,
                        transform.position,
                        Quaternion.identity
                    );
                    newSegment.transform.parent = segmentContainer.transform;
                }
                else
                {
                    Debug.Log("Not spawned because velocity too low.");
                }
                yield return new WaitForSeconds(spawnFrequencyInSeconds);
            }
        }
    }
}
