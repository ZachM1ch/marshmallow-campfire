using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class SpeechBubble : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typeSpeed = 0.05f;
    private Transform followTarget;
    public Color textColoring;

    public void Initialize(string text, string size, Transform follow, string textColor)
    {
        followTarget = follow;
        StartCoroutine(TypeText(text));
        if(ColorUtility.TryParseHtmlString(textColor, out textColoring))
        {
            dialogueText.color = textColoring;
        }

        dialogueText.fontSize = TextSizes.GetSize(size);
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
