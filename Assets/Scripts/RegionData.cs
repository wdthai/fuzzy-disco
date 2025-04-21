using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Region", menuName = "Region Data")]
public class RegionData : ScriptableObject
{
    public string regionName;
    public string challengeName; // unique regional problem
    public List<ActionData> actions;
    [Range(0, 100)] public int economy; // local region statistic
    [Range(0, 100)] public int education; 
    [Range(0, 100)] public int stability; 
    [Range(0, 100)] public int compliance; 
    [Range(0, 100)] public float health; 
    [Range(-100, 100)] public int economyChangeRate; // affects local money generation
    [Range(-100, 100)] public int educationChangeRate; // affects max research generation
    [Range(-100, 100)] public int stabilityChangeRate; // affects range of money/research generation
    [Range(-100, 100)] public int complianceChangeRate; // affects effectiveness of policies
    [Range(-100, 100)] public float healthChangeRate; // affects global health
}
