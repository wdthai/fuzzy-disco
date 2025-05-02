using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    public RegionData original;
    public RegionData data;
    public RegionAI ai;

    void Awake()
    {
        ai = GetComponent<RegionAI>();
        data = Instantiate(original);
        foreach (ActionData action in data.actionsOriginal)
        {
            ActionData newAction = ScriptableObject.Instantiate(action);
            data.actions.Add(newAction);
        }
        foreach (ActionData action in data.actionsAIOriginal)
        {
            ActionData newAction = ScriptableObject.Instantiate(action);
            data.actionsAI.Add(newAction);
        }
    }
    void Start() { ai.region = this;  }

    private void OnMouseDown()
    {
        if (GameManager.Instance.isTabOpen)
            return;
            
        // Debug.Log("Selected Region: " + data.regionName);
        RegionInfoPanel.Instance.OpenPanel(data);
    }

    public void killRegion(){
        if (data.isDead) return;

        data.isDead = true;

        data.economyChangeRate = -50;
        data.taxChangeRate = -50;
        data.educationChangeRate = -50;
        data.stabilityChangeRate = -50;
        data.complianceChangeRate = -50;
        data.happinessChangeRate = -50;
        data.healthChangeRate = -50;
    }

    public void Refresh()
    {
        if (data.health <= 0){
            killRegion();
            return;
        }

        if (data.health <= 5){
            Debug.Log("Region " + data.regionName + " is in critical condition.");
            data.isCritical = true;
        }
    
        data.economy += (data.economyChangeRate / 10f) * Random.Range(1f - (100-data.stability) / 100f, 1f + (100-data.stability) / 100f);
        data.tax += (data.economyChangeRate / 10f) * Random.Range(1f - (100-data.stability) / 100f, 1f + (100-data.stability) / 100f);
        data.education += (data.educationChangeRate / 10f) * Random.Range(1f - (100-data.stability) / 100f, 1f + (100-data.stability) / 100f);
        data.stability += (data.stabilityChangeRate / 10f) * Random.Range(1f - (100-data.stability) / 100f, 1f + (100-data.stability) / 100f);
        data.compliance += (data.complianceChangeRate / 10f) * Random.Range(1f - (100-data.stability) / 100f, 1f + (100-data.stability) / 100f);
        data.happiness += (data.happinessChangeRate / 10f) * Random.Range(1f - (100-data.stability) / 100f, 1f + (100-data.stability) / 100f);
        data.health += (data.healthChangeRate / 10f) * Random.Range(1f - (100-data.stability) / 100f, 1f + (100-data.stability) / 100f);

        // Clamp values to 0-100 range
        data.economy = Mathf.Clamp(data.economy, 0, 100);
        data.tax = Mathf.Clamp(data.tax, 0, 100);
        data.education = Mathf.Clamp(data.education, 0, 100);
        data.stability = Mathf.Clamp(data.stability, 0, 100);
        data.compliance = Mathf.Clamp(data.compliance, 0, 100);
        data.happiness = Mathf.Clamp(data.happiness, 0, 100);
        data.health = Mathf.Clamp(data.health, 0, 100);

        data.economyChangeRate = Mathf.Clamp(data.economyChangeRate, -50, 50);
        data.taxChangeRate = Mathf.Clamp(data.taxChangeRate, -50, 50);
        data.educationChangeRate = Mathf.Clamp(data.educationChangeRate, -50, 50);
        data.stabilityChangeRate = Mathf.Clamp(data.stabilityChangeRate, -50, 50);
        data.complianceChangeRate = Mathf.Clamp(data.complianceChangeRate, -50, 50);
        data.happinessChangeRate = Mathf.Clamp(data.happinessChangeRate, -50, 50);
        data.healthChangeRate = Mathf.Clamp(data.healthChangeRate, -50, 50);
    }

    public RegionSaveData SaveState()
    {
        RegionSaveData regionSave = new RegionSaveData();
        regionSave.regionName = data.regionName;
        regionSave.challengeName = data.challengeName;

        regionSave.economy = data.economy;
        regionSave.tax = data.tax;
        regionSave.education = data.education;
        regionSave.stability = data.stability;
        regionSave.compliance = data.compliance;
        regionSave.happiness = data.happiness;
        regionSave.health = data.health;

        regionSave.economyChangeRate = data.economyChangeRate;
        regionSave.taxChangeRate = data.taxChangeRate;
        regionSave.educationChangeRate = data.educationChangeRate;
        regionSave.stabilityChangeRate = data.stabilityChangeRate;
        regionSave.complianceChangeRate = data.complianceChangeRate;
        regionSave.happinessChangeRate = data.happinessChangeRate;
        regionSave.healthChangeRate = data.healthChangeRate;

        regionSave.actions = new List<ActionSaveData>();
        foreach (ActionData action in data.actions)
        {
            regionSave.actions.Add(ActionManager.Instance.SaveState(action));
        }

        regionSave.actionsAI = new List<ActionSaveData>();
        foreach (ActionData action in data.actionsAI)
        {
            regionSave.actionsAI.Add(ActionManager.Instance.SaveState(action));
        }
        
        return regionSave;
    }

    public void LoadState(RegionSaveData regionSave)
    {
        data.regionName = regionSave.regionName;
        data.challengeName = regionSave.challengeName;

        data.economy = regionSave.economy;
        data.tax = regionSave.tax;
        data.education = regionSave.education;
        data.stability = regionSave.stability;
        data.compliance = regionSave.compliance;
        data.happiness = regionSave.happiness;
        data.health = regionSave.health;
        
        data.economyChangeRate = regionSave.economyChangeRate;
        data.taxChangeRate = regionSave.taxChangeRate;
        data.educationChangeRate = regionSave.educationChangeRate;
        data.stabilityChangeRate = regionSave.stabilityChangeRate;
        data.complianceChangeRate = regionSave.complianceChangeRate;
        data.happinessChangeRate = regionSave.happinessChangeRate;
        data.healthChangeRate = regionSave.healthChangeRate;

        foreach (ActionSaveData actionSave in regionSave.actions)
        {
            foreach (ActionData action in data.actions)
            {
                if (action.actionName == actionSave.actionName)
                {
                    ActionManager.Instance.LoadState(action, actionSave);
                    break;
                }
            }
        }

        foreach (ActionSaveData actionSave in regionSave.actionsAI)
        {
            foreach (ActionData action in data.actionsAI)
            {
                if (action.actionName == actionSave.actionName)
                {
                    ActionManager.Instance.LoadState(action, actionSave);
                    break;
                }
            }
        }
    }


}
