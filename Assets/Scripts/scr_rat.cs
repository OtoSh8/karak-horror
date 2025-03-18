using UnityEngine;
using System.Collections;

public class MouseJumpscare : MonoBehaviour
{
    public GameObject mouse; // Assign the mouse GameObject
    public Transform startPoint; // Start position
    public Transform endPoint; // End position
    public float speed = 5f; // Speed of the mouse
    private bool hasTriggered = false;
    private AudioSource mouseSound; // Reference to the sound effect

    void Start()
    {
        if (mouse != null)
        {
            mouse.SetActive(false);
            mouse.transform.position = startPoint.position;
            mouseSound = mouse.GetComponent<AudioSource>(); // Get the AudioSource from the mouse
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Car") || !hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            mouse.SetActive(true); // Show the mouse
            if (mouseSound != null)
            {
                mouseSound.Play(); // Play sound effect
            }
            StartCoroutine(MoveMouse());
        }
    }

    IEnumerator MoveMouse()
    {
        GameObject.Find("obj_player").GetComponent<scr_react>().Shocked();

        while (Vector3.Distance(mouse.transform.position, endPoint.position) > 0.1f)
        {
            mouse.transform.position = Vector3.MoveTowards(mouse.transform.position, endPoint.position, speed * Time.deltaTime);
            yield return null;
        }

        
        yield return new WaitForSeconds(0.5f); // Wait before hiding
        mouse.SetActive(false);
    }
}
