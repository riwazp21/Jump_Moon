using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool characterOnPlatform;
    private bool characterLeftFor10Seconds;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the character is on the platform
        if (other.CompareTag("Character"))
        {
            characterOnPlatform = true;
            characterLeftFor10Seconds = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the character is leaving the platform
        if (other.CompareTag("Character"))
        {
            characterOnPlatform = false;
            StartCoroutine(DelayedDestroy(10f));
        }
    }

    private IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Check if the character has not returned to the platform
        if (!characterOnPlatform)
        {
            Destroy(gameObject);
        }
        else
        {
            characterLeftFor10Seconds = true;
        }
    }
}
