using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public GameObject pauseMenu;
    public Button pauseButton;
    public Button resumeButton;
    public Button saveButton;
    public Button loadButton;
    public Button quitButton;
    private bool isPaused = false;

    void Start()
    {
        Instance = this;
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        saveButton.onClick.AddListener(Save);
        // loadButton.onClick.AddListener(LoadGame);
        // quitButton.onClick.AddListener(QuitGame);

        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveManager.Save(GameManager.Instance.SaveState());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameSaveData gameSave = SaveManager.Load();
            GameManager.Instance.LoadState(gameSave);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        GameManager.Instance.isTabOpen = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        RegionInfoPanel.Instance.ClosePanel();
        DataPanel.Instance.ClosePanel();
        SkillsPanel.Instance.ClosePanel();
        GameManager.Instance.isTabOpen = true;
    }

    public void Save()
    {
        SaveManager.Save(GameManager.Instance.SaveState());
    }
    public void Load()
    {
        GameSaveData gameSave = SaveManager.Load();
        GameManager.Instance.LoadState(gameSave);
    }
}
