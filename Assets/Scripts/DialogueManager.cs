using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    [XmlRoot("DialogueData")]
    public class DialogueData
    {
        [XmlElement("Character")]
        public List<DialogueCharacter> Characters = new List<DialogueCharacter>();
    }

    public class DialogueCharacter
    {
        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("color")]
        public string Color;

        [XmlArray("PassiveLines")]
        [XmlArrayItem("Line")]
        public List<DialogueLine> PassiveLines = new List<DialogueLine>();

        [XmlArray("TriggeredLines")]
        [XmlArrayItem("Trigger")]
        public List<DialogueTrigger> TriggeredLines = new List<DialogueTrigger>();
    }

    public class DialogueTrigger
    {
        [XmlAttribute("type")]
        public string TriggerType;

        [XmlElement("Line")]
        public List<DialogueLine> Lines = new List<DialogueLine>();
    }

    public class DialogueLine
    {
        [XmlAttribute("text")]
        public string Text;

        [XmlAttribute("size")]
        public string Size;
    }

    // Passive lines: name => (line queue, color)
    private Dictionary<string, (Queue<DialogueLine> passiveLines, string color)> passiveDialogueDict =
        new Dictionary<string, (Queue<DialogueLine>, string)>();

    // Triggered lines: name => (trigger type => line queue, color)
    private Dictionary<string, (Dictionary<string, Queue<DialogueLine>> triggeredLines, string color)> triggeredDialogueDict =
        new Dictionary<string, (Dictionary<string, Queue<DialogueLine>>, string)>();

    void Start()
    {
        LoadDialogue();
    }

    void LoadDialogue()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "dialogue.xml");
        string xmlText = File.ReadAllText(path);

        XmlSerializer serializer = new XmlSerializer(typeof(DialogueData));
        using (StringReader reader = new StringReader(xmlText))
        {
            DialogueData data = serializer.Deserialize(reader) as DialogueData;
            foreach (var character in data.Characters)
            {
                var passiveQueue = new Queue<DialogueLine>(character.PassiveLines);
                passiveDialogueDict[character.Name] = (passiveQueue, character.Color);

                var triggeredDict = new Dictionary<string, Queue<DialogueLine>>();
                foreach (var trigger in character.TriggeredLines)
                {
                    if (!triggeredDict.ContainsKey(trigger.TriggerType))
                    {
                        triggeredDict[trigger.TriggerType] = new Queue<DialogueLine>(trigger.Lines);
                    }
                }

                triggeredDialogueDict[character.Name] = (triggeredDict, character.Color);
            }
        }
    }

    /// <summary>
    /// Get next passive line for character
    /// </summary>
    /// <param name="characterName">  </param>
    /// <returns>  </returns>
    public (string, string, string) GetNextPassiveLine(string characterName)
    {
        if (passiveDialogueDict.ContainsKey(characterName))
        {
            var (queue, color) = passiveDialogueDict[characterName];
            DialogueLine line = queue.Dequeue();
            queue.Enqueue(line); // loop
            return (line.Text, line.Size, color);
        }

        return ("", "normal", "#FFFFFF");
    }

    /// <summary>
    /// Get line for character and trigger
    /// </summary>
    /// <param name="characterName">  </param>
    /// <param name="trigger">  </param>
    /// <returns>  </returns>
    public (string, string, string) GetTriggeredLine(string characterName, string trigger)
    {
        if (triggeredDialogueDict.ContainsKey(characterName))
        {
            var (triggerDict, color) = triggeredDialogueDict[characterName];
            if (triggerDict.ContainsKey(trigger))
            {
                var queue = triggerDict[trigger];
                if (queue.Count > 0)
                {
                    DialogueLine line = queue.Dequeue();
                    queue.Enqueue(line); // loop
                    return (line.Text, line.Size, color);
                }
            }
        }

        return ("", "normal", "#FFFFFF");
    }

}
