using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TILE_TYPES
    {
        TILE_CLEAR = 0,
        TILE_DOWN_LEFT,
        TILE_DOWN_RIGHT,
        TILE_UP_LEFT,
        TILE_UP_RIGHT,
        TILE_HORIZONTAL,
        TILE_VERTICAL,
        TILE_COUNT
    };


    public Tile[] upNeighbors;
    public Tile[] rightNeighbors;
    public Tile[] downNeighbors;
    public Tile[] leftNeighbors;
}
