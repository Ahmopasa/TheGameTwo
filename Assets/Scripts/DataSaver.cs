using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;

public static class DataSaver
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Save/";
    public static void SaveData(Settings settings)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter(); // We'll use this to format our in game setting values.
        string filePath = Application.persistentDataPath + "/inGame.settings"; // The file path where we'll create our save files.
        FileStream fileHandler = new FileStream(filePath, FileMode.Create); // Our file handler.

        InGameSettings inGameSettings = new InGameSettings(settings); // We loaded our values onto the carrier class obj.

        binaryFormatter.Serialize(fileHandler, inGameSettings); // We converted our data into binary data.
        fileHandler.Close(); // End the process.
    }

    public static InGameSettings LoadData()
    {
        string filePath = Application.persistentDataPath + "/inGame.settings";
        if (File.Exists(filePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileHandler = new FileStream(filePath, FileMode.Open);
            InGameSettings inGameSettings = binaryFormatter.Deserialize(fileHandler) as InGameSettings;
            fileHandler.Close();

            return inGameSettings;
        }
        else
        {
            Debug.LogError("Save-file not found in => " + filePath);

            return null;
        }
    }

    public static void SavePlayerData(CharacterDataTracker playerData)
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
        string jsonString = JsonUtility.ToJson(playerData);
        File.WriteAllText(SAVE_FOLDER + "playerData.json", jsonString);
    }

    public static PlayerData LoadPlayerData()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        if (File.Exists(SAVE_FOLDER + "playerData.json"))
        {
            string jsonString = File.ReadAllText(SAVE_FOLDER + "playerData.json");

            PlayerData m_playerData = JsonUtility.FromJson<PlayerData>(jsonString);
            if (m_playerData == null)
            {
                Debug.LogError("The" + m_playerData + "is null. Returning [null].");
                return null;
            }
            else
            {
                return m_playerData;
            }
        }
        return null;
    }
}
