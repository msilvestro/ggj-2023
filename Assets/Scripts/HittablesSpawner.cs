using UnityEngine;

namespace GGJ23
{
    public class HittablesSpawner : MonoBehaviour
    {
        [SerializeField]
        private Hittable hittablePrefab;

        [SerializeField]
        private int numberToSpawnAtStart = 1;

        [SerializeField]
        private Transform terrainPlane;

        [SerializeField]
        private Vector2 spawnAreaOffset = new Vector2(0, 0);

        private Vector2 spawnArea;

        private void Awake()
        {
            spawnArea = new Vector2(terrainPlane.localScale.x * 10, terrainPlane.localScale.z * 10);
        }

        private void Start()
        {
            for (int i = 0; i < numberToSpawnAtStart; i++)
            {
                SpawnHittable();
            }
        }

        private void SpawnHittable()
        {
            Hittable newHittable = GameObject.Instantiate(
                hittablePrefab,
                new Vector3(
                    (spawnArea.x / 2 - spawnAreaOffset.x) * Random.Range(-1f, 1f),
                    0,
                    (spawnArea.y / 2 - spawnAreaOffset.y) * Random.Range(-1f, 1f)
                ),
                Quaternion.identity
            );
            newHittable.transform.parent = transform;
            newHittable.OnHit += SpawnAfterHit;
            Debug.Log("spawn!");
        }

        private void SpawnAfterHit(int numberOfHits)
        {
            if (numberOfHits > 1)
            {
                return;
            }
            SpawnHittable();
        }
    }
}
