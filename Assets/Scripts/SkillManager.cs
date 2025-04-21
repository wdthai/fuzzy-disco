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
        foreach (SkillData pre in skill.prerequisites)
        {
            if (!pre.isUnlocked) return false;
        }

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
}
