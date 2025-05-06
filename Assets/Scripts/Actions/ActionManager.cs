using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool isExecutable(ActionData action)
    {
        if (action.isUnlocked) return false;
        if (GameManager.Instance.research < (action.baseResearchCost * GameManager.Instance.researchCostMultiplier)) return false;
        if (GameManager.Instance.money < (action.baseMoneyCost * GameManager.Instance.moneyCostMultiplier)) return false;

        return true;
    }

    public void ExecuteAction(ActionData action, RegionData region, bool isAI = false)
    {
        if (!isExecutable(action)) return;

        if (!isAI){
            // Debug.Log($"Money: {GameManager.Instance.money}, Research: {GameManager.Instance.research}\n{action.actionName} costs: {action.finalMoneyCost} {action.finalResearchCost}");
            GameManager.Instance.research -= (action.finalResearchCost * GameManager.Instance.researchCostMultiplier);
            GameManager.Instance.money -= (action.finalMoneyCost * GameManager.Instance.moneyCostMultiplier);
            
            if (!action.isDirect)
                action.isUnlocked = true;
            
        }
        
        if (!action.isDirect)
        {
            region.economyChangeRate += action.economyChange * region.compliance / 100f;
            region.taxChangeRate += action.taxChange * region.compliance / 100f;
            region.educationChangeRate += action.educationChange * region.compliance / 100f;
            region.stabilityChangeRate += action.stabilityChange * region.compliance / 100f;
            region.complianceChangeRate += action.complianceChange * region.compliance / 100f;
            region.happinessChangeRate += action.happinessChange * region.compliance / 100f;
            region.healthChangeRate += action.healthChange * region.compliance / 100f;
        }
        else
        {
            region.economy += action.economyChange * region.compliance / 100f;
            region.tax += action.taxChange * region.compliance / 100f;
            region.education += action.educationChange * region.compliance / 100f;
            region.stability += action.stabilityChange * region.compliance / 100f;
            region.compliance += action.complianceChange * region.compliance / 100f;
            region.happiness += action.happinessChange * region.compliance / 100f;
            region.health += action.healthChange * region.compliance / 100f;
        }

        ConsoleManager.Instance.AddEntry($"Breaking News: {action.actionName} enforced in {region.regionName}.");
        // Debug.Log($"Action executed: {action.actionName} applied to {region.regionName}");
        // Debug.Log($"Compliance in {region.regionName}: {region.compliance}\nHappiness in {region.regionName}: {region.happiness}");
        // Debug.Log($"Money: {GameManager.Instance.money}, Research: {GameManager.Instance.research}");
        // Debug.Log($"Economy Change: {action.economyChange}, Tax Change: {action.taxChange}, Education Change: {action.educationChange}, Stability Change: {action.stabilityChange}, Compliance Change: {action.complianceChange}, Happiness Change: {action.happinessChange}, Health Change: {action.healthChange}");
        // Debug.Log($"Health Change: {action.healthChange}");
        // if (isAI) Debug.Log($"AI Action executed: {action.actionName} applied to {region.regionName}");

        if (!isAI) RegionInfoPanel.Instance.Refresh(region);
        GameInfoPanel.Instance.Refresh(GameManager.Instance);
    }

    public ActionSaveData SaveState(ActionData action)
    {
        ActionSaveData actionSave = new ActionSaveData();
        actionSave.actionName = action.actionName;
        actionSave.description = action.description;
        actionSave.isDirect = action.isDirect;

        actionSave.economyChange = action.economyChange;
        actionSave.taxChange = action.taxChange;
        actionSave.educationChange = action.educationChange;
        actionSave.stabilityChange = action.stabilityChange;
        actionSave.complianceChange = action.complianceChange;
        actionSave.happinessChange = action.happinessChange;
        actionSave.healthChange = action.healthChange;

        actionSave.baseResearchCost = action.baseResearchCost;
        actionSave.baseMoneyCost = action.baseMoneyCost;
        actionSave.finalResearchCost = action.finalResearchCost;
        actionSave.finalMoneyCost = action.finalResearchCost;
        actionSave.ticksToComplete = action.ticksToComplete;
        actionSave.isUnlocked = action.isUnlocked;

        return actionSave;
    }

    public void LoadState(ActionData action, ActionSaveData actionSave)
    {
        action.actionName = actionSave.actionName;
        action.description = actionSave.description;
        action.isDirect = actionSave.isDirect;

        action.economyChange = actionSave.economyChange;
        action.taxChange = actionSave.taxChange;
        action.educationChange = actionSave.educationChange;
        action.stabilityChange = actionSave.stabilityChange;
        action.complianceChange = actionSave.complianceChange;
        action.happinessChange = actionSave.happinessChange;
        action.healthChange = actionSave.healthChange;

        action.baseResearchCost = actionSave.baseResearchCost;
        action.baseMoneyCost = actionSave.baseMoneyCost;
        action.finalResearchCost = actionSave.finalResearchCost;
        action.finalMoneyCost = action.finalMoneyCost;
        action.ticksToComplete = actionSave.ticksToComplete;
        action.isUnlocked = actionSave.isUnlocked;
    }
}
