using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ23
{
    public class Root : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 1f;

        [SerializeField]
        private float rotationSpeed = 1f;

        private Rigidbody rb;
        private SegmentSpawner segmentSpawner;

        private Vector2 inputDirection;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            segmentSpawner = GetComponent<SegmentSpawner>();
        }

        private void Update()
        {
            float horizontalDelta = Input.GetAxis("Horizontal");
            float verticalDelta = Input.GetAxis("Vertical");
            inputDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        private void FixedUpdate()
        {
            if (inputDirection.y > Mathf.Epsilon)
            {
                transform.Rotate(
                    new Vector3(0, inputDirection.x * Time.fixedDeltaTime * rotationSpeed)
                );
            }
            rb.velocity = transform.forward * inputDirection.y * moveSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckHittables(other);
            CheckOtherSegments(other);
            CheckWalls(other);
        }

        private void CheckWalls(Collider other)
        {
            Wall wall = other.GetComponent<Wall>();
            if (wall == null)
                return;
            EndGame();
        }

        private void CheckOtherSegments(Collider other)
        {
            Segment segment = other.GetComponent<Segment>();
            if (segment == null)
                return;
            if (segment.GetSegmentNumber() > 1)
            {
                EndGame();
            }
        }

        private static void EndGame()
        {
            Debug.Log("YOU DIED");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void CheckHittables(Collider other)
        {
            Hittable hittable = other.gameObject.GetComponent<Hittable>();
            if (hittable == null)
                return;
            hittable.Hit(out bool isFirstTime);
            if (isFirstTime)
            {
                segmentSpawner.AddSegments(hittable.GetSegmentsToAddOnHit());
            }
        }
    }
}
