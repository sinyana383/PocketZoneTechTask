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
            // Use deathCount as needed (e.g., display it on a UI element)
        }
        else
        {
            // If the file doesn't exist, initialize deathCount to 0
            gameData.playerDeathCount = 0;
            // ...
        }

    }

    public static void ClearData(string file)
    {
        // Create an empty or default GameData object
        GameData defaultData = new GameData();
    
        // Serialize the default data to JSON
        string json = JsonUtility.ToJson(defaultData);

        // Specify the file path
        string filePath = Application.persistentDataPath + file;

        // Overwrite the existing file with the default data
        System.IO.File.WriteAllText(filePath, json);
    }
}
