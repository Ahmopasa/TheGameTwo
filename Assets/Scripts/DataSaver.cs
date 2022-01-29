﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
}