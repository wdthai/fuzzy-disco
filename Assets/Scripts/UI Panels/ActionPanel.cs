using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour
{
    // display stat changes
    public static ActionPanel Instance;
    public GameObject actionDataPrefab;  
    public Transform dataContainer;     
    private void Awake()
    {
        Instance = this;
    }
    public void Refresh(RegionData region){
        foreach (Transform child in dataContainer)
        {
            Destroy(child.gameObject);
        }
        
        if (region.isDead) return;

        foreach (ActionData action in region.actions)
        {
            GameObject actionUI = Instantiate(actionDataPrefab, dataContainer);
            TextMeshProUGUI[] textFields = actionUI.GetComponentsInChildren<TextMeshProUGUI>();
            Button button = actionUI.GetComponentInChildren<Button>();
            button.interactable = false;
            
            action.finalMoneyCost = (action.baseMoneyCost * GameManager.Instance.moneyCostMultiplier);
            action.finalResearchCost = (action.baseResearchCost * GameManager.Instance.researchCostMultiplier);

            string statChanges = "";
            statChanges += action.isDirect ? "Repeatable\nDirect Changes:\n" : "One-time investment\nRate Changes (per tick):\n" ;

            if (action.economyChange != 0) statChanges += $"Economy: {action.economyChange.ToString("+#;-#;0")}\n";
            if (action.taxChange != 0) statChanges += $"Tax: {action.taxChange.ToString("+#;-#;0")}\n";
            if (action.educationChange != 0) statChanges += $"Education: {action.educationChange.ToString("+#;-#;0")}\n";
            if (action.stabilityChange != 0) statChanges += $"Stability: {action.stabilityChange.ToString("+#;-#;0")}\n";
            if (action.complianceChange != 0) statChanges += $"Compliance: {action.complianceChange.ToString("+#;-#;0")}\n";
            if (action.happinessChange != 0) statChanges += $"Happiness: {action.happinessChange.ToString("+#;-#;0")}\n";
            if (action.healthChange != 0) statChanges += $"Health: {action.healthChange.ToString("+#;-#;0")}\n";

            textFields[0].text = $"{action.actionName}\n\n{action.description}";
            textFields[1].text = $"{statChanges}";

            if (action.isUnlocked) 
            {
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = "Enforced";
                button.interactable = false;
                textFields[2].text = $"";
                textFields[3].text = $"";
            }
            else 
            {
                button.interactable = ActionManager.Instance.isExecutable(action);
                button.onClick.AddListener(() => ActionManager.Instance.ExecuteAction(action, region));
                textFields[2].text = $"Money: {action.finalMoneyCost.ToString("F0")}";
                textFields[3].text = $"Research: {action.finalResearchCost.ToString("F0")}";
            }

        }
    }
}
