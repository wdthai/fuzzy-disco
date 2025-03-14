using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataPanel : MonoBehaviour
{

    public static DataPanel Instance;
    // Start is called before the first frame update
    public GameObject regionDataPrefab;  // Assign the prefab in the Inspector
    public Transform dataContainer;      // Parent object for instantiated UI elements

    void Awake()
    {
        Debug.Log("DataPanel Awake");
        Instance = this;
        gameObject.SetActive(false);
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
            textFields[1].text = $"Wealth: {region.data.wealthTier}";
            textFields[2].text = $"Education: {region.data.education}%";
            textFields[3].text = $"Stability: {region.data.stability}%";
            textFields[4].text = $"Compliance: {region.data.compliance}%";
            textFields[5].text = $"Sustainability: {region.data.sustainability}%";
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void OpenPanel()
    {
        Refresh();
        gameObject.SetActive(true);
    }
}
