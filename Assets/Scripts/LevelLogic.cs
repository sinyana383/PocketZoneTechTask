using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelLogic : MonoBehaviour
{
    public int totalEnemyCount;
    
    [SerializeField] private TextMeshProUGUI totalDeathsText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI gameOverTitle;
    [SerializeField] private string dataSessionsFileName;

    GameData gameData = new GameData();

    private void OnEnable()
    {
        Player.OnGameOver += Gameover;
    }

    private void OnDisable()
    {
        Player.OnGameOver -= Gameover;
    }

    private void Awake()
    {
        Time.timeScale = 1;
        
        GameDataSessions.LoadData(gameData, dataSessionsFileName);
        gameOverScreen.SetActive(false);
        Debug.Log(gameData.playerDeathCount);
        totalDeathsText.text = "Total Deaths: " + gameData.playerDeathCount;
        gameOverTitle.text = "GameOver";
    }

    private void Gameover()
    {
        gameData.playerDeathCount += 1;
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        GameDataSessions.SaveData(gameData, dataSessionsFileName);
    }
    
    public void Win()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        GameDataSessions.SaveData(gameData, dataSessionsFileName);
    }
    
    public void MinusEnemyCount()
    {
        --totalEnemyCount;

        if (totalEnemyCount <= 0)
        {
            gameOverTitle.text = "You win";
            Win();
        }
    }
}
