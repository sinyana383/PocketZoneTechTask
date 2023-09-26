using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIInventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<UIInventorySlot> inventorySlots = new List<UIInventorySlot>();

    [SerializeField] private int inventoryCapacity;

    private void OnEnable()
    {
        Inventory.OnInventoryChange += DrawInventory;
    }

    private void OnDisable()
    {
        // Garbage collector doesn't clean this event up, so do it here
        Inventory.OnInventoryChange -= DrawInventory;
    }

    void ResetInventory()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        inventorySlots = new List<UIInventorySlot>();
    }

    void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();

        for (int i = 0; i < inventoryCapacity; i++)
            CreateInventorySlot();

        var itemsNb = inventory.Count < inventoryCapacity ? 
            inventory.Count : inventoryCapacity;
        for (int i = 0; i < itemsNb; i++)
            inventorySlots[i].DrawSlot(inventory[i]);
    }

    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        UIInventorySlot newSlotComponent = newSlot.GetComponent<UIInventorySlot>();
        newSlotComponent.ClearSlot();
        
        inventorySlots.Add(newSlotComponent);
    }
}
