using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataSkillsPanel : MonoBehaviour
{

    public static DataSkillsPanel Instance;
    // Start is called before the first frame update
    public Button closeButton;
    void Awake()
    {
        Instance = this;
        // gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        closeButton.onClick.AddListener(ClosePanel);
    }

    public void OpenPanel(string panel)
    {
        gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
        RegionInfoPanel.Instance.ClosePanel();
        GameManager.Instance.isTabOpen = true;

        if (panel == "data")
        {
            // Open data panel
            DataPanel.Instance.OpenPanel();
            SkillsPanel.Instance.ClosePanel();
            Debug.Log("Data Panel Opened");
        }
        else if (panel == "skills")
        {
            // Open skill panel
            DataPanel.Instance.ClosePanel();
            SkillsPanel.Instance.OpenPanel();
            Debug.Log("Skills Panel Opened");
        }
    }

    public void ClosePanel()
    {
        DataPanel.Instance.ClosePanel();
        SkillsPanel.Instance.ClosePanel();
        gameObject.SetActive(false);
        GameManager.Instance.isTabOpen = false;
    }
}
