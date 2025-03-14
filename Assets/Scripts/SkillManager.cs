using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public static SkillManager Instance;

    public List<SkillData> allSkills; // Assign in Inspector


    public float moneyGenerationRate = 1f; // baseRate * generationRate 
    public float researchGenerationRate = 1f;
    public float moneyCostReduction = 1f; // baseCost * reduction
    public float researchCostReduction = 1f;
    public float policyCostReduction = 1f;


    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool isUnlockable(SkillData skill)
    {
        if (skill.isUnlocked) return false;
        if (GameManager.Instance.research < (int)(skill.baseResearchCost * researchCostReduction)) return false;
        if (GameManager.Instance.money < (int)(skill.baseMoneyCost * moneyCostReduction)) return false;

        // Check prerequisites
        // foreach (Skill pre in skill.prerequisites)
        // {
        //     if (!pre.isUnlocked) return false;
        // }

        return true;
    }

    public void UnlockSkill(SkillData skill)
    {
        if (!isUnlockable(skill)) return;

        GameManager.Instance.research -= (int)(skill.researchCost * researchCostReduction);
        GameManager.Instance.money -= (int)(skill.moneyCost * moneyCostReduction);
        skill.isUnlocked = true;
        
        // Debug.Log($"Unlocked Skill: {skill.skillName}");
        applySkill(skill);

        // Update UI (next)
        // SkillUI.Instance.UpdateUI();
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
