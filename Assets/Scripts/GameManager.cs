using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 0;
    public int research = 0;
    [Range(0, 100)] public float globalHealth = 100;

    const float baseMoneyRate = 1f;
    const float baseResearchRate = 1f;
    public float moneyGenerationMultiplier = 1f; // baseRate * multiplier
    public float researchGenerationMultiplier = 1f;
    public float moneyCostMultiplier = 1f; // baseCost * reduction
    public float researchCostMultiplier = 1f;
    public float policyCostMultiplier = 1f;

    public bool isTabOpen = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // ensure one instance only (singleton)
    }
    
    void Start()
    {
        StartCoroutine(Tick());
    }   

    IEnumerator Tick()
    {
        yield return new WaitUntil(() => 
            DataPanel.Instance != null 
            && SkillsPanel.Instance != null);
        
        while (true)
        {
            OnTick();
            GameInfoPanel.Instance.Refresh(Instance);
            DataPanel.Instance.Refresh();
            SkillsPanel.Instance.Refresh();

            yield return new WaitForSeconds(2f);
        }
    }

    public void OnTick()
    {
        money += (int)(baseMoneyRate * moneyGenerationMultiplier);
        research += (int)(baseResearchRate * researchGenerationMultiplier);
        globalHealth -= 0.1f;

    }
}
