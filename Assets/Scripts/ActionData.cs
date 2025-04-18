using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Region Action", menuName = "Region Action")]
public class ActionData : ScriptableObject
{
    public string actionName;
    public string description;
    // public List<RegionData> targetRegions;

    [Range(0, 100)] public int economyChange; // affects max money generation
    [Range(0, 100)] public int educationChange; // affects max research generation
    [Range(0, 100)] public int stabilityChange; // affects range of money/research generation
    [Range(0, 100)] public int complianceChange; // affects effectiveness of policies
    [Range(0, 100)] public float healthChange; // affects global health

    public int baseResearchCost; // research points
    public int baseMoneyCost; // money
    public int finalResearchCost; // research points
    public int finalMoneyCost; // money
    public bool isUnlocked = false;

}
