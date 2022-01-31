using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    public static CharacterTracker instance;

    public int currentHealth, maxHealth, currentCoins;

    private void Awake()
    {
        instance = this;

        LoadPlayerData();
    }

    void Update()
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
        if (otherObj.tag == "Player")
        {
            SavePlayerData();
        }
    }
}
