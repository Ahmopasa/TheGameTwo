using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataTracker : MonoBehaviour
{
    public static CharacterDataTracker instance;

    public GameObject saveAnnouncer;

    [HideInInspector]
    public int currentHealth, maxHealth, currentCoins;

    private void Awake()
    {
        instance = this;

        LoadPlayerData();
    }

    void Update()
    {
        updatePlayerStats();
    }

    private void updatePlayerStats()
    {
        currentHealth = PlayerHealthController.instance.currentHealth;
        maxHealth = PlayerHealthController.instance.maxHealth;
        currentCoins = LevelManager.instance.currentCoins;
    }

    public void SavePlayerData()
    {
        DataSaver.SavePlayerData(this);
    }

    public void LoadPlayerData()
    {
        // Load back the values.
        PlayerData playerData = DataSaver.LoadPlayerData();
        if (playerData != null)
        {
            this.currentHealth = playerData.currentHealthAmount;
            this.maxHealth = playerData.maxHealthAmount;
            this.currentCoins = playerData.currentCoinsAmount;
        }
        else
        {
            SavePlayerData();
            LoadPlayerData();
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
        saveAnnouncer.SetActive(false);
    }
}
