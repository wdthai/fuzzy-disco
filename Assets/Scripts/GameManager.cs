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
    public List<Region> Regions;

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
            Regions = new List<Region>(FindObjectsOfType<Region>());
            GameSaveData gameSave = new GameSaveData();
            LoadState(gameSave);
        }
        else
        {
            Regions = new List<Region>(FindObjectsOfType<Region>());
            GameSaveData gameSave = SaveManager.Load();
            LoadState(gameSave);
        }

        StartCoroutine(OnTick());
    }   

    IEnumerator OnTick()
    {
        yield return new WaitUntil(() => 
            DataPanel.Instance != null 
            && SkillsPanel.Instance != null 
            && GameInfoPanel.Instance != null);
        
        while (Instance != null)
        {
            ConsoleManager.Instance.AddEntry($"Day {tickCount}");
            foreach (Region region in Regions)
                region.Refresh();

            Tick();
            CheckWin();
            CheckLose();

            GameInfoPanel.Instance.Refresh(Instance);
            DataPanel.Instance.Refresh();
            SkillsPanel.Instance.Refresh();

            yield return new WaitForSeconds(2f);
        }
    }

    public void Tick()
    {
        // Debug.Log($"[TICK] Global health: {globalHealth} Money: {money}  Research: {research}"); // Requirement 1
        float moneyIncome = 0;
        float researchIncome = 0;
        globalHealth = 0f;
        foreach (Region region in Regions)
        {
            moneyIncome += 
            (
                region.data.economy 
                * (region.data.tax / 100f)
                * (Random.Range(100f - region.data.stability, 100f + region.data.stability) /100f)
            );
            researchIncome += 
            (
                region.data.education
                * (Random.Range(100f - region.data.stability, 100f + region.data.stability) /100f)
            ); 

            globalHealth += region.data.health;
        }

        moneyIncome /= Regions.Count;
        researchIncome /= Regions.Count;
        globalHealth /= Regions.Count;

        // Debug.Log($"Base moneyIncome: {moneyIncome} moneyGenerationMultiplier: {moneyGenerationMultiplier}\nMoney Income: {moneyIncome * moneyGenerationMultiplier}");
        // Debug.Log($"Base researchIncome: {researchIncome} researchGenerationMultiplier: {researchGenerationMultiplier}\nResearch Income: {researchIncome * researchGenerationMultiplier}");
        // Debug.Log($"Tick {tickCount} Money: {money} Research: {research} moneyIncome: {moneyIncome} researchIncome: {researchIncome}");
        // Debug.Log($"Global health: {globalHealth}");
        money += moneyIncome * moneyGenerationMultiplier;
        research += researchIncome * researchGenerationMultiplier;
        // Debug.Log($"Tick {tickCount} Money: {money} Research: {research}");
    }

    public void CheckWin()
    {
        tickCount += 1f;
        if (globalHealth >= 100)
        {
            daysUntilWin -= 1f;
            ConsoleManager.Instance.AddEntry($"Day {tickCount}: {25f - daysUntilWin} consecutive days above 100% global health.");
            Debug.Log($"Day {tickCount}: {25f - daysUntilWin} consecutive days above 100% global health.");
            if (daysUntilWin <= 0)
            {
                Time.timeScale = 0f; // stop the game
                Debug.Log($"Win condition met: {25f - daysUntilWin} consecutive days above 100% global health. {tickCount} days played.");
                ConsoleManager.Instance.AddEntry("Breaking News: The planet is saved!");
            }
        }
        else 
        {
            daysUntilWin = 25f;
        }
    }

    public void CheckLose()
    {
        if (globalHealth <= 0)
        {
            Time.timeScale = 0f; // stop the game
            Debug.Log($"Lose condition met: Global health is 0%. {tickCount} days played.");
            ConsoleManager.Instance.AddEntry("Breaking News: The planet is dead!");
        }
    }

    public GameSaveData SaveState()
    {
        GameSaveData gameSave = new GameSaveData();
        gameSave.money = money;
        gameSave.research = research; 
        gameSave.globalHealth = globalHealth;
        gameSave.tickCount = tickCount;
        gameSave.daysUntilWin = daysUntilWin;

        gameSave.moneyGenerationMultiplier = moneyGenerationMultiplier;
        gameSave.researchGenerationMultiplier = researchGenerationMultiplier;
        gameSave.moneyCostMultiplier = moneyCostMultiplier;
        gameSave.researchCostMultiplier = researchCostMultiplier;
        gameSave.policyCostMultiplier = policyCostMultiplier;

        gameSave.regions = new List<RegionSaveData>();
        foreach (Region region in Regions)
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
        daysUntilWin = gameSave.daysUntilWin;

        moneyGenerationMultiplier = gameSave.moneyGenerationMultiplier;
        researchGenerationMultiplier = gameSave.researchGenerationMultiplier;
        moneyCostMultiplier = gameSave.moneyCostMultiplier;
        researchCostMultiplier = gameSave.researchCostMultiplier;
        policyCostMultiplier = gameSave.policyCostMultiplier;

        foreach (RegionSaveData regionSave in gameSave.regions)
        {
            foreach (Region region in Regions)
            {
                if (region.data.regionName == regionSave.regionName)
                {
                    region.LoadState(regionSave);
                    break;
                }
            }
        }

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
