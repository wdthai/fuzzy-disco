using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Region", menuName = "Region Data")]
public class RegionData : ScriptableObject
{
    public string regionName;
    [Range(0, 5)] public int wealthTier;
    [Range(0, 100)] public int education;
    [Range(0, 100)] public int stability;
    [Range(0, 100)] public int compliance;
    [Range(0, 100)] public float sustainability;
}
