using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DataPanel : MonoBehaviour
{

    public static DataPanel Instance;
    // Start is called before the first frame update
    public GameObject regionDataPrefab;  // Assign the prefab in the Inspector
    public Transform dataContainer;      // Parent object for instantiated UI elements
    public Button closeButton;

    void Awake()
    {
        Debug.Log("DataPanel Awake");
        Instance = this;
        gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        closeButton.onClick.AddListener(ClosePanel);
    }

    public void Refresh()
    {
        if (!gameObject.activeSelf) return;

        foreach (Transform child in dataContainer)
        {
            Destroy(child.gameObject);
        }

        Region[] regions = FindObjectsOfType<Region>();

        foreach (Region region in regions)
        {
            // Debug.Log("Region: " + region.data.regionName);
            // Instantiate UI prefab inside the dataContainer
            GameObject regionUI = Instantiate(regionDataPrefab, dataContainer);
            
            // Get references to the TMP fields inside the prefab
            TextMeshProUGUI[] textFields = regionUI.GetComponentsInChildren<TextMeshProUGUI>();

            // Set data
            textFields[0].text = $"{region.data.regionName}";
            textFields[1].text = $"Wealth: {region.data.economy.ToString("F2")}%";
            textFields[2].text = $"Education: {region.data.education.ToString("F2")}%";
            textFields[3].text = $"Stability: {region.data.stability.ToString("F2")}%";
            textFields[4].text = $"Compliance: {region.data.compliance.ToString("F2")}%";
            textFields[5].text = $"Health: {region.data.health.ToString("F2")}%";
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        GameManager.Instance.isTabOpen = false;
    }
    public void OpenPanel()
    {
        Refresh();
        SkillsPanel.Instance.ClosePanel();
        RegionInfoPanel.Instance.ClosePanel();
        gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
        Debug.Log("Data Panel Opened");
        GameManager.Instance.isTabOpen = true;
    }
}
