using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData 
{
    public float money;
    public float research;
    public float globalHealth;
    public float tickCount;
    public float daysUntilWin; // days until win condition is met, set to 0 if no win condition is set
    public float moneyGenerationMultiplier; // baseRate * multiplier
    public float researchGenerationMultiplier;
    public float moneyCostMultiplier; // baseCost * reduction
    public float researchCostMultiplier;
    public float policyCostMultiplier;

    public List<RegionSaveData> regions;
    public List<SkillSaveData> skills;

    public GameSaveData()
    {
        money = 0f;
        research = 0f;
        globalHealth = 100f;
        tickCount = 0f;
        daysUntilWin = 25f;
        moneyGenerationMultiplier = 1f;
        researchGenerationMultiplier = 1f;
        moneyCostMultiplier = 1f;
        researchCostMultiplier = 1f;
        policyCostMultiplier = 1f;

        regions = new List<RegionSaveData>();
        skills = new List<SkillSaveData>();

        Region[] allRegions = GameObject.FindObjectsOfType<Region>();
        foreach (Region region in allRegions)
        {
            regions.Add(region.SaveState());
        }

        foreach (SkillData skill in SkillManager.Instance.allSkills)
        {
            skills.Add(SkillManager.Instance.SaveState(skill));
        }

    }
}

[System.Serializable]
public class RegionSaveData
{
    public string regionName;
    public string challengeName; // unique regional problem
    
    public float economy; // affects global money statistic
    public float tax; // affects global money statistic
    public float education; // research
    public float stability; // research multipliter
    public float compliance; // money and policy effectiveness
    public float happiness; // AI FSM state
    public float health; // affects global health

    public float economyChangeRate; // affects local money generation
    public float taxChangeRate; // affects local contribution to global economy
    public float educationChangeRate; // affects max research generation
    public float stabilityChangeRate; // affects research generation
    public float complianceChangeRate; // affects effectiveness of policies and money gen
    public float happinessChangeRate; // affects AI FSM state
    public float healthChangeRate; // affects global health
    public List<ActionSaveData> actions;
    public List<ActionSaveData> actionsAI; // actions that must be completed before this one can be taken

}

[System.Serializable]
public class SkillSaveData
{
    public string skillName;
    public string description;

    // global modifiers
    public float moneyGenerationModifier = 1f;
    public float researchGenerationModifier = 1f;
    public float moneyCostModifier = 1f;
    public float researchCostModifier = 1f;
    public float policyCostModifier = 1f;

    public float baseResearchCost; // research points
    public float baseMoneyCost; // money
    public float finalResearchCost; // research points
    public float finalMoneyCost; // money
    public int ticksToComplete;
    public bool isUnlocked = false;

}

[System.Serializable]
public class ActionSaveData
{
    public string actionName;
    public string description;
    public bool isDirect = false; // true affects ChangeRate, false affects direct values

     public float economyChange; // affects local economy change rate
     public float taxChange; // affects local contribution to global economy
     public float educationChange; // affects local change rate
     public float stabilityChange;
     public float complianceChange;
     public float happinessChange; // affects AI FSM state
     public float healthChange;

    public float baseResearchCost; // research points
    public float baseMoneyCost; // money
    public float finalResearchCost; // research points
    public float finalMoneyCost; // money
    public int ticksToComplete;
    public bool isUnlocked = false;
}

