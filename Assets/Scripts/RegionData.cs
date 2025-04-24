using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Region", menuName = "Region Data")]
public class RegionData : ScriptableObject
{
    public string regionName;
    public string challengeName; // unique regional problem
    public List<ActionData> actions;
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
    public bool isCritical = false;
    public bool isDead = false;
    //

}
