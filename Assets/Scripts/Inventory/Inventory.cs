using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChange;
    public static event EventHandler OnInventoryChangeToBullets;
    public delegate void EventHandler(bool state);
    
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    private void OnEnable()
    {
        AK74Bullets.OnAk74BulletsCollected += Add;
        MakarovPistol.OnMakarovPistolCollected += Add;
        PlayerAimWeapon.OnShootBullets += Remove;
        UIInventorySlot.OnItemCompleteRemove += CompleteRemove;
    }

    private void OnDisable()
    {
        AK74Bullets.OnAk74BulletsCollected -= Add;
        MakarovPistol.OnMakarovPistolCollected -= Add;
        PlayerAimWeapon.OnShootBullets -= Remove;
        UIInventorySlot.OnItemCompleteRemove -= CompleteRemove;
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
            if (itemData.isBullet)
                OnInventoryChangeToBullets?.Invoke(true);
        }
    }

    public void Remove(ItemData itemData, int number)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem invItem))
        {
            invItem.RemoveFromStack(number);
            if (invItem.stackSize <= 0)
            {
                inventory.Remove(invItem);
                itemDictionary.Remove(itemData);
                if (itemData.isBullet)
                    OnInventoryChangeToBullets?.Invoke(false);
            }
            OnInventoryChange?.Invoke(inventory);
        }

    }

    public void CompleteRemove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem invItem))
        {
            inventory.Remove(invItem);
            itemDictionary.Remove(itemData);
            if (itemData.isBullet)
                OnInventoryChangeToBullets?.Invoke(false);
            OnInventoryChange?.Invoke(inventory);
        }
    }
    
}
