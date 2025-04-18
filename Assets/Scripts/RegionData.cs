using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Region", menuName = "Region Data")]
public class RegionData : ScriptableObject
{
    public string regionName;
    public List<ActionData> actions;
    [Range(0, 100)] public int localEconomy; // affects max money generation
    [Range(0, 100)] public int localEducation; // affects max research generation
    [Range(0, 100)] public int localStability; // affects range of money/research generation
    [Range(0, 100)] public int localCompliance; // affects effectiveness of policies
    public string localChallenge; // unique regional challenge
    [Range(0, 100)] public float localHealth; // affects global health
}
