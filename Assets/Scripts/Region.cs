using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    public RegionData data;
    // public Coroutine refreshCoroutine;

    private void OnMouseDown()
    {
        if (GameManager.Instance.isTabOpen)
            return;
            
        Debug.Log("Selected Region: " + data.regionName);
        RegionInfoPanel.Instance.OpenPanel(data);
    }

    public void Refresh()
    {
        if (data.health <= 5){
            Debug.Log("Region " + data.regionName + " is in critical condition.");
            data.isCritical = true;
        }
        if (data.health <= 0){
            data.isDead = true;
            return;
        }

        data.economy += data.economyChangeRate;  Random.Range(-(100-data.stability), (100-data.stability));
        data.education += data.educationChangeRate;  Random.Range(-(100-data.stability), (100-data.stability));
        data.stability += data.stabilityChangeRate;
        data.compliance += data.complianceChangeRate;
        data.health += data.healthChangeRate / 10f;

        // Clamp values to 0-100 range
        data.economy = Mathf.Clamp(data.economy, 0, 100);
        data.education = Mathf.Clamp(data.education, 0, 100);
        data.stability = Mathf.Clamp(data.stability, 0, 100);
        data.compliance = Mathf.Clamp(data.compliance, 0, 100);
        data.health = Mathf.Clamp(data.health, 0, 100);
    }

    public RegionSaveData SaveState()
    {
        RegionSaveData regionSave = new RegionSaveData();
        regionSave.regionName = data.regionName;
        regionSave.challengeName = data.challengeName;
        regionSave.economy = data.economy;
        regionSave.education = data.education;
        regionSave.stability = data.stability;
        regionSave.compliance = data.compliance;
        regionSave.health = data.health;
        regionSave.economyChangeRate = data.economyChangeRate;
        regionSave.educationChangeRate = data.educationChangeRate;
        regionSave.stabilityChangeRate = data.stabilityChangeRate;
        regionSave.complianceChangeRate = data.complianceChangeRate;
        regionSave.healthChangeRate = data.healthChangeRate;

        regionSave.actions = new List<ActionSaveData>();
        foreach (ActionData action in data.actions)
        {
            regionSave.actions.Add(ActionManager.Instance.SaveState(action));
        }
        
        return regionSave;
    }

    public void LoadState(RegionSaveData regionSave)
    {
        data.regionName = regionSave.regionName;
        data.challengeName = regionSave.challengeName;
        data.economy = regionSave.economy;
        data.education = regionSave.education;
        data.stability = regionSave.stability;
        data.compliance = regionSave.compliance;
        data.health = regionSave.health;
        data.economyChangeRate = regionSave.economyChangeRate;
        data.educationChangeRate = regionSave.educationChangeRate;
        data.stabilityChangeRate = regionSave.stabilityChangeRate;
        data.complianceChangeRate = regionSave.complianceChangeRate;
        data.healthChangeRate = regionSave.healthChangeRate;

        data.actions = new List<ActionData>();
        foreach (ActionSaveData actionSave in regionSave.actions)
        {
            ActionData action = ActionManager.Instance.LoadState(actionSave);
            if (action != null)
            {
                data.actions.Add(action);
            }
        }
    }


}
