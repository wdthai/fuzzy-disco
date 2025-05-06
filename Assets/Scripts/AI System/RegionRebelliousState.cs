using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionRebelliousState : RegionBaseState
{
    public float taxDifference = 10f;
    public float complianceDifference = 10f;
    public float aggression = 10f;
    public ActionData nextAction;

    public override void Enter(RegionAI regionAI)
    {
        ConsoleManager.Instance.AddEntry($"{regionAI.region.data.regionName} has become Rebellious");
    }
    public override void Tick(RegionAI regionAI)
    {
        if (regionAI.region.data.happiness >= 15)
        {
            regionAI.region.data.tax += taxDifference;
            regionAI.region.data.compliance += complianceDifference;
            regionAI.ChangeState(regionAI.resistantState);
            return;
        }

        regionAI.currentPriority = regionAI.EvaluatePriority();
        nextAction = regionAI.GetBestAction();

        if (Random.Range(0f, 100f) < aggression)
        {
            ActionManager.Instance.ExecuteAction(nextAction, regionAI.region.data, true);
        }
        
    }
    public override void Exit(RegionAI regionAI){}
}
