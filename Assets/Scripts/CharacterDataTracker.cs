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

        Debug.Log("Array : " + playerData.listOfGuns.Length);

        LoadPlayerData();
    }

    private void Start()
    {
        playerData.listOfGuns = new PlayerGunData[7];

        for (int i = 0; i < PlayerController.instance.availableGuns.Count; i++)
        {
            PlayerGunData playerGunData = new PlayerGunData();
            playerGunData.bulletToFire = PlayerController.instance.availableGuns[i].bulletToFire;
            playerGunData.thePosition = PlayerController.instance.availableGuns[i].firePoint.position;
            playerGunData.theRotation = PlayerController.instance.availableGuns[i].firePoint.rotation;
            playerGunData.timeBetweenShots = PlayerController.instance.availableGuns[i].timeBetweenShots;
            playerGunData.shotCounter = PlayerController.instance.availableGuns[i].shotCounter;
            playerGunData.weaponName = PlayerController.instance.availableGuns[i].weaponName;
            playerGunData.gunUI = PlayerController.instance.availableGuns[i].gunUI;
            playerGunData.itemCost = PlayerController.instance.availableGuns[i].itemCost;
            playerGunData.gunShopSprite = PlayerController.instance.availableGuns[i].gunShopSprite;
            playerData.listOfGuns[i] = playerGunData;
        }
    }

    void Update()
    {
        UpdatePlayerStats();

        UpdatePlayerGunList();
    }

    private void UpdatePlayerStats()
    {
        playerData.currentHealth = PlayerHealthController.instance.currentHealth;
        playerData.maxHealth = PlayerHealthController.instance.maxHealth;
        playerData.currentCoins = LevelManager.instance.currentCoins;

        playerData.playerPosition = PlayerController.instance.transform.position;
    }

    private void UpdatePlayerGunList()
    {
        for (int i = 0; i < PlayerController.instance.availableGuns.Count; i++)
        {
            PlayerGunData playerGunData = new PlayerGunData();
            playerGunData.bulletToFire = PlayerController.instance.availableGuns[i].bulletToFire;
            playerGunData.thePosition = PlayerController.instance.availableGuns[i].firePoint.position;
            playerGunData.theRotation = PlayerController.instance.availableGuns[i].firePoint.rotation;
            playerGunData.timeBetweenShots = PlayerController.instance.availableGuns[i].timeBetweenShots;
            playerGunData.shotCounter = PlayerController.instance.availableGuns[i].shotCounter;
            playerGunData.weaponName = PlayerController.instance.availableGuns[i].weaponName;
            playerGunData.gunUI = PlayerController.instance.availableGuns[i].gunUI;
            playerGunData.itemCost = PlayerController.instance.availableGuns[i].itemCost;
            playerGunData.gunShopSprite = PlayerController.instance.availableGuns[i].gunShopSprite;
            playerData.listOfGuns[i] = playerGunData;
        }
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
  
    public PlayerGunData[] listOfGuns;
}

[System.Serializable]
public class PlayerGunData
{
    public GameObject bulletToFire;
    // public Transform firePoint;
    public Vector3 thePosition;
    public Quaternion theRotation;

    public float timeBetweenShots;
    public float shotCounter;

    public string weaponName;
    public Sprite gunUI;

    public int itemCost;
    public Sprite gunShopSprite;
}
