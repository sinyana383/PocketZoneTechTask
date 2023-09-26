using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI textMeshProText;
    [FormerlySerializedAs("healthBar")] [SerializeField] private UIHealthBar uiHealthBar;
    [SerializeField] private float health, maxHealth = 50f;

    GameData gameData = new GameData();
    private void Awake()
    {
        LoadData();
        uiHealthBar = GetComponentInChildren<UIHealthBar>();
        gameOverScreen.SetActive(false);
    }

    private void Start()
    {
        textMeshProText.text = "Total Deaths: " + gameData.playerDeathCount.ToString();
        Time.timeScale = 1;
        health = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        uiHealthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Gameover();
        }
    }

    private void Gameover()
    {
        gameData.playerDeathCount += 1;
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        SaveData();
    }
    
    public void Win()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        SaveData();
    }

    void SaveData()
    {
        string json = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + "/gameData.json";
        System.IO.File.WriteAllText(filePath, json);
    }
    
    private void LoadData()
    {
        string filePath = Application.persistentDataPath + "/gameData.json";

        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            GameData loadedGameData = JsonUtility.FromJson<GameData>(json);

            gameData.playerDeathCount = loadedGameData.playerDeathCount;
            // Use deathCount as needed (e.g., display it on a UI element)
        }
        else
        {
            // If the file doesn't exist, initialize deathCount to 0
            gameData.playerDeathCount = 0;
            // ...
        }

    }

    void ClearData()
    {
        // Create an empty or default GameData object
        GameData defaultData = new GameData();
    
        // Serialize the default data to JSON
        string json = JsonUtility.ToJson(defaultData);

        // Specify the file path
        string filePath = Application.persistentDataPath + "/gameData.json";

        // Overwrite the existing file with the default data
        System.IO.File.WriteAllText(filePath, json);
    }
    
}
