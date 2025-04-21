using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public SkillData[] prerequisites; 

    // global modifiers
    public float moneyGenerationModifier = 1f;
    public float researchGenerationModifier = 1f;
    public float moneyCostModifier = 1f;
    public float researchCostModifier = 1f;
    public float policyCostModifier = 1f;

    public int baseResearchCost; // research points
    public int baseMoneyCost; // money
    public int finalResearchCost; // research points
    public int finalMoneyCost; // money
    public bool isUnlocked = false; 
}
