using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Region", menuName = "Region Data")]
public class RegionData : ScriptableObject
{
    public string regionName;
    public string challengeName; // unique regional problem
    public List<ActionData> actions;
    public List<ActionData> actionsAI; // actions that must be completed before this one can be taken
    
    [Range(0, 100)] public float economy; // higher money generation;
    [Range(0, 100)] public float tax; // local contribution to global economy;
    [Range(0, 100)] public float education; // higher research output;
    [Range(0, 100)] public float stability; // randomness multiplier; affects reliability of region statistics; frequency random events
    [Range(0, 100)] public float compliance; // policy effectiveness, positives/negatives of actions amplified
    [Range(0, 100)] public float happiness; // AI FSM state
    [Range(0, 100)] public float health; // affects global health

    [Range(-10, 10)] public float economyChangeRate; // affects local money generation#
    [Range(-10, 10)] public float taxChangeRate; // affects local contribution to global economy
    [Range(-10, 10)] public float educationChangeRate; // affects max research generation
    [Range(-10, 10)] public float stabilityChangeRate; // affects research generation
    [Range(-10, 10)] public float complianceChangeRate; // affects effectiveness of policies and money gen
    [Range(-10, 10)] public float happinessChangeRate;
    [Range(-10, 10)] public float healthChangeRate; // affects global health
    public bool isCritical = false;
    public bool isDead = false;

}
