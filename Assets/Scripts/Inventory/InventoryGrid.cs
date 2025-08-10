using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryGrid : MonoBehaviour
{
    [SerializeField] private BackpackManager m_backpackManager;
    [SerializeField] private GameObject m_inventorySlotPrefab;
    [SerializeField] private GameObject m_backpackGridObj;
    [SerializeField] private GameObject m_gridSlotsParentObj;
    [SerializeField] private int m_inventoryRows = 0;
    [SerializeField] private int m_inventoryColumns = 0;
    [SerializeField] private float m_spacing = 0.0f;
    [SerializeField] private float m_slotSize = 0.0f;

    private List<GameObject> m_inventorySlots = new List<GameObject>();

    public void Init()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        ClearGrid();

        for (int rowIndex = 0; rowIndex < m_inventoryRows; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < m_inventoryColumns; columnIndex++)
            {
                GameObject newSlot = Instantiate(m_inventorySlotPrefab, m_gridSlotsParentObj.transform);
                newSlot.name = $"{m_inventorySlotPrefab.name}_R{rowIndex}_C{columnIndex}";  // R = row, C = column
                
                RectTransform rectTransform = newSlot.GetComponent<RectTransform>();

                if (rectTransform != null)
                {
                    rectTransform.anchorMin = new Vector2(0, 1);
                    rectTransform.anchorMax = new Vector2(0, 1);
                    rectTransform.pivot = new Vector2(0, 1);
                    rectTransform.sizeDelta = new Vector2(m_slotSize, m_slotSize);

                    float xPos = columnIndex * (m_slotSize + m_spacing);
                    float yPos = -rowIndex * (m_slotSize + m_spacing);

                    // Spacing for top and left side
                    xPos += m_spacing;
                    yPos -= m_spacing;

                    rectTransform.anchoredPosition = new Vector2(xPos, yPos);
                }

                m_inventorySlots.Add(newSlot);
            }
        }

        RectTransform gridParentRT = m_gridSlotsParentObj.GetComponent<RectTransform>();

        if (gridParentRT != null)
        {
            float gridWidth = m_inventoryColumns * (m_slotSize + m_spacing);
            float gridHeight = m_inventoryRows * (m_slotSize + m_spacing);
            gridWidth += m_spacing;
            gridHeight += m_spacing;
            gridParentRT.sizeDelta = new Vector2(gridWidth, gridHeight);
        }
    }

    public void ClearGrid()
    {
        foreach (GameObject slot in m_inventorySlots)
        {
            Destroy(slot);
        }

        m_inventorySlots.Clear();
    }

    public void RefreshGrid()
    {
        for (int i = 0; i < m_inventorySlots.Count; i++)
        {
            InventorySlot currSlot = m_inventorySlots[i].GetComponent<InventorySlot>();
            if (currSlot != null)
            {
                currSlot.Refresh();
            }
        }
    }

    public int GetSlotIndex(int _row, int _column)
    {
        int slotIndex = _row * m_inventoryColumns + _column;

        if (slotIndex < 0 || slotIndex >= m_inventorySlots.Count)
        {
            return -1;
        }

        return slotIndex;
    }

    public InventorySlot GetSlot(int _row, int _column)
    {
        int slotIndex = GetSlotIndex(_row, _column);
        return GetSlot(slotIndex);
    }

    public InventorySlot GetSlot(int _slotIndex)
    {
        if (_slotIndex < 0 || _slotIndex >= m_inventorySlots.Count)
        {
            return null;
        }

        return m_inventorySlots[_slotIndex].GetComponent<InventorySlot>();
    }

    /// <summary>
    /// Checks if slots are connected, meaning they share an inventory item. For example, a sword that covers two slots will return true if both those slots are passed in
    /// </summary>
    /// <param name="_slotIndex1"></param>
    /// <param name="_slotIndex2"></param>
    /// <returns></returns>
    public bool AreSlotsConnected(int _slotIndex1, int _slotIndex2)
    {
        // TODO: Implement code to see if slots are connected
        return false;
    }

    public GameObject GetBackpackGridObj()
    {
        return m_backpackGridObj;
    }

    public GameObject GetGridSlotsParentObj()
    {
        return m_gridSlotsParentObj;
    }

    public int GetInventoryRows()
    {
        return m_inventoryRows;
    }

    public int GetInventoryColumns()
    {
        return m_inventoryColumns;
    }

    public float GetSlotSize()
    {
        return m_slotSize;
    }

    public float GetSpacing()
    {
        return m_spacing;
    }

}
