using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float money = 0;
    public float research = 0;
    public float globalHealth = 100;
    public float tickCount = 0f;
    public float daysUntilWin = 25f;
    
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
        if (SaveManager.isNewGame)
        {
            allRegions = new List<Region>(FindObjectsOfType<Region>());
            GameSaveData gameSave = new GameSaveData();
            LoadState(gameSave);
        }
        else
        {
            allRegions = new List<Region>(FindObjectsOfType<Region>());
            GameSaveData gameSave = SaveManager.Load();
            LoadState(gameSave);
        }

        StartCoroutine(OnTick());
    }   

    IEnumerator OnTick()
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

            Tick();
            CheckWin();

            GameInfoPanel.Instance.Refresh(Instance);
            DataPanel.Instance.Refresh();
            SkillsPanel.Instance.Refresh();

            

            yield return new WaitForSeconds(2f);
        }
    }

    public void Tick()
    {
        
        float moneyIncome = 0;
        float researchIncome = 0;
        globalHealth = 0f;
        foreach (Region region in allRegions)
        {
            moneyIncome += 
            (
                region.data.economy 
                * (region.data.tax / 100f)
                * (Random.Range(100f - (100-region.data.stability), 100f + (100-region.data.stability)) / 100f)
            );
            researchIncome += 
            (
                region.data.education
                * (Random.Range(1f - (100-region.data.stability) / 100f, 1f + (100-region.data.stability) / 100f))
            ); 

            globalHealth += region.data.health;
        }

        moneyIncome /= allRegions.Count;
        researchIncome /= allRegions.Count;
        globalHealth /= allRegions.Count;

        money += moneyIncome * moneyGenerationMultiplier;
        research += researchIncome * researchGenerationMultiplier;
    }

    public void CheckWin()
    {
        tickCount += 1f;
        if (globalHealth >= 50)
        {
            daysUntilWin -= 1f;
            ConsoleManager.Instance.AddEntry($"Day {tickCount}: {25f - daysUntilWin} consecutive days above 50% global health.");
            if (daysUntilWin <= 0)
            {
                Time.timeScale = 0f; // stop the game
                ConsoleManager.Instance.AddEntry("Breaking News: The planet is saved!");
            }
        }
        else 
        {
            daysUntilWin = 25f;
            ConsoleManager.Instance.AddEntry($"Day {tickCount}");
        }
    }

    public GameSaveData SaveState()
    {
        GameSaveData gameSave = new GameSaveData();
        gameSave.money = money;
        gameSave.research = research; 
        gameSave.globalHealth = globalHealth;
        gameSave.tickCount = tickCount;

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
        tickCount = gameSave.tickCount;

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

        // SkillManager.Instance.allSkills = new List<SkillData>();
        foreach  (SkillSaveData skillSave in gameSave.skills)
        {
            foreach (SkillData skill in SkillManager.Instance.allSkills)
            {
                if (skill.skillName == skillSave.skillName)
                {
                    SkillManager.Instance.LoadState(skill, skillSave);
                    break;
                }
            }
        }

        Tick();
    }
}
