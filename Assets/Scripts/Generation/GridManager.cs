using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int gridWidth;
    [SerializeField] int gridHeight;

    Dictionary<Vector2Int, GameObject> gridOfNodes = new Dictionary<Vector2Int, GameObject>();
    WaveFunction generator;
    public Dictionary<Vector2Int, GameObject> Grid { get { return gridOfNodes; } }
    public Tile[] tileObjects;
    public Cell cellObj;


    private void Awake()
    {
        generator = gameObject.AddComponent<WaveFunction>();
        generator.Initialize(gridWidth, gridHeight, this, tileObjects);
        

        generator.InitializeGrid();
    }


    public void AddTile(Tile tile)
    {
        foreach(Node n in tile.nodes)
        {
            Vector2Int coords = new Vector2Int((int)n.transform.position.x, (int)n.transform.position.z);
            gridOfNodes.Add(coords, n.gameObject);
        }
    }


}
