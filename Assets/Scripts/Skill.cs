using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public string description;
    public int cost; // research points
    public Skill[] prerequisites; 
    public bool isUnlocked = false; 
}
