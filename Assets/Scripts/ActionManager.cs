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
        if (GameManager.Instance.research < (int)(action.baseResearchCost * GameManager.Instance.researchCostMultiplier)) return false;
        if (GameManager.Instance.money < (int)(action.baseMoneyCost * GameManager.Instance.moneyCostMultiplier)) return false;

        // Check prerequisites
        foreach (ActionData pre in action.prerequisites)
        {
            if (!pre.isUnlocked) return false;
        }

        return true;
    }

    public void ExecuteAction(ActionData action, RegionData region)
    {
        // if (!isUnlockable(skill)) return;

        GameManager.Instance.research -= (int)(action.finalResearchCost * GameManager.Instance.researchCostMultiplier);
        GameManager.Instance.money -= (int)(action.finalMoneyCost * GameManager.Instance.moneyCostMultiplier);
        action.isUnlocked = true;
        
        if (action.isRateChange)
        {
            region.economyChangeRate += action.economyChange;
            region.educationChangeRate += action.educationChange;
            region.stabilityChangeRate += action.stabilityChange;
            region.complianceChangeRate += action.complianceChange;
            region.healthChangeRate += action.healthChange;
        }
        else
        {
            region.economy += action.economyChange;
            region.education += action.educationChange;
            region.stability += action.stabilityChange;
            region.compliance += action.complianceChange;
            region.health += action.healthChange;
        }

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
        actionSave.educationChange = action.educationChange;
        actionSave.stabilityChange = action.stabilityChange;
        actionSave.complianceChange = action.complianceChange;
        actionSave.healthChange = action.healthChange;

        actionSave.baseResearchCost = action.baseResearchCost;
        actionSave.baseMoneyCost = action.baseMoneyCost;
        actionSave.finalResearchCost = action.finalResearchCost;
        actionSave.finalMoneyCost = action.finalResearchCost;
        actionSave.isUnlocked = action.isUnlocked;

        return actionSave;
    }

    public ActionData LoadState(ActionSaveData actionSave)
    {
        ActionData action = new ActionData();
        action.actionName = actionSave.actionName;
        action.description = actionSave.description;
        action.isRateChange = actionSave.isRateChange;

        action.economyChange = actionSave.economyChange;
        action.educationChange = actionSave.educationChange;
        action.stabilityChange = actionSave.stabilityChange;
        action.complianceChange = actionSave.complianceChange;
        action.healthChange = actionSave.healthChange;

        action.baseResearchCost = actionSave.baseResearchCost;
        action.baseMoneyCost = actionSave.baseMoneyCost;
        action.finalResearchCost = actionSave.finalResearchCost;
        action.finalMoneyCost = action.finalMoneyCost;
        action.isUnlocked = actionSave.isUnlocked;

        return action;
    }
}
