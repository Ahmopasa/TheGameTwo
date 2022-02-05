using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataTracker : MonoBehaviour
{
    public static CharacterDataTracker instance;

    public GameObject saveAnnouncer;

    public PlayerData playerData;
    // public PlayerGunData playergunData;

    private void Awake()
    {
        instance = this;

        LoadPlayerData();
    }


    void Update()
    {
        UpdatePlayerStats();
    }

    private void UpdatePlayerStats()
    {
        playerData.currentHealth = PlayerHealthController.instance.currentHealth;
        playerData.maxHealth = PlayerHealthController.instance.maxHealth;
        playerData.currentCoins = LevelManager.instance.currentCoins;

        playerData.playerPosition = PlayerController.instance.transform.position;
    }

    public void SavePlayerData()
    {
        DataSaver.SavePlayerData(this.playerData);
    }

    public void LoadPlayerData()
    {
        // Load back the values.
        playerData = DataSaver.LoadPlayerData();
        if (playerData != null)
        {
            PlayerHealthController.instance.currentHealth = playerData.currentHealth;
            PlayerHealthController.instance.maxHealth = playerData.maxHealth;
            LevelManager.instance.currentCoins = playerData.currentCoins;
            LevelManager.instance.startPoint.position = playerData.playerPosition;
        }
        else
        {
            SavePlayerData();
        }
    }

    private void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.CompareTag("Player"))
        {
            SavePlayerData();

            saveAnnouncer.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D otherObj)
    {
        if (otherObj.CompareTag("Player"))
        {
            saveAnnouncer.SetActive(false);
        }
    }
}

[System.Serializable]
public class PlayerData
{
    // Player Data
    public int currentHealth = 3;
    public int maxHealth = 3;
    public int currentCoins = 1;

    public Vector3 playerPosition;
}
