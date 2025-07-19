using UnityEngine;

/// <summary>
/// Helper class of methods to call specific dialogue triggers.
/// </summary>
public class DialogueTriggers : MonoBehaviour
{
    public static SpeechBubbleController FindSBC()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Campfire");
        SpeechBubbleController sbc = obj.GetComponent<SpeechBubbleController>();
        return sbc;
    }

    public static void TriggerOnSeeStone()
    {
        FindSBC().StartTrigger("onSeeStone");
    }

    public static void TriggerOnLowFuel()
    {
        FindSBC().StartTrigger("onLowFuel");
    }

    public static void TriggerOnCompleteRequest()
    {
        FindSBC().StartTrigger("onCompleteRequest");
    }

    public static void TriggerOnLowAffection()
    {
        FindSBC().StartTrigger("onLowAffection");
    }

    public static void TriggerOnHighAffection()
    {
        FindSBC().StartTrigger("onHighAffection");
    }

    public static void TriggerOnIgnoreRequest()
    {
        FindSBC().StartTrigger("onIgnoreRequest");
    }
}
