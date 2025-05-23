using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillsPanel : MonoBehaviour
{

    public static SkillsPanel Instance;
    public GameObject skillsDataPrefab; 
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

        foreach (SkillData skill in SkillManager.Instance.allSkills)
        {
            GameObject skillUI = Instantiate(skillsDataPrefab, dataContainer);
            
            TextMeshProUGUI[] textFields = skillUI.GetComponentsInChildren<TextMeshProUGUI>();
            Button button = skillUI.GetComponentInChildren<Button>();

            if (skill.isUnlocked) 
            {
                textFields[0].text = $"{skill.skillName}";
                textFields[1].text = $"{skill.description}";
                textFields[2].text = $"";
                textFields[3].text = $"";
                button.interactable = false;
                TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
                buttonText.text = "Acquired";
                continue;
            } // Skip if already unlocked


            skill.finalMoneyCost = (skill.baseMoneyCost * GameManager.Instance.moneyCostMultiplier);
            skill.finalResearchCost = (skill.baseResearchCost * GameManager.Instance.researchCostMultiplier);
        
            button.interactable = SkillManager.Instance.isUnlockable(skill);
            button.onClick.AddListener(() => SkillManager.Instance.UnlockSkill(skill));

            textFields[0].text = $"{skill.skillName}";
            textFields[1].text = $"{skill.description}";
            textFields[2].text = $"Money: {skill.finalMoneyCost.ToString("F0")}";
            textFields[3].text = $"Research: {skill.finalResearchCost.ToString("F0")}";
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
        DataPanel.Instance.ClosePanel();
        RegionInfoPanel.Instance.ClosePanel();
        ConsolePanel.Instance.Minimise();
        gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
        GameManager.Instance.isTabOpen = true;
    }
}

