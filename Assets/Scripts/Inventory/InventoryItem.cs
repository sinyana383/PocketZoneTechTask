using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int stackSize;

    public InventoryItem(ItemData item, int num)
    {
        itemData = item;
        AddToStack(num);
    }

    public void AddToStack(int num)
    {
        stackSize += num;
    }
    
    public void RemoveFromStack(int num)
    {
        stackSize -= num;
    }
}
