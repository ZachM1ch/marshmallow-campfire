using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool collapsed;
    public List<Tile> tileOptions;

    public void CreateCell(bool collapseState, List<Tile> tiles)
    {
        collapsed = collapseState;
        tileOptions = tiles;
    }

    public void RecreateCell(List<Tile> tiles)
    {
        tileOptions = tiles;
    }

    public void ClearTileOptions()
    {
        tileOptions.Clear();
    }

    public void AddTileOption(Tile tile)
    {
        tileOptions.Add(tile);
    }
}
