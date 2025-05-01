using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public static bool isNewGame = true;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // prevent duplicates
        }
    }
    public static void Save(GameSaveData data)
    {
        string fileName = "savegame.json";
        string path = Application.persistentDataPath + fileName;
        string json = JsonUtility.ToJson(data, true); // pretty print

        File.WriteAllText(path, json);
        Debug.Log($"Game saved to {path}");
    }

    public static GameSaveData Load()
    {
        string fileName = "savegame.json";
        string path = Application.persistentDataPath + fileName;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);
            Debug.Log("Game loaded successfully.");
            return data;
        }
        else
        {
            Debug.Log("No save file found. Creating new save data.");
            return new GameSaveData(); // Return fresh data if no file
        }
    }
}
