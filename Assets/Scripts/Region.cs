using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    public RegionData data;

    private void OnMouseDown()
    {
        Debug.Log("Selected Region: " + data.regionName);
        RegionInfoPanel.Instance.ShowRegionInfo(data);
    }
}
