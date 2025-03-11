using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RegionInfoPanel : MonoBehaviour
{
    public static RegionInfoPanel Instance; // Singleton for easy access

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI wealthText;
    public TextMeshProUGUI educationText;
    public TextMeshProUGUI stabilityText;
    public TextMeshProUGUI complianceText;
    public TextMeshProUGUI sustainabilityText;
    public Button closeButton;


    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false); 

        closeButton.onClick.AddListener(ClosePanel);
    }

    public void ShowRegionInfo(RegionData region)
    {
        nameText.text = region.regionName;
        wealthText.text = $"Wealth: {region.wealthTier}"  ;
        educationText.text = $"Education: {region.education}%";
        stabilityText.text = $"Stability: {region.stability}%";
        complianceText.text = $"Compliance: {region.compliance}%";
        sustainabilityText.text = $"Sustainability: {region.sustainability}%";

        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
