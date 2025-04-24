using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 0;
    public int research = 0;
    [Range(0, 100)] public float globalHealth = 100;
    public float moneyGenerationMultiplier = 1f; // baseRate * multiplier
    public float researchGenerationMultiplier = 1f;
    public float moneyCostMultiplier = 1f; // baseCost * reduction
    public float researchCostMultiplier = 1f;
    public float policyCostMultiplier = 1f;

    public bool isTabOpen = false;
    public List<Region> allRegions;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // ensure one instance only (singleton)
    }
    
    void Start()
    {
        allRegions = new List<Region>(FindObjectsOfType<Region>());
        StartCoroutine(Tick());
    }   

    IEnumerator Tick()
    {
        yield return new WaitUntil(() => 
            DataPanel.Instance != null 
            && SkillsPanel.Instance != null);
        
        while (Instance != null)
        {
            foreach (Region region in allRegions)
            {
                region.Refresh();
            }

            OnTick();

            GameInfoPanel.Instance.Refresh(Instance);
            DataPanel.Instance.Refresh();
            SkillsPanel.Instance.Refresh();
            yield return new WaitForSeconds(2f);
        }
    }

    public void OnTick()
    {
        
        globalHealth = 0f;
        foreach (Region region in allRegions)
        {
            
            money += (int) (region.data.economy * region.data.compliance / 100f); // compliance to tax
            research += (int) (region.data.education * region.data.stability / 100f); // stability for research

            globalHealth += region.data.health;
        }
        globalHealth /= allRegions.Count;

        money *= (int) moneyGenerationMultiplier;
        research *= (int) researchGenerationMultiplier;

    }

    public GameSaveData SaveState()
    {
        GameSaveData gameSave = new GameSaveData();
        gameSave.money = money;
        gameSave.research = research; 
        gameSave.globalHealth = globalHealth;

        gameSave.moneyGenerationMultiplier = moneyGenerationMultiplier;
        gameSave.researchGenerationMultiplier = researchGenerationMultiplier;
        gameSave.moneyCostMultiplier = moneyCostMultiplier;
        gameSave.researchCostMultiplier = researchCostMultiplier;
        gameSave.policyCostMultiplier = policyCostMultiplier;

        gameSave.regions = new List<RegionSaveData>();
        foreach (Region region in allRegions)
        {
            gameSave.regions.Add(region.SaveState());
        }

        gameSave.skills = new List<SkillSaveData>();
        foreach (SkillData skill in SkillManager.Instance.allSkills)
        {
            gameSave.skills.Add(SkillManager.Instance.SaveState(skill));
        }

        return gameSave;
    }

    public void LoadState(GameSaveData gameSave)
    {
        money = gameSave.money;
        research = gameSave.research;
        globalHealth = gameSave.globalHealth;

        moneyGenerationMultiplier = gameSave.moneyGenerationMultiplier;
        researchGenerationMultiplier = gameSave.researchGenerationMultiplier;
        moneyCostMultiplier = gameSave.moneyCostMultiplier;
        researchCostMultiplier = gameSave.researchCostMultiplier;
        policyCostMultiplier = gameSave.policyCostMultiplier;

        foreach (RegionSaveData regionSave in gameSave.regions)
        {
            foreach (Region region in allRegions)
            {
                if (region.data.regionName == regionSave.regionName)
                {
                    region.LoadState(regionSave);
                    break;
                }
            }
        }

        SkillManager.Instance.allSkills = new List<SkillData>();
        foreach  (SkillSaveData skillSave in gameSave.skills)
        {
            SkillData skill = SkillManager.Instance.LoadState(skillSave);
            if (skill != null)
            {
                SkillManager.Instance.allSkills.Add(skill);
            }
            
        }
    }
}
