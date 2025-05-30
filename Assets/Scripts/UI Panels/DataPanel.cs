using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DataPanel : MonoBehaviour
{
    // show all region data stats, not actions
    public static DataPanel Instance;

    public GameObject regionDataPrefab;  
    public Transform dataContainer;      
    public Button closeButton;

    void Awake()
    {
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
            GameObject regionUI = Instantiate(regionDataPrefab, dataContainer);
            
            TextMeshProUGUI[] textFields = regionUI.GetComponentsInChildren<TextMeshProUGUI>();

            // Set data
            textFields[0].text = $"{region.data.regionName}";

            string coreStats = "";
            coreStats += $"Economy: {region.data.economy.ToString("F2")}\n";
            coreStats += $"Tax: {region.data.tax.ToString("F2")}\n";
            coreStats += $"Education: {region.data.education.ToString("F2")}\n";
            coreStats += $"Stability: {region.data.stability.ToString("F2")}\n";
            coreStats += $"Compliance: {region.data.compliance.ToString("F2")}\n";
            coreStats += $"Happiness: {region.data.happiness.ToString("F2")}\n";
            coreStats += $"Health: {region.data.health.ToString("F2")}\n";
            textFields[2].text = $"{coreStats}";

            string rateChanges = "";
            rateChanges += $"Economy: {(region.data.economyChangeRate/10f).ToString("+#0.00;-#0.00;0.00")}\n";
            rateChanges += $"Tax: {(region.data.taxChangeRate/10f).ToString("+#0.00;-#0.00;0.00")}\n";
            rateChanges += $"Education: {(region.data.educationChangeRate/10f).ToString("+#0.00;-#0.00;0.00")}\n";
            rateChanges += $"Stability: {(region.data.stabilityChangeRate/10f).ToString("+#0.00;-#0.00;0.00")}\n";
            rateChanges += $"Compliance: {(region.data.complianceChangeRate/10f).ToString("+#0.00;-#0.00;0.00")}\n";
            rateChanges += $"Happiness: {(region.data.happinessChangeRate/10f).ToString("+#0.00;-#0.00;0.00")}\n";
            rateChanges += $"Health: {(region.data.healthChangeRate/10f).ToString("+#0.00;-#0.00;0.00")}\n";
            textFields[4].text = $"{rateChanges}";

            textFields[5].text = $"{region.ai.currentState}";
            textFields[6].text = $"{region.data.challengeName}";
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
        ConsolePanel.Instance.Minimise();
        gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
        GameManager.Instance.isTabOpen = true;
    }
}
