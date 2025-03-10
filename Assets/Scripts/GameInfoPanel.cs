using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameInfoPanel : MonoBehaviour
{
    public static GameInfoPanel Instance;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI researchText;
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    public void ShowGameInfo(GameManager game)
    {
        moneyText.text = "Money: " + game.money;
        researchText.text = "Research: " + game.research;
        healthText.text = "Health: " + game.globalHealth + "%";
    }
}
