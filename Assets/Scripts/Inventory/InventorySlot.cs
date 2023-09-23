using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI stackSize;

    public void ClearSlot()
    {
        icon.enabled = false;
        stackSize.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }
        
        icon.enabled = true;
        stackSize.enabled = true;

        icon.sprite = item.itemData.icon;
        if (item.stackSize > 1) 
            stackSize.text = item.stackSize.ToString();
        else
            stackSize.text = string.Empty;
    }
}
