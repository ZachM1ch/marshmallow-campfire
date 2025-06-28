using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Values : MonoBehaviour
{

}

/// <summary>
/// Class for holding and working with the Size Enum
/// </summary>
public class TextSizes
{
    enum Size
    {
        Tiny = 20,
        Small = 30,
        Normal = 40,
        Large = 50,
        Huge = 60
    }

    /// <summary>
    /// Returns the relevant Size Enum value for a given string
    /// </summary>
    /// <param name="xSize"> The size string taken from parsed XML </param>
    /// <returns> An int font size from Size Enum </returns>
    public static int GetSize(string xSize)
    {
        Size outSize;
        if (Enum.TryParse(xSize, out outSize))
        {
            return (int)outSize;
        }
        // Contingency in case it fails for any reason
        else
        {
            return (int)Size.Normal;
        }
    }
}
