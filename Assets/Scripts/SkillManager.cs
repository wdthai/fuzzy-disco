using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public static SkillManager Instance;

    public List<Skill> allSkills; // Assign in Inspector

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool isUnlockable(Skill skill)
    {
        if (skill.isUnlocked) return false;
        if (GameManager.Instance.research < skill.cost) return false;

        // Check prerequisites
        // foreach (Skill pre in skill.prerequisites)
        // {
        //     if (!pre.isUnlocked) return false;
        // }

        return true;
    }

    public void UnlockSkill(Skill skill)
    {
        if (!isUnlockable(skill)) return;

        GameManager.Instance.research -= skill.cost;
        skill.isUnlocked = true;
        
        // Debug.Log($"Unlocked Skill: {skill.skillName}");

        // Update UI (next)
        // SkillUI.Instance.UpdateUI();
        SkillsPanel.Instance.Refresh();
        GameInfoPanel.Instance.Refresh(GameManager.Instance);
    }
}
