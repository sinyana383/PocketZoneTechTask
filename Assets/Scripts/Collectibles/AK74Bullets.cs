using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class AK74Bullets : MonoBehaviour, ICollectible
{
    public static event HandleAk74BulletsCollected OnAk74BulletsCollected;
    public delegate void HandleAk74BulletsCollected(ItemData itemData);

    public ItemData ak74BulletsData;
    public void Collect()
    {
        Destroy(gameObject);
        OnAk74BulletsCollected?.Invoke(ak74BulletsData);
    }
}
