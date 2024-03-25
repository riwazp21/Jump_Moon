using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject character;
    public float yOffset = 2f;

    private void Update()
    {
        // Update the camera's position to follow the character in the y-direction
        transform.position = new Vector3(transform.position.x, character.transform.position.y + yOffset, transform.position.z);
    }
}