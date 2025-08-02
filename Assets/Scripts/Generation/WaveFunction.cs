using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveFunction : MonoBehaviour
{
    public List<Cell> gridComponents;

    private GridManager gridManager;
    private Tile[] tileObjects;
    private int iter = 0;
    private int gridWidth;
    private int gridHeight;

    public void Initialize(int width, int height, GridManager gridMan, Tile[] tiles)
    {
        gridComponents = new List<Cell>();
        gridManager = gridMan;
        tileObjects = tiles;

        gridWidth = width * 3;
        gridHeight = height * 3; 

    }

    public void InitializeGrid()
    {
        List<Tile> clearTile = new List<Tile>(1) { tileObjects[(int)Tile.TILE_TYPES.TILE_CLEAR] };

        // create a start and end point
        int startPointZ = (int) Math.Ceiling(UnityEngine.Random.Range(0, gridHeight) / 3f) * 3;
        
        Cell startCell = Instantiate(gridManager.cellObj, new Vector3(0, 0f, startPointZ), Quaternion.identity, gridManager.gameObject.transform);
        startCell.gameObject.name = "StartCell";
        startCell.CreateCell(false, clearTile);
        gridComponents.Add(startCell);

        int endPointZ = (int)Math.Ceiling(UnityEngine.Random.Range(0, gridHeight) / 3f) * 3;

        Cell endCell = Instantiate(gridManager.cellObj, new Vector3(gridWidth - 3, 0f, endPointZ), Quaternion.identity, gridManager.gameObject.transform);
        endCell.gameObject.name = "EndCell";
        endCell.CreateCell(false, clearTile);
        gridComponents.Add(endCell);

        for (int z = 0; z < gridHeight; z += 3)
        {
            for (int x = 0; x < gridWidth; x += 3)
            {   
                // dont recreate the start and end cell
                if((x == 0 && z == startPointZ) || (x == gridWidth - 3 && z == endPointZ))
                    continue;

                Cell newCell = Instantiate(gridManager.cellObj, new Vector3(x, 0f, z), Quaternion.identity, gridManager.gameObject.transform);
                newCell.CreateCell(false, tileObjects.ToList<Tile>());

                gridComponents.Add(newCell);
            }
        }

        StartCoroutine(CheckEntropy());
    }

    IEnumerator CheckEntropy()
    {
        List<Cell> tempGrid = new List<Cell>(gridComponents);

        tempGrid.RemoveAll(c => c.collapsed);

        tempGrid.Sort((a, b) => { return a.tileOptions.Count - b.tileOptions.Count; });

        int arrLength = tempGrid[0].tileOptions.Count;
        int stopIndex = 0;

        for (int i = 1; i < tempGrid.Count; i++)
        {
            if (tempGrid[i].tileOptions.Count > arrLength)
            {
                stopIndex = i;
                break;
            }
        }

        if (stopIndex > 0)
            tempGrid.RemoveRange(stopIndex, tempGrid.Count - stopIndex);

        yield return new WaitForSeconds(0.01f);

        CollapseCell(tempGrid);
    }

    void CollapseCell(List<Cell> tempGrid)
    {
        Cell cellToCollapse = tempGrid[0];

        cellToCollapse.collapsed = true;

        Tile selectedTile = cellToCollapse.tileOptions[UnityEngine.Random.Range(0, cellToCollapse.tileOptions.Count)];
        cellToCollapse.tileOptions = new List<Tile>() { selectedTile };

        Tile foundTile = cellToCollapse.tileOptions[0];
        gridManager.AddTile(Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity, cellToCollapse.transform));

        UpdateGeneration();
    }


    void UpdateGeneration()
    {
        List<Cell> newGenCell = new List<Cell>(gridComponents);

        int arrayDimensionsWidth = gridWidth / 3;
        int arrayDimensionsHeight = gridHeight / 3;

        for (int z = 0; z < arrayDimensionsHeight; z++)
        {
            for (int x = 0; x < arrayDimensionsWidth; x++)
            {
                var index = x + z * (arrayDimensionsWidth);

                if(gridComponents[index].collapsed)
                {
                    Debug.Log("called");
                    newGenCell[index] = gridComponents[index];
                }
                else
                {
                    List<Tile> options = new List<Tile>();

                    foreach(Tile t in tileObjects)
                    {
                        options.Add(t);
                    }

                    // update up
                    if (z > 0)
                    {
                        Cell up = gridComponents[x + (z - 1) * arrayDimensionsWidth];
                        List<Tile> validOptions = new List<Tile>();

                        foreach(Tile possibleOptions in up.tileOptions)
                        {
                            int valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            Tile[] valid = tileObjects[valOption].upNeighbors;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    // update right
                    if (x < arrayDimensionsWidth - 1)
                    {
                        Cell right = gridComponents[x + 1 + z * arrayDimensionsWidth];
                        List<Tile> validOptions = new List<Tile>();

                        foreach (Tile possibleOptions in right.tileOptions)
                        {
                            int valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            Tile[] valid = tileObjects[valOption].leftNeighbors;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    // update down
                    if (z < arrayDimensionsHeight - 1)
                    {
                        Cell down = gridComponents[x + (z + 1) * arrayDimensionsWidth];
                        List<Tile> validOptions = new List<Tile>();

                        foreach (Tile possibleOptions in down.tileOptions)
                        {
                            int valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            Tile[] valid = tileObjects[valOption].downNeighbors;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    // update left
                    if (x > 0)
                    {
                        Cell left = gridComponents[x - 1 + z * arrayDimensionsWidth];
                        List<Tile> validOptions = new List<Tile>();

                        foreach (Tile possibleOptions in left.tileOptions)
                        {
                            int valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            Tile[] valid = tileObjects[valOption].rightNeighbors;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    List<Tile> newTileList = new List<Tile>();

                    for(int i = 0; i < options.Count; i++)
                    {
                        newTileList.Add(options[i]);
                    }

                    newGenCell[index].RecreateCell(newTileList);
                }
            }
        }

        gridComponents = newGenCell;
        iter++;

        if (iter < arrayDimensionsWidth * arrayDimensionsHeight)
        {
            StartCoroutine(CheckEntropy());
        }
    }

    void CheckValidity(List<Tile> optionList, List<Tile> validOption)
    {
        for(int x = optionList.Count - 1; x >= 0; x--)
        {
            var element = optionList[x];
            if(!validOption.Contains(element))
            {
                optionList.RemoveAt(x);
            }
        }
    }

}
