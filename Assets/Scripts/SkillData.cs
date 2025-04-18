using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public int baseResearchCost; // research points
    public int baseMoneyCost; // money
    public int finalResearchCost; // research points
    public int finalMoneyCost; // money
    public SkillData[] prerequisites; 
    public bool isUnlocked = false; 
    public float moneyGenerationRate = 1f;
    public float researchGenerationRate = 1f; 
    public float moneyCostReduction = 1f;
    public float researchCostReduction = 1f;
    public float policyCostReduction = 1f;
}
