using System.Collections;
using UnityEngine;
using TMPro; // If you’re using TextMeshProUGUI

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // Assign your TextMeshProUGUI
    public string fullText = "This is your typing animation text!";
    public float typingSpeed = 0.05f;     // Adjust typing speed
    public AudioSource typingSound;

    void Start()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        dialogueText.text = "";
        foreach (char c in fullText)
        {
            dialogueText.text += c;
            if (typingSound != null)
                typingSound.Play();
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
