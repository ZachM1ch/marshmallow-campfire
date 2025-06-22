using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [XmlRoot("DialogueData")]
    public class DialogueData
    {
        [XmlElement("Character")]
        public List<CharacterDialogue> Characters = new List<CharacterDialogue>();
    }

    public class CharacterDialogue
    {
        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("color")]
        public string Color;

        [XmlElement("Line")]
        public List<DialogueLines> Lines = new List<DialogueLines>();
    }

    public class DialogueLines
    {
        [XmlAttribute("text")]
        public string Text;

        [XmlAttribute("size")]
        public string Size;
    }

    private Dictionary<string, (Queue<DialogueLines> lines, string color)> dialogueDict = new Dictionary<string, (Queue<DialogueLines>, string)>();

    void Start()
    {
        LoadDialogue();
    }

    void LoadDialogue()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "passive_dialogue.xml");
        string xmlText = File.ReadAllText(path);

        XmlSerializer serializer = new XmlSerializer(typeof(DialogueData));
        using (StringReader reader = new StringReader(xmlText))
        {
            DialogueData data = serializer.Deserialize(reader) as DialogueData;
            foreach (var character in data.Characters)
            {
                dialogueDict[character.Name] = (new Queue<DialogueLines>(character.Lines), character.Color);
            }
        }
    }

    public (string, string, string) GetNextLine(string characterName)
    {
        if (dialogueDict.ContainsKey(characterName))
        {
            var queue = dialogueDict[characterName];
            DialogueLines line = queue.lines.Dequeue();
            queue.lines.Enqueue(line); // loop the dialogue
            string color = queue.color;
            return (line.Text, line.Size, color);
        }
        return ("", "normal", "#FFFFFF");
    }

}
