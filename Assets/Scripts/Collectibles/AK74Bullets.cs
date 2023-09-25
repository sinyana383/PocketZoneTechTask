using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class AK74Bullets : MonoBehaviour, ICollectible
{
    public static event HandleAk74BulletsCollected OnAk74BulletsCollected;
    public delegate void HandleAk74BulletsCollected(ItemData itemData, int number);

    public ItemData ak74BulletsData;
    public int minNum = 20;
    public int maxNum = 100;
    public void Collect()
    {
        Destroy(gameObject);
        OnAk74BulletsCollected?.Invoke(ak74BulletsData, UnityEngine.Random.Range(minNum, maxNum + 1));
    }
}
