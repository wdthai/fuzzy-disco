using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Region", menuName = "Region Data")]
public class RegionData : ScriptableObject
{
    public string regionName;
    [Range(0, 5)] public int wealthTier; // affects max money generation
    [Range(0, 100)] public int education; // affects max research generation
    [Range(0, 100)] public int stability; // affects range of money/research generation
    [Range(0, 100)] public int compliance; // affects effectiveness of policies
    [Range(0, 100)] public float sustainability; // affects global health
}
