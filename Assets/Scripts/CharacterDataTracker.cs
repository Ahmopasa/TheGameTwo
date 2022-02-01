using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataTracker : MonoBehaviour
{
    public static CharacterDataTracker instance;

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
        var loadedObj = DataSaver.LoadPlayerData();
        if (loadedObj != null)
        {
            this.currentHealth = loadedObj.currentHealth;
            this.maxHealth = loadedObj.maxHealth;
            this.currentCoins = loadedObj.currentCoins;
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
        }
    }


}
