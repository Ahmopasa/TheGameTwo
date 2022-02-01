using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;

public static class DataSaver
{
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
        BinaryFormatter binaryFormatter = new BinaryFormatter(); // We'll use this to format our in game setting values.
        string filePath = Application.persistentDataPath + "/playerData.settings"; // The file path where we'll create our save files.
        FileStream fileHandler = new FileStream(filePath, FileMode.Create); // Our file handler.

        PlayerData inGameSettings = new PlayerData(playerData); // We loaded our values onto the carrier class obj.

        binaryFormatter.Serialize(fileHandler, inGameSettings); // We converted our data into binary data.
        fileHandler.Close(); // End the process.
    }

    public static PlayerData LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/playerData.settings";
        if (File.Exists(filePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileHandler = new FileStream(filePath, FileMode.Open);

            PlayerData inGameSettings = binaryFormatter.Deserialize(fileHandler) as PlayerData;
            
            fileHandler.Close();

            return inGameSettings;
        }
        else
        {
            Debug.LogError("Save-file not found in => " + filePath);

            return null;
        }
    }

    public static void SavePlayerGuns( )
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter(); // We'll use this to format our in game setting values.
        string filePath = Application.persistentDataPath + "/playerGunData.settings"; // The file path where we'll create our save files.
        FileStream fileHandler = new FileStream(filePath, FileMode.Create); // Our file handler.


    }
}
