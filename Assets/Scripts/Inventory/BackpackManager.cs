using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    [SerializeField] private InventoryGrid m_inventoryGrid;

    private bool m_isHoveringInventory = false;
    private int m_oldHighlightedSlot = -1;
    private int m_currHighlightedSlot = -1;

    private void Awake()
    {
        m_inventoryGrid.Init();
    }

    private void Update()
    {
        m_isHoveringInventory = false;

        CheckMousePositionOnGrid();

        if (!m_inventoryGrid.AreSlotsConnected(m_oldHighlightedSlot, m_currHighlightedSlot))
        {
            InventorySlot currSlot = m_inventoryGrid.GetSlot(m_oldHighlightedSlot);
            if (currSlot != null)
            {
                currSlot.OnDehighlight();
            }
        }

        if (m_isHoveringInventory)
        {
            InventorySlot currSlot = m_inventoryGrid.GetSlot(m_currHighlightedSlot);
            if (currSlot != null)
            {
                currSlot.OnHighlight();
            }
        }

        m_oldHighlightedSlot = m_currHighlightedSlot;
    }

    private void CheckMousePositionOnGrid()
    {
        Vector3 mousePos = Input.mousePosition;
        int columnsCount = m_inventoryGrid.GetInventoryColumns();
        int rowsCount = m_inventoryGrid.GetInventoryRows();
        float gridSpacing = m_inventoryGrid.GetSpacing();
        float gridSlotSize = m_inventoryGrid.GetSlotSize();

        RectTransform gridSlotsParentRT = m_inventoryGrid.GetGridSlotsParentObj().GetComponent<RectTransform>();
        if (gridSlotsParentRT == null)
        {
            Debug.LogError("Could not get RectTransform from m_gridSlotsParentObj!");
            return;
        }

        m_isHoveringInventory = RectTransformUtility.RectangleContainsScreenPoint(
            gridSlotsParentRT,
            mousePos,
            null
            );

        if (!m_isHoveringInventory)
        {
            return;
        }

        Vector2 localMousePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            gridSlotsParentRT,
            mousePos,
            null,
            out localMousePos
            );

        float gridWidthWithoutMargin = gridSlotsParentRT.rect.width - gridSpacing * 2;
        float gridHeightWithoutMargin = gridSlotsParentRT.rect.height - gridSpacing * 2;

        // Local position to top-left origin
        Vector2 originTopLeft = new Vector2(
            -gridSlotsParentRT.rect.width / 2.0f,
            gridSlotsParentRT.rect.height / 2.0f
            );

        // Offset mouse position by origin
        localMousePos -= originTopLeft;

        // Check if mouse is inside grid (excluding outer margin)
        if (localMousePos.x < gridSpacing || localMousePos.x > (gridSlotsParentRT.rect.width - gridSpacing) ||
            localMousePos.y > (-gridSpacing) || localMousePos.y < (-gridSlotsParentRT.rect.height + gridSpacing))
        {
            m_isHoveringInventory = false;
            return;
        }

        float halfSpacing = gridSpacing / 2.0f;

        float adjustedMousePosX = localMousePos.x - halfSpacing;
        float adjustedMousePosY = -localMousePos.y - halfSpacing;

        int column = Mathf.FloorToInt(adjustedMousePosX / (gridSlotSize + gridSpacing));
        int row = Mathf.FloorToInt(adjustedMousePosY / (gridSlotSize + gridSpacing));

        if (column < 0 || column >= columnsCount ||
            row < 0 || row >= rowsCount)
        {
            // outside bounds - likely hovering over the left or bottom spacing area
            m_isHoveringInventory = false;
            return;
        }

        m_currHighlightedSlot = m_inventoryGrid.GetSlotIndex(row, column);
    }

}
