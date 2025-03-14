using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillsPanel : MonoBehaviour
{

    public static SkillsPanel Instance;
    // Start is called before the first frame update
    public GameObject skillsDataPrefab;  // Assign the prefab in the Inspector
    public Transform dataContainer;      // Parent object for instantiated UI elements

    public static List<Skill> skills;
    void Awake()
    {
        Debug.Log("SkillPanel Awake");
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Refresh()
    {
        if (!gameObject.activeSelf) return;

        foreach (Transform child in dataContainer)
        {
            Destroy(child.gameObject);
        }

        skills = SkillManager.Instance.allSkills;

        foreach (Skill skill in SkillManager.Instance.allSkills)
        {
            // Debug.Log("Skill: " + skill.skillName);
            // Instantiate UI prefab inside the dataContainer
            GameObject skillUI = Instantiate(skillsDataPrefab, dataContainer);
            
            // Get references to the TMP fields inside the prefab
            TextMeshProUGUI[] textFields = skillUI.GetComponentsInChildren<TextMeshProUGUI>();
            
            Button button = skillUI.GetComponentInChildren<Button>();
            button.interactable = SkillManager.Instance.isUnlockable(skill);
            button.onClick.AddListener(() => SkillManager.Instance.UnlockSkill(skill));
            // Set data
            textFields[0].text = $"{skill.skillName}";
            textFields[1].text = $"Desc: {skill.description}";
            textFields[2].text = $"Cost: {skill.cost}";
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void OpenPanel()
    {
        Refresh();
        gameObject.SetActive(true);
    }
}

