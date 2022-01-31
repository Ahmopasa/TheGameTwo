using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InGameSettings
{
    // Sound Settings
    public bool muteStatus;
    public bool loopStatus;
    public bool autoSaveStatus;
    public float volumePercentage;
    public string volumeInfo;

    public InGameSettings(Settings inGameSettings)
    {
        this.muteStatus = inGameSettings.muteToggle.isOn;
        this.loopStatus = inGameSettings.loopToggle.isOn;
        this.autoSaveStatus = inGameSettings.autoSaveToggle.isOn;
        this.volumePercentage = inGameSettings.volumeSlider.value;
        this.volumeInfo = inGameSettings.volumeText.text;
    }
}
