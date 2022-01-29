using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scenes to load")]
    public string levelToLoad;
    public string settingScene;

    [Header("Panels to pop-up")]
    public GameObject deletePanel;

    [Header("Chars. to be deleted")]
    public CharacterSelector[] charactersToDelete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Loads the scene
    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    // Exits the game
    public void ExitGame()
    {
        Application.Quit();
    }

    // Pop-ups the confirmation panel before deleting chars.
    public void DeleteSave()
    {
        deletePanel.SetActive(true);
    }

    // Deletes all chars in the list.
    public void ConfirmDelete()
    {
        deletePanel.SetActive(false);

        foreach(CharacterSelector theChar in charactersToDelete)
        {
            PlayerPrefs.SetInt(theChar.playerToSpawn.name, 0);
        }
    }

    // Cancel deletion of chars.
    public void CancelDelete()
    {
        deletePanel.SetActive(false);
    }

    public void SwitchToSettingScene()
    {
        SceneManager.LoadScene(settingScene);
    }
}
