using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RegionInfoPanel : MonoBehaviour
{
    public static RegionInfoPanel Instance; // Singleton for easy access

    public TextMeshProUGUI nameText, wealthText, educationText, stabilityText, complianceText, sustainabilityText;
    public Button closeButton;
    public RegionData currentRegion;
    public Coroutine refreshCoroutine;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false); 
        closeButton.onClick.AddListener(ClosePanel);
    }

    public void Refresh(RegionData region)
    {
        nameText.text = region.regionName;
        wealthText.text = $"Wealth {region.economy}"  ;
        educationText.text = $"Education {region.education}";
        stabilityText.text = $"Stability {region.stability}";
        complianceText.text = $"Compliance {region.compliance}";
        sustainabilityText.text = $"Health {region.health}";

        ActionPanel.Instance.Refresh(region); // Refresh the ActionPanel with the current region data
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
        while (true)
        {
            if (currentRegion != null)
                Refresh(currentRegion);
            yield return new WaitForSeconds(2f);
        }
    }
}
