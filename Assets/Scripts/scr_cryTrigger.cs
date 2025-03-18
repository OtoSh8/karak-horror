using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource; // Assign the AudioSource in Inspector

    private bool hasPlayed = false; // Optional: Prevent multiple plays

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            if (audioSource != null)
            {
                audioSource.Play();
                hasPlayed = true;
            }
            else
            {
                Debug.LogError("AudioSource not assigned on " + gameObject.name);
            }
        }
    }
}
