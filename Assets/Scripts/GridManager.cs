using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unityGridSize;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Material walkableColor;
    [SerializeField] Material obstructedColor;


    public int UnityGridSize { get { return unityGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                float walkChance = Random.Range(0.0f, 1.0f);
                bool walkable = false;
                Material color = obstructedColor;

                Vector2Int coords = new Vector2Int(x, y);

                if (walkChance > 0.25f)
                {
                    walkable = true;
                    color = walkableColor;
                }

                grid.Add(coords, new Node(tilePrefab, coords, walkable, color));
            }
        }
    }
}
