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
            GameManager.Instance.research -= (action.finalResearchCost * GameManager.Instance.researchCostMultiplier);
            GameManager.Instance.money -= (action.finalMoneyCost * GameManager.Instance.moneyCostMultiplier);
            action.isUnlocked = true;
        }
        
        if (action.isRateChange)
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

        ConsoleManager.Instance.AddEntry($"Breaking News: {action.actionName} enforced in {region.regionName}."); // reword
        RegionInfoPanel.Instance.Refresh(region);
        GameInfoPanel.Instance.Refresh(GameManager.Instance);
    }

    public ActionSaveData SaveState(ActionData action)
    {
        ActionSaveData actionSave = new ActionSaveData();
        actionSave.actionName = action.actionName;
        actionSave.description = action.description;
        actionSave.isRateChange = action.isRateChange;

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
        action.isRateChange = actionSave.isRateChange;

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
