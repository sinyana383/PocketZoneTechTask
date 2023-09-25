using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // When smth happens with inventory it invokes ui changes
    public static event Action<List<InventoryItem>> OnInventoryChange;
    
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    private void OnEnable()
    {
        AK74Bullets.OnAk74BulletsCollected += Add;
        MakarovPistol.OnMakarovPistolCollected += Add;
    }

    private void OnDisable()
    {
        // On disable Add() wouldn't be invoked 
        AK74Bullets.OnAk74BulletsCollected -= Add;
        MakarovPistol.OnMakarovPistolCollected -= Add;
    }

    public void Add(ItemData itemData, int number)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem invItem))
        {
            invItem.AddToStack(number);
            Debug.Log($"{invItem.itemData.displayName} total stack is now {invItem.stackSize}");
            OnInventoryChange?.Invoke(inventory);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData, number);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            Debug.Log($"Added {itemData.displayName} to the inventory");
            OnInventoryChange?.Invoke(inventory);
        }
    }

    public void Remove(ItemData itemData, int number)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem invItem))
        {
            invItem.RemoveFromStack(number);
            if (invItem.stackSize == 0)
            {
                inventory.Remove(invItem);
                itemDictionary.Remove(itemData);
            }
            OnInventoryChange?.Invoke(inventory);
        }

    }
    
}
