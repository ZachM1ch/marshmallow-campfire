using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Node : MonoBehaviour
{
    public Vector2Int coords;
    public bool isWalkable = false;
    public Material walkableColor;
    public Material unwalkableColor;

    void Update()
    {
        gameObject.GetComponentInChildren<Renderer>().material = isWalkable ? walkableColor : unwalkableColor;
    }
}
