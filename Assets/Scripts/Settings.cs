using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Settings : MonoBehaviour
{
    public static Settings instance;

    [Header("Sound Settings")]
    public Toggle muteToggle;
    public Toggle loopToggle;
    public Toggle autoSaveToggle;
    public Slider volumeSlider;
    public Text volumeText;

    [Header("Scenes To load")]
    public string backToMainMenu; // It loads the Title Scene

    [Header("Panels To Pop-Up")]
    public GameObject confirmPanel;

    [Header("AutoSave Settings")]
    public GameObject confirmButton;
    public float autoSaveTimer = 10.0f; // The amount of time required to pass before auto-saving.
    private float autoSaveCounter; // The counter for that.

    public InGameSettings soundSettings; // Sound setting class object.

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
        UpdateSoundSettings();

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

        GameObject.Find("InGameSong").GetComponent<AudioSource>().volume = DataSaver.LoadData().volumeSlider;
        GameObject.Find("InGameSong").GetComponent<AudioSource>().mute = DataSaver.LoadData().muteToggle;
        GameObject.Find("InGameSong").GetComponent<AudioSource>().loop = DataSaver.LoadData().loopToggle;
    }

    private void UpdateSoundSettings()
    {
        soundSettings.muteToggle = this.muteToggle.isOn;
        soundSettings.loopToggle = this.loopToggle.isOn;
        soundSettings.autoSaveToggle = this.autoSaveToggle.isOn;
        soundSettings.volumeSlider = this.volumeSlider.value;
        soundSettings.volumeText = this.volumeText.text;
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
        DataSaver.SaveData(this.soundSettings);
    }

    public void LoadChanges()
    {
        // Load back the values.
        soundSettings = DataSaver.LoadData();
        if (soundSettings != null)
        {
            this.muteToggle.isOn = soundSettings.muteToggle;
            this.loopToggle.isOn = soundSettings.loopToggle;
            this.autoSaveToggle.isOn = soundSettings.autoSaveToggle; 
            this.volumeSlider.value = soundSettings.volumeSlider; 
            this.volumeText.text = soundSettings.volumeText; 
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

[System.Serializable]
public class InGameSettings
{
    // Sound Settings
    public bool muteToggle;
    public bool loopToggle;
    public bool autoSaveToggle;
    public float volumeSlider;
    public string volumeText;
}
