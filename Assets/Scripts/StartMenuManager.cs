using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public Button NewGameButton;
    public Button LoadButton;
    public Button QuitButton;

    void Start()
    {
        NewGameButton.onClick.AddListener(NewGame);
        LoadButton.onClick.AddListener(LoadGame);
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
