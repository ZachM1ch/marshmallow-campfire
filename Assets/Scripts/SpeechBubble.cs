using System.Collections;
using UnityEngine;
using TMPro;
using static DialogueManager;

/// <summary>
/// Handles the text and position of the speech bubbles.
/// </summary>
public class SpeechBubble : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typeSpeed = 0.05f;
    private Transform followTarget;

    public void Initialize(LineData lineData, Transform follow)
    {
        followTarget = follow;
        dialogueText.color = lineData.color;
        dialogueText.fontSize = TextSizes.GetSize(lineData.line.Size);
        StartCoroutine(TypeText(lineData.line.Text));
    }

    IEnumerator TypeText(string text)
    { 
        dialogueText.text = "";
        foreach (char letter in text)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    void Update()
    {
        if (followTarget != null)
        {
            transform.position = followTarget.position + Vector3.up * 2f;
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }
}
