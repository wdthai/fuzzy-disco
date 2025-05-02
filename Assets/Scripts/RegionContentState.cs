using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionContentState : RegionBaseState
{
    public float taxDifference = 20f;
    public float complianceDifference = 20f;
    public float aggression = 0.075f;
    public ActionData nextAction;
    public override void Enter(RegionAI regionAI)
    {
        ConsoleManager.Instance.AddEntry($"{regionAI.region.data.regionName} has become Content");
    }
    public override void Tick(RegionAI regionAI)
    {
        if (regionAI.region.data.happiness >= 55)
        {
            regionAI.region.data.tax += taxDifference;
            regionAI.region.data.compliance += complianceDifference;
            regionAI.ChangeState(regionAI.cooperativeState);
            return;
        } 
        else if (regionAI.region.data.happiness < 35)
        {
            regionAI.region.data.tax -= taxDifference;
            regionAI.region.data.compliance -= complianceDifference;
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
