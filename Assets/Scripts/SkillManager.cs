using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public static SkillManager Instance;

    public List<SkillData> allSkills; // Assign in Inspector

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool isUnlockable(SkillData skill)
    {
        if (skill.isUnlocked) return false;
        if (GameManager.Instance.research < (int)(skill.baseResearchCost * GameManager.Instance.researchCostMultiplier)) return false;
        if (GameManager.Instance.money < (int)(skill.baseMoneyCost * GameManager.Instance.moneyCostMultiplier)) return false;

        // Check prerequisites
        // foreach (SkillData pre in skill.prerequisites)
        // {
        //     if (!pre.isUnlocked) return false;
        // }

        return true;
    }

    public void UnlockSkill(SkillData skill)
    {
        if (!isUnlockable(skill)) return;

        GameManager.Instance.research -= (int)(skill.finalResearchCost * GameManager.Instance.researchCostMultiplier);
        GameManager.Instance.money -= (int)(skill.finalMoneyCost * GameManager.Instance.moneyCostMultiplier);
        skill.isUnlocked = true;
        
        // Debug.Log($"Unlocked Skill: {skill.skillName}");
        applySkill(skill);

        SkillsPanel.Instance.Refresh();
        GameInfoPanel.Instance.Refresh(GameManager.Instance);
    }

    public void applySkill(SkillData skill)
    {
        GameManager.Instance.moneyGenerationMultiplier *= skill.moneyGenerationModifier;
        GameManager.Instance.researchGenerationMultiplier *= skill.researchGenerationModifier;
        GameManager.Instance.moneyCostMultiplier *= skill.moneyCostModifier;
        GameManager.Instance.researchCostMultiplier *= skill.researchCostModifier;
        GameManager.Instance.policyCostMultiplier *= skill.policyCostModifier;
    }

    public SkillSaveData SaveState(SkillData skill)
    {
        SkillSaveData skillSave = new SkillSaveData();
        skillSave.skillName = skill.skillName;
        skillSave.description = skill.description;

        skillSave.moneyGenerationModifier = skill.moneyGenerationModifier;
        skillSave.researchGenerationModifier = skill.researchGenerationModifier;
        skillSave.moneyCostModifier = skill.moneyCostModifier;
        skillSave.researchCostModifier = skill.researchCostModifier;
        skillSave.policyCostModifier = skill.policyCostModifier;

        skillSave.baseResearchCost = skill.baseResearchCost;
        skillSave.baseMoneyCost = skill.baseMoneyCost;
        skillSave.finalResearchCost = skill.finalResearchCost;
        skillSave.finalMoneyCost = skill.finalMoneyCost;
        skillSave.isUnlocked = skill.isUnlocked;

        return skillSave;
    }

    public SkillData LoadState(SkillSaveData skillSave)
    {
        SkillData skill = new SkillData();
        skill.description = skillSave.description;
        skill.moneyGenerationModifier = skillSave.moneyGenerationModifier;
        skill.researchGenerationModifier = skillSave.researchGenerationModifier;
        skill.moneyCostModifier = skillSave.moneyCostModifier;
        skill.researchCostModifier = skillSave.researchCostModifier;
        skill.policyCostModifier = skillSave.policyCostModifier;

        skill.baseResearchCost = skillSave.baseResearchCost;
        skill.baseMoneyCost = skillSave.baseMoneyCost;
        skill.finalResearchCost = skillSave.finalResearchCost;
        skill.finalMoneyCost = skillSave.finalMoneyCost;
        skill.isUnlocked = skillSave.isUnlocked;

        return skill;
    }
}
