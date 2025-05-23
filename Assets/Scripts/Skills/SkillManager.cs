using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public static SkillManager Instance;

    public List<SkillData> originalSkills; // Assign in Inspector
    public List<SkillData> allSkills;

    void Awake()
    {
        if (Instance == null) Instance = this;
        foreach (SkillData skill in originalSkills)
        {
            SkillData newSkill = ScriptableObject.Instantiate(skill);
            allSkills.Add(newSkill);
        }
    }

    void Start()
    {
        
    }

    public bool isUnlockable(SkillData skill)
    {
        if (skill.isUnlocked) return false;
        if (GameManager.Instance.research < (skill.baseResearchCost * GameManager.Instance.researchCostMultiplier)) return false;
        if (GameManager.Instance.money < (skill.baseMoneyCost * GameManager.Instance.moneyCostMultiplier)) return false;

        return true;
    }

    public void UnlockSkill(SkillData skill)
    {
        if (!isUnlockable(skill)) return;

        GameManager.Instance.research -= (skill.finalResearchCost * GameManager.Instance.researchCostMultiplier);
        GameManager.Instance.money -= (skill.finalMoneyCost * GameManager.Instance.moneyCostMultiplier);
        skill.isUnlocked = true;
        
        applySkill(skill);

        SkillsPanel.Instance.Refresh();
        GameInfoPanel.Instance.Refresh(GameManager.Instance);
    }

    public void applySkill(SkillData skill)
    {
        // Debug.Log("Applying skill: " + skill.skillName);
        GameManager.Instance.moneyGenerationMultiplier += skill.moneyGenerationModifier;
        GameManager.Instance.researchGenerationMultiplier += skill.researchGenerationModifier;
        GameManager.Instance.moneyCostMultiplier += skill.moneyCostModifier;
        GameManager.Instance.researchCostMultiplier += skill.researchCostModifier;
        GameManager.Instance.policyCostMultiplier += skill.policyCostModifier;
    }

    public SkillSaveData SaveState(SkillData skill, bool isNewGame = false)
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
        skillSave.ticksToComplete = skill.ticksToComplete;
        skillSave.isUnlocked = isNewGame ? false : skill.isUnlocked;

        return skillSave;
    }

    public void LoadState(SkillData skill, SkillSaveData skillSave)
    {
        skill.skillName = skillSave.skillName;
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
        skill.ticksToComplete = skillSave.ticksToComplete;
        skill.isUnlocked = skillSave.isUnlocked;
    }
}
