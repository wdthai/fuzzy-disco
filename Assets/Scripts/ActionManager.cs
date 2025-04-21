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
        
        // Debug.Log($"Unlocked Skill: {skill.skillName}");
        region.economy += action.economyChange;
        region.education += action.educationChange;
        region.stability += action.stabilityChange;
        region.compliance += action.complianceChange;
        region.health += action.healthChange;

        RegionInfoPanel.Instance.Refresh(region);
        GameInfoPanel.Instance.Refresh(GameManager.Instance);
    }
}
