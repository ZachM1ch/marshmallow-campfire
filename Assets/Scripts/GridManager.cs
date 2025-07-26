using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unityGridSize;
    [SerializeField] GameObject tilePrefab;


    public int UnityGridSize { get { return unityGridSize; } }

    Dictionary<Vector2Int, GameObject> grid = new Dictionary<Vector2Int, GameObject>();
    public Dictionary<Vector2Int, GameObject> Grid { get { return grid; } }

    private void Awake()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                float walkChance = Random.Range(0.0f, 1.0f);
                bool walkable = false;

                Vector2Int coords = new Vector2Int(x, y);

                if ((x == 0 && y == 0) || walkChance > 0.25f)
                {
                    walkable = true;
                }

                grid.Add(coords, Instantiate(tilePrefab, new Vector3(coords.x, 0f, coords.y), Quaternion.identity));
                grid[coords].GetComponent<Node>().isWalkable = walkable;
            }
        }
    }
}
