using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData 
{
    public int money;
    public int research;
    public float globalHealth;
    public float moneyGenerationMultiplier; // baseRate * multiplier
    public float researchGenerationMultiplier;
    public float moneyCostMultiplier; // baseCost * reduction
    public float researchCostMultiplier;
    public float policyCostMultiplier;

    public List<RegionSaveData> regions;
    public List<SkillSaveData> skills;
}

[System.Serializable]
public class RegionSaveData
{
    public string regionName;
    public string challengeName; // unique regional problem
    [Range(0, 100)] public int economy; // affects global money statistic
    [Range(0, 100)] public int education; // research
    [Range(0, 100)] public int stability; // research multipliter
    [Range(0, 100)] public int compliance; // money and policy effectiveness
    [Range(0, 100)] public float health; // affects global health
    [Range(-100, 100)] public int economyChangeRate; // affects local money generation
    [Range(-100, 100)] public int educationChangeRate; // affects max research generation
    [Range(-100, 100)] public int stabilityChangeRate; // affects research generation
    [Range(-100, 100)] public int complianceChangeRate; // affects effectiveness of policies and money gen
    [Range(-100, 5)] public int healthChangeRate; // affects global health
    public List<ActionSaveData> actions;
}

[System.Serializable]
public class SkillSaveData
{
    public string skillName;
    public string description;
    public List<string> prerequisites; 

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

[System.Serializable]
public class ActionSaveData
{
    public string actionName;
    public string description;
    // public List<RegionData> targetRegions;
    public List<string> prerequisites;
    public bool isRateChange = false; // true affects ChangeRate, false affects direct values

    [Range(0, 100)] public int economyChange; // affects local economy change rate
    [Range(0, 100)] public int educationChange; // affects local change rate
    [Range(0, 100)] public int stabilityChange;
    [Range(0, 100)] public int complianceChange;
    [Range(0, 100)] public int healthChange;

    public int baseResearchCost; // research points
    public int baseMoneyCost; // money
    public int finalResearchCost; // research points
    public int finalMoneyCost; // money
    public bool isUnlocked = false;
}

