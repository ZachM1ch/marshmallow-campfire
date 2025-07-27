using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Labeller : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coords = new Vector2Int();
    GridManager gridManager;


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponentInChildren<TextMeshPro>();

        DisplayCoords();
        transform.name = coords.ToString();
    }

    private void DisplayCoords()
    {
        if (!gridManager)  
            return;

        coords.x = Mathf.RoundToInt(transform.position.x);
        coords.y = Mathf.RoundToInt(transform.position.z);

        if(label)
            label.text = $"({coords.x}, {coords.y})";
    }
}
