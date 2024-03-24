using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private bool isGrounded;
    private float minX, maxX;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Calculate screen boundaries
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        minX = -screenBounds.x + 0.5f; // Add a small buffer to prevent sticking to the edge
        maxX = screenBounds.x - 0.5f; // Add a small buffer to prevent sticking to the edge

        // Set initial position on top of the platform
        GameObject platform = GameObject.FindGameObjectWithTag("Platform");
        if (platform != null)
        {
            transform.position = new Vector3(platform.transform.position.x, platform.transform.position.y + 1f, platform.transform.position.z);
        }
    }

    private void Update()
    {
        // Move sideways
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Clamp position to screen boundaries
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);

        // Check if grounded

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Check if character falls below the screen
        if (transform.position.y < -Camera.main.orthographicSize)
        {
            print("Game Over");
            // Optionally, reset the character's position or perform other game over actions
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}


