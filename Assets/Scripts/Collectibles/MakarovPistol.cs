using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakarovPistol : MonoBehaviour, ICollectible
{
    public static event HandleMakarovPistolCollected OnMakarovPistolCollected;
    public delegate void HandleMakarovPistolCollected(ItemData itemData);

    public ItemData makarovPistolData;
    public void Collect()
    {
        Destroy(gameObject);
        OnMakarovPistolCollected?.Invoke(makarovPistolData);
    }
}
