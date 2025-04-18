using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RegionInfoPanel : MonoBehaviour
{
    public static RegionInfoPanel Instance; // Singleton for easy access

    public TextMeshProUGUI nameText, wealthText, educationText, stabilityText, complianceText, sustainabilityText;
    public GameObject regionActionDataPrefab;  // Assign the prefab in the Inspector
    public Transform dataContainer;      // Parent object for instantiated UI elements
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
        wealthText.text = $"Wealth: {region.localEconomy}"  ;
        educationText.text = $"Education: {region.localEducation}%";
        stabilityText.text = $"Stability: {region.localStability}%";
        complianceText.text = $"Compliance: {region.localCompliance}%";
        sustainabilityText.text = $"Health: {region.localHealth}%";

        foreach (Transform child in dataContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (ActionData action in region.actions)
        {


            // if (action.isUnlocked) 
            // {
            //     continue;
            // } // Skip if already unlocked


            action.finalMoneyCost = (int)(action.baseMoneyCost * SkillManager.Instance.moneyCostReduction);
            action.finalResearchCost = (int)(action.baseResearchCost * SkillManager.Instance.researchCostReduction);
            // Debug.Log("Skill: " + skill.skillName + skill.finalMoneyCost + skill.finalResearchCost);

            // Instantiate UI prefab inside the dataContainer
            GameObject actionUI = Instantiate(regionActionDataPrefab, dataContainer);
            TextMeshProUGUI[] textFields = actionUI.GetComponentsInChildren<TextMeshProUGUI>();
            Button button = actionUI.GetComponentInChildren<Button>();

            // Get references to the TMP fields inside the prefab
            // button.interactable = SkillManager.Instance.isUnlockable(skill);
            // button.onClick.AddListener(() => SkillManager.Instance.UnlockSkill(skill));
            // Set data
            textFields[0].text = $"{action.actionName}";
            textFields[1].text = $"Desc: {action.description}";
            textFields[2].text = $"Money: {action.finalMoneyCost}";
            textFields[3].text = $"Research: {action.finalResearchCost}";
        }
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
