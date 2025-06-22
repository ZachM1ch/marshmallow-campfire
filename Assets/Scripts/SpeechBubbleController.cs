using System.Collections;
using UnityEngine;

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
            yield return new WaitForSeconds(Random.Range(5f, 15f)); // Formerly speakInterval

            var tuple = dialogueManager.GetNextLine(characterName);
            string line = tuple.Item1;
            string size = tuple.Item2;
            string textColor = tuple.Item3;

            var bubble = Instantiate(speechBubblePrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
            bubble.GetComponent<SpeechBubble>().Initialize(line, size, transform, textColor);

            yield return new WaitForSeconds(showDuration);

            Destroy(bubble.gameObject);
        }
    }
}
