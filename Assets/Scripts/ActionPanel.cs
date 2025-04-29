using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour
{
    public static ActionPanel Instance;
    public GameObject actionDataPrefab;  // Assign the prefab in the Inspector
    public Transform dataContainer;      // Parent object for instantiated UI elements
    private void Awake()
    {
        Instance = this;
    }
    public void Refresh(RegionData region){
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

            GameObject actionUI = Instantiate(actionDataPrefab, dataContainer);
            TextMeshProUGUI[] textFields = actionUI.GetComponentsInChildren<TextMeshProUGUI>();
            Button button = actionUI.GetComponentInChildren<Button>();
            
            action.finalMoneyCost = (action.baseMoneyCost * GameManager.Instance.moneyCostMultiplier);
            action.finalResearchCost = (action.baseResearchCost * GameManager.Instance.researchCostMultiplier);

            // Instantiate UI prefab inside the dataContainer
            
            // Get references to the TMP fields inside the prefab
            button.interactable = ActionManager.Instance.isExecutable(action);
            button.onClick.AddListener(() => ActionManager.Instance.ExecuteAction(action, region));
            // Set data
            textFields[0].text = $"{action.actionName}";
            textFields[1].text = $"Desc: {action.description}";
            textFields[2].text = $"Money: {action.finalMoneyCost.ToString("F0")}";
            textFields[3].text = $"Research: {action.finalResearchCost.ToString("F0")}";
        }
    }
}
