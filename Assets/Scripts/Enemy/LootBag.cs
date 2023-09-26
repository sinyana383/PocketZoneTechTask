using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootBag : MonoBehaviour
{
    public List<GameObject> lootObjects = new List<GameObject>();

    GameObject GetDroppedItem()
    {
        int random = Random.Range(1, 100);
        
        GameObject randItem = null;
        int randItemDropChabce = 101;
        foreach (var item in lootObjects)
        {
            int dropChance = item.GetComponent<ICollectible>().GetItemData().dropChance;
            if (random <= dropChance && 
                (randItem == null || dropChance < randItemDropChabce))
            {
                randItem = item;
                randItemDropChabce = randItem.GetComponent<ICollectible>().GetItemData().dropChance;
            }
        }

        return randItem;
    }

    public void InstantiateLoot(Vector3 dropPos)
    {
        GameObject droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItem,
                dropPos, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = 
                droppedItem.GetComponent<ICollectible>().GetItemData().icon;
        }
    }
}
