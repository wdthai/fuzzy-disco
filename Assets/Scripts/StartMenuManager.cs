using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public Button NewGameButton;
    public Button LoadButton;
    public Button HelpButton;
    public Button QuitButton;

    void Start()
    {
        NewGameButton.onClick.AddListener(NewGame);
        LoadButton.onClick.AddListener(LoadGame);
        HelpButton.onClick.AddListener(ShowHelp);
        QuitButton.onClick.AddListener(QuitGame);
    }
    public void NewGame()
    {
        Time.timeScale = 1f;
        SaveManager.isNewGame = true;
        SceneManager.LoadScene("Game");
    }

    public void LoadGame()
    {
        Time.timeScale = 1f;
        SaveManager.isNewGame = false;
        SceneManager.LoadScene("Game");
    }

    public void ShowHelp()
    {
        // Show help menu or instructions
        Debug.Log("Help button clicked. Show help menu.");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
