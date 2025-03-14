using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 0;
    public int research = 0;
    [Range(0, 100)] public float globalHealth = 100;

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
        StartCoroutine(AutoRefreshPanels());
    }   

    IEnumerator AutoRefreshPanels()
    {
        yield return new WaitUntil(() => 
            DataPanel.Instance != null 
            && SkillsPanel.Instance != null);
        
        while (true)
        {
            GameInfoPanel.Instance.Refresh(Instance);

            // if (DataPanel.Instance)
            DataPanel.Instance.Refresh();
            SkillsPanel.Instance.Refresh();

            yield return new WaitForSeconds(2f);
        }
    }
}
