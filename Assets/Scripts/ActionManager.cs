using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{

    public static ActionManager Instance;

    public List<ActionData> actions; // Assign in Inspector

    public float moneyGenerationRate = 1f; // baseRate * generationRate 
    public float researchGenerationRate = 1f;
    public float moneyCostReduction = 1f; // baseCost * reduction
    public float researchCostReduction = 1f;
    public float policyCostReduction = 1f;


    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool isExecutable(ActionData action)
    {
        if (action.isUnlocked) return false;
        if (GameManager.Instance.research < (int)(action.baseResearchCost * researchCostReduction)) return false;
        if (GameManager.Instance.money < (int)(action.baseMoneyCost * moneyCostReduction)) return false;

        // Check prerequisites
        // foreach (Skill pre in skill.prerequisites)
        // {
        //     if (!pre.isUnlocked) return false;
        // }

        return true;
    }

    public void ExecuteAction(ActionData action)
    {
        // if (!isUnlockable(skill)) return;

        GameManager.Instance.research -= (int)(action.finalResearchCost * researchCostReduction);
        GameManager.Instance.money -= (int)(action.finalMoneyCost * moneyCostReduction);
        action.isUnlocked = true;
        
        // Debug.Log($"Unlocked Skill: {skill.skillName}");
        // applySkill(skill);

        SkillsPanel.Instance.Refresh();
        GameInfoPanel.Instance.Refresh(GameManager.Instance);
    }

    public void applySkill(SkillData skill)
    {
        moneyGenerationRate *= skill.moneyGenerationRate;
        researchGenerationRate *= skill.researchGenerationRate;
        moneyCostReduction *= skill.moneyCostReduction;
        researchCostReduction *= skill.researchCostReduction;
        policyCostReduction *= skill.policyCostReduction;
    }
}
