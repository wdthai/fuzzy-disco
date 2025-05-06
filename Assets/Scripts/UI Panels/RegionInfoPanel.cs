using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RegionInfoPanel : MonoBehaviour
{
    // display core stats only
    public static RegionInfoPanel Instance; // Singleton for easy access

    public TextMeshProUGUI nameText, wealthText, taxText, educationText, stabilityText, complianceText, happinessText, sustainabilityText;
    public Button closeButton;
    public RegionData currentRegion;
    public Coroutine refreshCoroutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        gameObject.SetActive(false); 
        closeButton.onClick.AddListener(ClosePanel);
    }

    public void Refresh(RegionData region)
    {
        nameText.text = region.regionName;
        wealthText.text = $"Economy {region.economy.ToString("F2")}";
        taxText.text = $"Tax {region.tax.ToString("F2")}";
        educationText.text = $"Education {region.education.ToString("F2")}";
        stabilityText.text = $"Stability {region.stability.ToString("F2")}";
        complianceText.text = $"Compliance {region.compliance.ToString("F2")}";
        happinessText.text = $"Happiness {region.happiness.ToString("F2")}";
        sustainabilityText.text = $"Health {region.health.ToString("F2")}";

        ActionPanel.Instance.Refresh(region);
    }

    public void OpenPanel(RegionData region)
    {
        currentRegion = region;
        Refresh(region);
        gameObject.SetActive(true);

        if (refreshCoroutine != null)
            StopCoroutine(refreshCoroutine);
        refreshCoroutine = StartCoroutine(AutoRefreshRegion());
        GameManager.Instance.isTabOpen = true; // Set the tab open state
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        GameManager.Instance.isTabOpen = false;
    }

    IEnumerator AutoRefreshRegion()
    {
        while (currentRegion != null)
        {
            Refresh(currentRegion);
            yield return new WaitForSeconds(2f);
        }
    }
}
