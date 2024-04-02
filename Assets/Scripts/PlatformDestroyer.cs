using UnityEngine;
using TMPro;

public class PlatformDestroyer : MonoBehaviour
{
    private float destroyYThreshold = -1f; 
    public int Score = 0; 
    public int HighScore = 0; 
    public MainCamera MainCamera;

    private void Start()
    {
        MainCamera = Camera.main.GetComponent<MainCamera>();
        // Load the high score if it's saved
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        // Update UI for both score and high score
        UpdateScoreText();
        UpdateHighScoreText();
    }

    private void Update()
    {
        // Get the bottom of the screen in world coordinates
        Vector3 bottomOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        // Find all platforms below the screen and destroy them
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platform in platforms)
        {
            if (platform.transform.position.y < bottomOfScreen.y + destroyYThreshold)
            {
                MainCamera.GenerateOnePlatform();
                Destroy(platform);
                Score += 100;
                UpdateScoreText(); // Update the score text when score changes

                // Check for new high score
                if (Score > HighScore)
                {
                    HighScore = Score;
                    PlayerPrefs.SetInt("HighScore", HighScore); // Save the high score
                    UpdateHighScoreText(); // Update high score UI
                }
            }
        }
    }

    // Update the TextMeshPro component with the current score
    private void UpdateScoreText()
    {
        GameObject scoreObject = GameObject.FindGameObjectWithTag("CurrScore");
        if (scoreObject != null)
        {
            TextMeshProUGUI scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
            if (scoreText != null)
            {
                scoreText.text = "Score: " + Score.ToString();
            }
        }
    }

    // Update the TextMeshPro component with the high score
    private void UpdateHighScoreText()
    {
        GameObject highScoreObject = GameObject.FindGameObjectWithTag("HighScore");
        if (highScoreObject != null)
        {
            TextMeshProUGUI highScoreText = highScoreObject.GetComponent<TextMeshProUGUI>();
            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + HighScore.ToString();
            }
        }
    }
}
