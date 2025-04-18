using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    public RegionData data;
    public List<ActionData> actions;
    // public Coroutine refreshCoroutine;

    private void OnMouseDown()
    {
        if (GameManager.Instance.isTabOpen)
            return;
            
        Debug.Log("Selected Region: " + data.regionName);
        RegionInfoPanel.Instance.OpenPanel(data);
    }
}
