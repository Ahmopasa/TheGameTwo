using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // Player Data
    public int currentHealthAmount = 3, maxHealthAmount = 3, currentCoinsAmount = 1;

    public PlayerData(CharacterTracker inGamePlayerData)
    {
        this.currentHealthAmount = inGamePlayerData.currentHealth;
        this.maxHealthAmount = inGamePlayerData.maxHealth;
        this.currentCoinsAmount = inGamePlayerData.currentCoins;
    }
}
