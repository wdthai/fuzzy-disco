using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionAI : MonoBehaviour
{


    public RegionBaseState currentState;
    public RegionCooperativeState cooperativeState = new RegionCooperativeState();
    public RegionContentState contentState = new RegionContentState();
    public RegionResistantState resistantState = new RegionResistantState();
    public RegionRebelliousState rebelliousState = new RegionRebelliousState();


    public enum RegionPriority { Neutral, Economy, Happiness }
    public RegionPriority currentPriority = RegionPriority.Neutral;

    public Region region;

    public float stateCheckCooldown = 30f; // 2 seconds = 1 tick
    public float stateCheckTimer;

    void Start()
    {
        currentState = contentState;
        currentState.Enter(this);
        stateCheckTimer = Random.Range(0f, stateCheckCooldown);
    }

    void Update()
    {
        stateCheckTimer -= Time.deltaTime;
        if (stateCheckTimer <= 0f)
        {
            stateCheckTimer = stateCheckCooldown;
            currentState.Tick(this);
        }
    }

    public void ChangeState(RegionBaseState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public RegionPriority EvaluatePriority()
    {
        // higher utility = more useful
        float economyUtility = 100 - region.data.economy + Random.Range(-25f, 25f);
        float happinessUtility = 100 - region.data.happiness + Random.Range(-25f, 25f);

        float highestUtility = Mathf.Max(economyUtility, happinessUtility);

        if (highestUtility < 40f)
        {
            return RegionPriority.Neutral;
        }
        else if (economyUtility > happinessUtility)
        {
            // Debug.Log($"AI: Low Economy detected in {region.data.regionName}");
            return RegionPriority.Economy;
        }
        else
        {
            // Debug.Log($"AI: Low Happiness detected in {region.data.regionName}");
            return RegionPriority.Happiness;
        }
    }

    public ActionData GetBestAction()
    {
        ActionData bestAction = null;
        float highestUtility = float.MinValue;

        foreach (ActionData action in region.data.actionsAI)
        {
            float currentUtility = 0f;
            switch (currentPriority)
            {
                case RegionPriority.Economy:
                    currentUtility += action.economyChange * 1f;
                    currentUtility -= action.taxChange * 0.6f; 
                    currentUtility += action.happinessChange * 0.3f;
                    break;
                case RegionPriority.Happiness:
                    currentUtility += action.happinessChange * 1.0f;
                    currentUtility += action.economyChange * 0.3f;
                    break;
                case RegionPriority.Neutral:
                    currentUtility += (action.economyChange + action.happinessChange) * 1f;
                    currentUtility += (action.educationChange + action.stabilityChange + action.healthChange) * 0.4f;
                    break;
            }

            float cost = action.baseMoneyCost + action.baseResearchCost;
            if (cost > 0f)
                currentUtility /= cost/2;

            if (ActionManager.Instance.isExecutable(action))
                currentUtility *= 1f; 
            else
                currentUtility *= 0f;
            


            if (currentUtility > highestUtility)
            {
                highestUtility = currentUtility;
                bestAction = action;
            }
        }

        if (bestAction == null || highestUtility == 0f)
        {
            bestAction = GetRandomAction();
        }
        
        return bestAction;
    }

    public ActionData GetRandomAction()
    {
        if (region.data.actionsAI.Count == 0)
            return null;

        int randomIndex = Random.Range(0, region.data.actionsAI.Count);
        return region.data.actionsAI[randomIndex];
    }
 
    
}
