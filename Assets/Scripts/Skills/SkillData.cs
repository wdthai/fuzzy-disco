using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;

    // global modifiers
    public float moneyGenerationModifier = 0f;
    public float researchGenerationModifier = 0f;
    public float moneyCostModifier = 0f;
    public float researchCostModifier = 0f;
    public float policyCostModifier = 0f;

    public float baseResearchCost; // research points
    public float baseMoneyCost; // money
    public float finalResearchCost; // research points
    public float finalMoneyCost; // money
    public int ticksToComplete;
    public bool isUnlocked = false; 
}
