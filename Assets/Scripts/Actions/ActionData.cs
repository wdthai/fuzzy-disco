using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Region Action", menuName = "Region Action")]
public class ActionData : ScriptableObject
{
    public string actionName;
    public string description;
    public bool isDirect = false; // false affects ChangeRate, true affects direct values

    [Range(-100, 100)] public float economyChange; // affects local economy change rate
    [Range(-100, 100)] public float taxChange; // affects local contribution to global economy
    [Range(-100, 100)] public float educationChange; // affects local change rate
    [Range(-100, 100)] public float stabilityChange;
    [Range(-100, 100)] public float complianceChange;
    [Range(-100, 100)] public float happinessChange;
    [Range(-100, 100)] public float healthChange;

    public float baseResearchCost; // research points
    public float baseMoneyCost; // money
    public float finalResearchCost; // research points
    public float finalMoneyCost; // money
    public int ticksToComplete;
    public bool isUnlocked = false;

}
