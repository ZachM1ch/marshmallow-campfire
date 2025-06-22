using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActiveDialogueManager : DialogueManager
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
        public List<string> Lines = new List<string>();
    }
    /*
    private Dictionary<string, (Queue<string> lines, string color)> dialogueDict = new Dictionary<string, (Queue<string>, string)>();

    [Header("UI References")]
    public GameObject dialogueUIPrefab;
    public Sprite portrait;
    private GameObject currentUI;
    private TextMeshProUGUI dialogueText;
    private Image portraitImage;

    private Queue<(string, string)> currentLines;
    private bool isActive = false;
    private string currentCharacter;

    void Start()
    {
        LoadDialogue();
    }

    void LoadDialogue()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "active_dialogue.xml");
        string xmlText = File.ReadAllText(path);

        XmlSerializer serializer = new XmlSerializer(typeof(DialogueData));
        using (StringReader reader = new StringReader(xmlText))
        {
            DialogueData data = serializer.Deserialize(reader) as DialogueData;
            foreach (var character in data.Characters)
            {
                dialogueDict[character.Name] = (new Queue<string>(character.Lines), character.Color);
            }
        }
    }

    public void BeginDialogue(string characterName)
    {
        if (!dialogueDict.ContainsKey(characterName) || isActive)
            return;

        currentLines = dialogueDict[characterName].lines;
        currentCharacter = characterName;

        currentUI = Instantiate(dialogueUIPrefab, GameObject.Find("Canvas").transform);
        dialogueText = currentUI.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        portraitImage = currentUI.transform.Find("Portrait").GetComponent<Image>();

        if (portraitImage != null && portrait != null)
            portraitImage.sprite = portrait;

        isActive = true;
        ShowNextLine();
    }

    void Update()
    {
        if (!isActive)
            return;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
        {
            ShowNextLine();
        }
    }

    void ShowNextLine()
    {
        if (currentLines == null || currentLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        var (line, color) = currentLines.Dequeue();
        dialogueText.text = $"<color={color}>{line}</color>";
    }

    void EndDialogue()
    {
        Destroy(currentUI);
        isActive = false;
        OnDialogueFinished(currentCharacter);
    }

    protected virtual void OnDialogueFinished(string characterName)
    {
        Debug.Log($"Active dialogue for {characterName} finished. Hook into game logic here.");
        // You could broadcast a UnityEvent or call a GameManager here
    }
    */
}
