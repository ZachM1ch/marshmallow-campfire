using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using static DialogueManager;

public class SpeechBubble : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typeSpeed = 0.05f;
    private Transform followTarget;

    public void Initialize(LineData lineData, Transform follow)
    {
        followTarget = follow;
        StartCoroutine(TypeText(lineData.line.Text));
        dialogueText.color = lineData.color;

        dialogueText.fontSize = TextSizes.GetSize(lineData.line.Size);
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
