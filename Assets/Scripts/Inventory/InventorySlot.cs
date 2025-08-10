using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image m_image;
    [SerializeField] private Color m_defaultColor = Color.white;

    public void Refresh()
    {
        // Set values to default
        m_image.color = m_defaultColor;
    }

    public void OnHighlight()
    {
        m_image.color = Color.black;
    }

    public void OnDehighlight()
    {
        m_image.color = m_defaultColor;
    }

}
