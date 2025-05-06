using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsolePanel : MonoBehaviour
{
    public static ConsolePanel Instance;
    public GameObject ConsoleDataPrefab;  
    public Transform dataContainer;     
    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        if (gameObject.activeSelf)
        {
            Minimise();
            GameManager.Instance.isTabOpen = false;
        }
        else
        {
            Expand();
            GameManager.Instance.isTabOpen = true;
        }
    }

    public void Expand()
    {
        gameObject.SetActive(true);
        RegionInfoPanel.Instance.ClosePanel();
        DataPanel.Instance.ClosePanel();
        SkillsPanel.Instance.ClosePanel();
        Refresh();
    }
    public void Minimise()
    {
        gameObject.SetActive(false);
    }

    public void Refresh()
    {
        if (!gameObject.activeSelf) return;

        foreach (Transform child in dataContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (string entry in ConsoleManager.Instance.logQueue)
        {
            GameObject consoleUI = Instantiate(ConsoleDataPrefab);
            consoleUI.transform.SetParent(dataContainer, false); 
            consoleUI.transform.SetSiblingIndex(0);
            TextMeshProUGUI[] textFields = consoleUI.GetComponentsInChildren<TextMeshProUGUI>();
            textFields[0].text = entry;
        }
    }
}
