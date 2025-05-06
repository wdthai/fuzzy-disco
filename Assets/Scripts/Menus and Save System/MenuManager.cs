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
    public Button pauseButton, resumeButton, saveButton, loadButton, quitButton;
    private bool isPaused = false;

    public GameObject confirmPanel;
    public Button confirmButton, cancelButton;
    public TextMeshProUGUI warningText;
    public enum menuOption{Pause, Save, Load, Quit}
    public menuOption choice;

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
        ConsolePanel.Instance.Minimise();
        GameManager.Instance.isTabOpen = true;
    }

    public void Save()
    {
        choice = menuOption.Save; // Save
        confirmPanel.SetActive(true);
        warningText.text = "Are you sure you want to save? This will overwrite the current save.";
    }
    public void Load()
    {
        choice = menuOption.Load; // Load
        confirmPanel.SetActive(true);
        warningText.text = "Are you sure you want to load? You will lose unsaved progress.";
    }

    public void confirmAction()
    {
        switch(choice)
        {
            case menuOption.Save:
                SaveManager.Save(GameManager.Instance.SaveState());
                confirmPanel.SetActive(false);
                break;
            case menuOption.Load:
                GameSaveData gameSave = SaveManager.Load();
                GameManager.Instance.LoadState(gameSave);
                confirmPanel.SetActive(false);
                break;
            case menuOption.Quit:
                Debug.Log("Quit");
                SceneManager.LoadScene("Start Menu");
                confirmPanel.SetActive(false);
                break;
        }

        choice = menuOption.Pause; // Reset menu option
    }

    public void Quit()
    {
        choice = menuOption.Quit; // Quit
        confirmPanel.SetActive(true);
        warningText.text = "Are you sure you want to quit? You will lose unsaved progress.";
    }
}
