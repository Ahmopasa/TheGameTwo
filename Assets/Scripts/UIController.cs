﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("Player HP and Coins")]
    public Slider healthSlider;
    public Text healthText, coinText;

    [Header("Player Death Pop-Up Window")]
    public GameObject deathScreen;

    [Header("Fade Screen")]
    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack, fadeOutBlack;

    [Header("Scenes To Load")]
    public string newGameScene, mainMenuScene;

    [Header("Others")]
    public GameObject pauseMenu, mapDisplay, bigMapText;

    [Header("Weapon System")]
    public Image currentGun;
    public Text gunText;

    [Header("Boss System")]
    public Slider bossHealthBar;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;

        currentGun.sprite = PlayerController.instance.availableGuns[PlayerController.instance.currentGun].gunUI;
        gunText.text = PlayerController.instance.availableGuns[PlayerController.instance.currentGun].weaponName;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }

        if(fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(newGameScene);

        Destroy(PlayerController.instance.gameObject);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuScene);

        Destroy(PlayerController.instance.gameObject);
    }

    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }
}
