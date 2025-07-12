using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    GameObject tile;
    public Vector2Int coords;
    public bool isWalkable;
    public Material color;

    public Node(GameObject tilePrefab, Vector2Int coords, bool isWalkable, Material color)
    {
        this.coords = coords;

        this.isWalkable = isWalkable;

        this.color = color;

        tile = tilePrefab;

        SpawnTile();
    }

    private void SpawnTile()
    {
        tile.GetComponentInChildren<Renderer>().material = color;
        Instantiate(tile, new Vector3(coords.x, 0, coords.y), Quaternion.identity);
    }
}
