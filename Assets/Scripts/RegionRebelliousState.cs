using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionRebelliousState : RegionBaseState
{
    public float newTax = 20f;
    public float newCompliance = 20f;
    public float eventSeverity = 1.2f;
    public float aggression = 0.3f;
    public ActionData nextAction;

    public override void Enter(RegionAI regionAI)
    {
        // Debug.Log(regionAI.region.data.regionName +" Entering Rebellious State");
        if (regionAI.region.data.tax < newTax) regionAI.region.data.tax = newTax;
        if (regionAI.region.data.compliance < newCompliance) regionAI.region.data.compliance = newCompliance;
    }
    public override void Tick(RegionAI regionAI)
    {
        if (regionAI.region.data.happiness >= 25)
        {
            regionAI.ChangeState(regionAI.resistantState);
            return;
        }

        regionAI.currentPriority = regionAI.EvaluatePriority();
        nextAction = regionAI.GetBestAction();

        if (Random.Range(0f, 1f) < aggression)
        {
            ActionManager.Instance.ExecuteAction(nextAction, regionAI.region.data, true);
        }
        
    }
    public override void Exit(RegionAI regionAI){}
}
