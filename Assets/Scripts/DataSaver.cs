using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;

public static class DataSaver
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Save/";
    public static void SaveData(InGameSettings settings)
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        File.WriteAllText(SAVE_FOLDER + "gameSettings.json", JsonUtility.ToJson(settings));
    }

    public static InGameSettings LoadData()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        if (File.Exists(SAVE_FOLDER + "gameSettings.json"))
        {
            InGameSettings m_playerData = JsonUtility.FromJson<InGameSettings>(File.ReadAllText(SAVE_FOLDER + "gameSettings.json"));
            if (m_playerData == null)
            {
                return null;
            }
            else
            {
                return m_playerData;
            }
        }
        return null;
    }

    public static void SavePlayerData(PlayerData playerData)
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        File.WriteAllText(SAVE_FOLDER + "playerData.json", JsonUtility.ToJson(playerData));
    }

    public static PlayerData LoadPlayerData()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        if (File.Exists(SAVE_FOLDER + "playerData.json"))
        {
            PlayerData m_playerData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(SAVE_FOLDER + "playerData.json"));
            if (m_playerData == null)
            {
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
