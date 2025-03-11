using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataSkillsPanel : MonoBehaviour
{

    public static DataSkillsPanel Instance;
    // public DataPanel dataPanel;
    public GameObject skillsPanel;
    // Start is called before the first frame update
    public Button closeButton;
    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
        // skillsPanel.SetActive(false);
        closeButton.gameObject.SetActive(false);
        closeButton.onClick.AddListener(ClosePanel);
    }

    public void OpenPanel(string panel)
    {
        gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
        // GameInfoPanel.Instance.gameObject.SetActive(false);
        RegionInfoPanel.Instance.gameObject.SetActive(false);

        if (panel == "data")
        {
            // Open data panel
            DataPanel.Instance.gameObject.SetActive(true);
            skillsPanel.SetActive(false);
            Debug.Log("Data Panel Opened");
        }
        else if (panel == "skills")
        {
            // Open skill panel
            DataPanel.Instance.gameObject.SetActive(false);
            skillsPanel.SetActive(true);
            Debug.Log("Skills Panel Opened");
        }
    }

    public void ClosePanel()
    {
        DataPanel.Instance.gameObject.SetActive(false);
        skillsPanel.SetActive(false);
        gameObject.SetActive(false);
        // GameInfoPanel.Instance.gameObject.SetActive(true);
    }
}
