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

        switch (size)
        {
            case "tiny":
                dialogueText.fontSize = 20;
                break;
            case "small":
                dialogueText.fontSize = 30;
                break;
            case "normal":
                dialogueText.fontSize = 40;
                break;
            case "large":
                dialogueText.fontSize = 50;
                break;
            case "huge":
                dialogueText.fontSize = 60;
                break;
            default:
                dialogueText.fontSize = 40;
                break;
        }
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
