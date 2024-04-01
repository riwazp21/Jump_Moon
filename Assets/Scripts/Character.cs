using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public GameObject platformPrefab; // Reference to the platform prefab
    public float platformHeight = 1f; // Height of the new platform above the current platform

    private Rigidbody rb;
    private bool isGrounded;
    private float minX, maxX;
    public MainCamera MainCamera;
    
     

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        MainCamera = Camera.main.GetComponent<MainCamera>();
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
            //SceneManager.LoadScene("EndScreen");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            // Instantiate a new platform above the current platform
            //if (MainCamera.platformCount <= 5)
            //{
            //MainCamera.GenerateOnePlatform();
            isGrounded = true;
            //}
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