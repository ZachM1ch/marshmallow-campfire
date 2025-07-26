using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using Unity.VisualScripting;
using System.Drawing;
using Color = UnityEngine.Color;

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

        public DialogueLine() 
        { 
        }

        public DialogueLine(string param_text, string param_size) 
        {
            Text = param_text;
            Size = param_size;
        }
    }

    public class LineData
    {
        public DialogueLine line;
        public Color color;

        public LineData(DialogueLine param_line, string param_color)
        {
            line = param_line;
            color = ConvertToColor(param_color);
        }

        public LineData(DialogueLine param_line, Color param_color)
        {
            line = param_line;
            color = param_color;
        }

        public LineData(string param_text, string param_size, string param_color)
        {
            line.Text = param_text;
            line.Size = param_size;
            color = ConvertToColor(param_color);
        }

        public LineData(string param_text, string param_size, Color param_color)
        {
            line.Text = param_text;
            line.Size = param_size;
            color = param_color;
        }

        public Color ConvertToColor(string param_color)
        {
            Color tempColor;
            if (UnityEngine.ColorUtility.TryParseHtmlString(param_color, out tempColor))
            {
                return tempColor;
            }
            else
            {
                return Color.black;
            }
        }
    }

    // Passive lines: name => (line data)
    private Dictionary<string, Queue<LineData>> passiveDialogueDict =
        new Dictionary<string, Queue<LineData>>();

    // Triggered lines: name => (trigger type => line data)
    private Dictionary<string, Dictionary<string, LineData>> triggeredDialogueDict =
        new Dictionary<string, Dictionary<string, LineData>>();

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
                // Passive Lines
                Queue<LineData> passiveQueue = new Queue<LineData>();
                foreach (var dialine in character.PassiveLines)
                {
                    passiveQueue.Enqueue(new LineData(dialine, character.Color));
                }
                passiveDialogueDict[character.Name] = passiveQueue;

                // Triggered Lines
                Dictionary<string, LineData> triggeredDict = new Dictionary<string, LineData>();
                foreach (var trigger in character.TriggeredLines)
                {
                    if (!triggeredDict.ContainsKey(trigger.TriggerType))
                    {
                        if (trigger.Lines.Count != 0)
                        {
                            DialogueLine dialine = trigger.Lines[0];
                            triggeredDict[trigger.TriggerType] = new LineData(dialine, character.Color);
                        }
                    }
                }
                triggeredDialogueDict[character.Name] = triggeredDict;
            }
        }
    }

    /// <summary>
    /// Get next passive line for character
    /// </summary>
    /// <param name="characterName">  </param>
    /// <returns>  </returns>
    public LineData GetNextPassiveLine(string characterName)
    {
        if (passiveDialogueDict.ContainsKey(characterName))
        {
            Queue<LineData> lineQueue = passiveDialogueDict[characterName];
            LineData line = lineQueue.Dequeue();
            lineQueue.Enqueue(line); // loop
            return line;
        }

        return new LineData("", "normal", "black");
    }

    /// <summary>
    /// Get line for character and trigger
    /// </summary>
    /// <param name="characterName">  </param>
    /// <param name="trigger">  </param>
    /// <returns>  </returns>
    public LineData GetTriggeredLine(string characterName, string trigger)
    {
        if (triggeredDialogueDict.ContainsKey(characterName))
        {
            Dictionary<string, LineData> triggerDict = triggeredDialogueDict[characterName];
            if (triggerDict.ContainsKey(trigger))
            {
                LineData lineData = triggerDict[trigger];
                return (lineData);
            }
        }

        return new LineData("", "normal", "black");
    }

}
