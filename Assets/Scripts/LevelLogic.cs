using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelLogic : MonoBehaviour
{
    public int totalEnemyCount;
    
    [SerializeField] private TextMeshProUGUI gameOverTitle;
    [SerializeField] private Player player;

    private void Awake()
    {
        gameOverTitle.text = "GameOver";
    }

    public void MinusEnemyCount()
    {
        --totalEnemyCount;

        if (totalEnemyCount <= 0)
        {
            gameOverTitle.text = "You win";
            player.Win();
        }
    }
}
