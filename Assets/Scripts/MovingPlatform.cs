using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveRange = 5f; // Total range the platform can move
    private Vector3 originalPosition;
    private bool moveRight = true;
    private bool isCharacterOnTop = false;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the new position based on the current direction
        float movement = moveSpeed * Time.deltaTime * (moveRight ? 1 : -1);
        Vector3 newPosition = transform.position + new Vector3(movement, 0f, 0f);

        // Move the platform horizontally
        transform.position = newPosition;

        // Change direction if the platform reaches the end of the range
        if (Mathf.Abs(transform.position.x - originalPosition.x) >= moveRange)
        {
            moveRight = !moveRight;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            isCharacterOnTop = true;
            // Prevent vertical movement
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            isCharacterOnTop = false;
        }
    }
}

