using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionContentState : RegionBaseState
{
    public float newTax = 60f; 
    public float newCompliance = 60f;
    public float eventSeverity = 0.5f;
    public float aggression = 0.15f;
    public ActionData nextAction;
    public override void Enter(RegionAI regionAI)
    {
        // Debug.Log(regionAI.region.data.regionName +" Entering Content State");
        if (regionAI.region.data.tax < newTax) regionAI.region.data.tax = newTax;
        if (regionAI.region.data.compliance < newCompliance) regionAI.region.data.compliance = newCompliance;
    }
    public override void Tick(RegionAI regionAI)
    {
        if (regionAI.region.data.happiness >= 80)
        {
            regionAI.ChangeState(regionAI.cooperativeState);
            return;
        } 
        else if (regionAI.region.data.happiness < 50)
        {
            regionAI.ChangeState(regionAI.resistantState);
            return;
        }

        regionAI.currentPriority = regionAI.EvaluatePriority();
        ActionData nextAction = regionAI.GetBestAction();

        if (Random.Range(0f, 1f) < aggression)
        {
            ActionManager.Instance.ExecuteAction(nextAction, regionAI.region.data, true);
        }
    }
    public override void Exit(RegionAI regionAI) {}
}
