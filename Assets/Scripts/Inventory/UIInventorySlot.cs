using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    public delegate void EventHandler(ItemData data, int num);
    public static event EventHandler OnItemCompleteRemove;

    [SerializeField] private GameObject delButton;
    
    private InventoryItem curItem = null;
    public Image icon;
    public TextMeshProUGUI stackSize;

    public void ShowDeleteButton()
    {
        delButton.SetActive(true);
    }

    public void CompleteRemoveItem()
     {
        if (curItem != null)
            OnItemCompleteRemove?.Invoke(curItem.itemData, curItem.stackSize);
        delButton.SetActive(false);
    }
    public void ClearSlot()
    {
        icon.enabled = false;
        stackSize.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        curItem = item;
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
