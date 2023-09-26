using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDataSessions
{
    
    public static void SaveData(GameData gameData, string file)
    {
        string json = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + file;
        System.IO.File.WriteAllText(filePath, json);
    }
    
    public static void LoadData(GameData gameData, string file)
    {
        string filePath = Application.persistentDataPath + file;

        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            GameData loadedGameData = JsonUtility.FromJson<GameData>(json);

            gameData.playerDeathCount = loadedGameData.playerDeathCount;
        }
        else
        {
            gameData.playerDeathCount = 0;
        }

    }

    // potential called on button click 
    public static void ClearData(string file)
    {
        GameData defaultData = new GameData();
        string json = JsonUtility.ToJson(defaultData);
        string filePath = Application.persistentDataPath + file;
        
        System.IO.File.WriteAllText(filePath, json);
    }
}
