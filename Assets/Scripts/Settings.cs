using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    [Header("Scenes To load")]
    public string backToMainMenu; // It loads the Title Scene

    [Header("Panels To Pop-Up")]
    public GameObject confirmPanel;

    [Header("Sound Settings")]
    public Toggle muteToggle;
    public Toggle loopToggle;
    public Slider volumeSlider;
    public Text volumeText;
    public Toggle autoSaveToggle;

    [Header("AutoSave Settings")]
    public GameObject confirmButton;
    public float autoSaveTimer = 10.0f; // The amount of time required to pass before auto-saving.
    private float autoSaveCounter; // The counter for that.
    

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        autoSaveCounter = autoSaveTimer;
        LoadChanges();
    }

    // Update is called once per frame
    void Update()
    {
        volumeText.text = (volumeSlider.value / volumeSlider.maxValue * 100.0f).ToString("0.00");

        if (autoSaveToggle.isOn)
        {
            confirmButton.SetActive(false);

            // AutoSave Section
            if (autoSaveCounter <= 0.0f)
            {
                autoSaveCounter = autoSaveTimer;
                SaveChanges();
            }
            else
            {
                autoSaveCounter -= Time.deltaTime;
            }
        }
        else
        {
            confirmButton.SetActive(true);

            autoSaveCounter = autoSaveTimer;
        }

        GameObject.Find("InGameSong").gameObject.GetComponent<AudioSource>().volume = DataSaver.LoadData().volumePercentage;
        GameObject.Find("InGameSong").gameObject.GetComponent<AudioSource>().mute = DataSaver.LoadData().muteStatus;
        GameObject.Find("InGameSong").gameObject.GetComponent<AudioSource>().loop = DataSaver.LoadData().loopStatus;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(backToMainMenu);
    }

    public void ConfirmChanges()
    {
        confirmPanel.SetActive(true);
    }

    public void SaveChanges()
    {
        confirmPanel.SetActive(false);

        // Saving the changes.
        DataSaver.SaveData(this);
    }

    public void LoadChanges()
    {
        // Load back the values.
        InGameSettings inGameSettings = DataSaver.LoadData();
        if (inGameSettings != null)
        {
            this.muteToggle.isOn = inGameSettings.muteStatus;
            this.loopToggle.isOn = inGameSettings.loopStatus;
            this.autoSaveToggle.isOn = inGameSettings.autoSaveStatus;
            this.volumeSlider.value = inGameSettings.volumePercentage;
            this.volumeText.text = inGameSettings.volumeInfo;
        }
        else
        {
            SaveChanges();
            LoadChanges();
        }
    }

    public void CancelSavingChanges()
    {
        confirmPanel.SetActive(false);
    }
}
