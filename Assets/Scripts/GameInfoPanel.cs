using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameInfoPanel : MonoBehaviour
{
    public static GameInfoPanel Instance;
    public TextMeshProUGUI moneyText, researchText, healthText;

    public Button dataButton, skillButton;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        dataButton.onClick.AddListener(() => DataPanel.Instance.OpenPanel());
        skillButton.onClick.AddListener(() => SkillsPanel.Instance.OpenPanel());
    }

    public void Refresh(GameManager game)
    {
        moneyText.text = $"Money: {game.money.ToString("F0")}" ;
        researchText.text = $"Research: {game.research.ToString("F0")}" ;
        healthText.text = $"Health: {game.globalHealth.ToString("F2")}%";
    }
}
