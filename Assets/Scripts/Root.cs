using UnityEngine;

namespace GGJ23
{
    public class Root : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 1f;

        [SerializeField]
        private float rotationSpeed = 1f;

        private Rigidbody rb;

        private Vector2 inputDirection;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
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
    }
}
