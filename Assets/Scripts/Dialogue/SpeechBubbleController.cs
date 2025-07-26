using System.Collections;
using UnityEngine;
using static DialogueManager;

public class SpeechBubbleController : MonoBehaviour
{
    public string characterName;
    public float speakInterval = 30f;
    public float showDuration = 4f;

    public DialogueManager dialogueManager;
    public GameObject speechBubblePrefab;

    void Start()
    {
        StartCoroutine(SpeakRoutine());
    }

    IEnumerator SpeakRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 8f)); // Formerly speakInterval

            LineData nextLine = dialogueManager.GetNextPassiveLine(characterName);

            var bubble = Instantiate(speechBubblePrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
            bubble.GetComponent<SpeechBubble>().Initialize(nextLine, transform);

            yield return new WaitForSeconds(showDuration);

            Destroy(bubble.gameObject);
        }
    }

    IEnumerator TriggerSpeakRoutine(string trigger)
    {
        GameObject[] speechBubbles = GameObject.FindGameObjectsWithTag("SpeechBubble");
        foreach (GameObject speechBubble in speechBubbles)
        {
            Destroy(speechBubble);
        }

        LineData nextLine = dialogueManager.GetTriggeredLine(characterName, trigger);

        var bubble = Instantiate(speechBubblePrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
        bubble.GetComponent<SpeechBubble>().Initialize(nextLine, transform);

        yield return new WaitForSeconds(showDuration);

        Destroy(bubble.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(TriggerSpeakRoutine("onSeeStone"));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(TriggerSpeakRoutine("onLowFuel"));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TriggerSpeakRoutine("onCompleteRequest"));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(TriggerSpeakRoutine("onLowAffection"));
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(TriggerSpeakRoutine("onHighAffection"));
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(TriggerSpeakRoutine("onIgnoreRequest"));
        }
    }
}
