using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


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

    public GameObject confirmPanel;
    public Button confirmButton;
    public Button cancelButton;
    public TextMeshProUGUI warningText;
    public int menuOption = 0; // 1 = save, 2 = load, 3 = quit

    void Start()
    {
        Instance = this;
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        saveButton.onClick.AddListener(Save);
        loadButton.onClick.AddListener(Load);
        quitButton.onClick.AddListener(Quit);

        confirmButton.onClick.AddListener(confirmAction);
        cancelButton.onClick.AddListener(() => { confirmPanel.SetActive(false); });

        confirmPanel.SetActive(false);
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
        GameInfoPanel.Instance.gameObject.SetActive(true);
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
        GameInfoPanel.Instance.gameObject.SetActive(false);
        GameManager.Instance.isTabOpen = true;
    }

    public void Save()
    {
        menuOption = 1; // Save
        confirmPanel.SetActive(true);
        warningText.text = "Are you sure you want to save? This will overwrite the current save.";
    }
    public void Load()
    {
        menuOption = 2; // Load
        confirmPanel.SetActive(true);
        warningText.text = "Are you sure you want to load? You will lose unsaved progress.";
    }

    public void confirmAction()
    {
        if (menuOption == 1)
        {
            GameSaveData gameSave = SaveManager.Load();
            GameManager.Instance.LoadState(gameSave);
            confirmPanel.SetActive(false);
        }
        else if (menuOption == 2)
        {
            SaveManager.Save(GameManager.Instance.SaveState());
            confirmPanel.SetActive(false);
        }
        else if (menuOption == 3)
        {
            SceneManager.LoadScene("Start Menu");
            confirmPanel.SetActive(false);
        }

        menuOption = 0; // Reset menu option
    }

    public void Quit()
    {
        menuOption = 3; // Quit
        confirmPanel.SetActive(true);
        warningText.text = "Are you sure you want to quit? You will lose unsaved progress.";
    }
}
