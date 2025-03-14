using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 0;
    public int research = 0;
    [Range(0, 100)] public float globalHealth = 100;
    public float baseMoneyRate = 10f;
    public float baseResearchRate = 1f;



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
        DataSkillsPanel.Instance.ClosePanel();
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
        money += (int)(baseMoneyRate * SkillManager.Instance.moneyGenerationRate);
        research += (int)(baseResearchRate * SkillManager.Instance.researchGenerationRate);
        globalHealth -= 0.1f;

    }
}
