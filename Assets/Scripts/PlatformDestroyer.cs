using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    private float destroyYThreshold = -1f; 
    public MainCamera MainCamera;
    // Decreased threshold for platform destruction
    private void Start()
    {
        MainCamera = Camera.main.GetComponent<MainCamera>();
    }
    private void Update()
    {
        // Get the bottom of the screen in world coordinates
        Vector3 bottomOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        // Find all platforms below the screen and destroy them
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platform in platforms)
        {
            if (platform.transform.position.y < bottomOfScreen.y+destroyYThreshold)
            {
                
                MainCamera.GenerateOnePlatform();
                Destroy(platform);

            }
        }
    }
}
