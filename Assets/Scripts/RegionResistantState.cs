using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionResistantState : RegionBaseState
{
    public float taxDifference = 15f;
    public float complianceDifference = 15f;
    public float aggression = 0.15f;
    public ActionData nextAction;
    public override void Enter(RegionAI regionAI)
    {
        ConsoleManager.Instance.AddEntry($"{regionAI.region.data.regionName} has become Resistant");
    }
    public override void Tick(RegionAI regionAI)
    {
        if (regionAI.region.data.happiness >= 35)
        {
            regionAI.region.data.tax += taxDifference;
            regionAI.region.data.compliance += complianceDifference;
            regionAI.ChangeState(regionAI.contentState);
            return;
        } 
        else if (regionAI.region.data.happiness < 15)
        {
            regionAI.region.data.tax -= taxDifference;
            regionAI.region.data.compliance -= complianceDifference;
            regionAI.ChangeState(regionAI.rebelliousState);
            return;
        }

        regionAI.currentPriority = regionAI.EvaluatePriority();
        ActionData nextAction = regionAI.GetBestAction();

        if (Random.Range(0f, 1f) < aggression)
        {
            ActionManager.Instance.ExecuteAction(nextAction, regionAI.region.data, true);
        }
    }
    public override void Exit(RegionAI regionAI){}
}
