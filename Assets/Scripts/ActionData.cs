using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Region Action", menuName = "Region Action")]
public class ActionData : ScriptableObject
{
    public string actionName;
    public string description;
    // public List<RegionData> targetRegions;
    public ActionData[] prerequisites; 

    [Range(0, 100)] public int economyChange; // affects local economy change rate
    [Range(0, 100)] public int educationChange; // affects local change rate
    [Range(0, 100)] public int stabilityChange;
    [Range(0, 100)] public int complianceChange;
    [Range(0, 100)] public float healthChange;

    public int baseResearchCost; // research points
    public int baseMoneyCost; // money
    public int finalResearchCost; // research points
    public int finalMoneyCost; // money
    public bool isUnlocked = false;

}
