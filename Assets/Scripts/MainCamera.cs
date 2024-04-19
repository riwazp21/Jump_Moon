using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject platformPrefab;
    public GameObject movingPlatformPrefab;

    public GameObject gapPlatformPrefab;
    public GameObject movingGapPrefab;
    public GameObject spikePrefab;
    public GameObject movingSpikePrefab;
    public float yOffset = 2f;
    private float platformGap = 6f;

    public float platformNewGap = 4f;
    public int platformCount = 0;
    public float minX = -5f;
    public float maxX = 5f;
    public float lowestPlatformY = -5f;
    public float movingPlatformProbability = 0.1f;

    public float newY = 0f;

    private GameObject characterObject;

    private void Start()
    {
        // Generate platforms
        GeneratePlatforms();

        // Generate character on the lowest platform
        GenerateCharacter();

        // Subscribe to the OnPlatformLanded event
        //Character.OnPlatformLanded += OnPlatformLanded;
    }

  public void GenerateOnePlatform()
{
    float totalHeight = platformCount * platformGap;
    float startY = lowestPlatformY - totalHeight / 2f;
    float xPos = Random.Range(minX, maxX);
    float yPos = startY + platformCount * platformGap; // Use platformGap here
    float randomValue = Random.value;
    GameObject platformToInstantiate;

    if (randomValue < 0.40f)
    {
        platformToInstantiate = platformPrefab;
    }
    else if (randomValue < 0.60f)
    {
        platformToInstantiate = movingPlatformPrefab;
        // Set x-position to midpoint
        xPos = (minX + maxX) / 2f;
    }
    else if (randomValue < 0.80f)
    {
        platformToInstantiate = gapPlatformPrefab;
    }
    else if (randomValue < 0.90f)
    {

        platformToInstantiate = movingGapPrefab;
        xPos = (minX + maxX)/2f;
    }
    else if (randomValue < 0.95f)
    {
        platformToInstantiate = spikePrefab;
    }
    else
    {
        platformToInstantiate = movingSpikePrefab;
        xPos = (minX + maxX)/2f;
    }

    Vector3 platformPosition = new Vector3(xPos, yPos, 0f);
    Instantiate(platformToInstantiate, platformPosition, Quaternion.identity);
    platformCount++;
}


    private void GeneratePlatforms()
    {
    float totalHeight = platformCount * platformGap;
    float startY = lowestPlatformY - totalHeight / 2f;
    float xPos = Random.Range(minX, maxX);
    float yPos = startY + platformCount * platformGap; // Use platformGap here
    Vector3 platformPosition = new Vector3(xPos,yPos,0f);
    Instantiate(platformPrefab,platformPosition,Quaternion.identity);
    platformCount++;
        for (int i = 0; i<4; i++)
        {
            GenerateOnePlatform();
        }
    
        /*float totalHeight = (platformCount-1) * platformGap;
        float startY = lowestPlatformY - totalHeight / 2f;
        print("Total normal height at start: " + totalHeight);
        for (int i = 0; i < platformCount; i++)
        {
           float xPos = Random.Range(minX, maxX);
            float yPos = startY + i * platformGap;

            GameObject platformToInstantiate = Random.value < movingPlatformProbability ? movingPlatformPrefab : platformPrefab;

            Vector3 platformPosition = new Vector3(xPos, yPos, 0f);
            Instantiate(platformToInstantiate, platformPosition, Quaternion.identity);
            
        }
    */
        
    }

    private void GenerateCharacter()
    {
        // Place the character on top of the lowest platform
        Vector3 characterPosition = new Vector3(0f, lowestPlatformY + platformGap / 2f, 0f); // Adjust the y-offset as needed
        characterObject = Instantiate(characterPrefab, characterPosition, Quaternion.identity);
    }

    private void OnPlatformLanded(Transform platform)
    {
        // Instantiate a new platform above the current one
        Vector3 platformPosition = platform.position + Vector3.up * platformGap; // Adjust the height as needed
        Instantiate(platform.gameObject, platformPosition, Quaternion.identity);
    }

    private void Update()
    {
        // Update the camera's position to follow the character in the y-direction
        if (characterObject != null)
        {
            transform.position = new Vector3(transform.position.x, characterObject.transform.position.y + yOffset, transform.position.z);
        }
    }
}
