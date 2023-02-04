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

        private Vector3 movementInput;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            float horizontalDelta = Input.GetAxis("Horizontal");
            float verticalDelta = Input.GetAxis("Vertical");
            movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        }

        private void FixedUpdate()
        {
            Vector3 moveVector = transform.TransformDirection(movementInput) * moveSpeed;
            // rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
            transform.Rotate(new Vector3(0, movementInput.x * Time.fixedDeltaTime * rotationSpeed));
            rb.velocity = transform.forward * movementInput.z * moveSpeed;
        }
    }
}
