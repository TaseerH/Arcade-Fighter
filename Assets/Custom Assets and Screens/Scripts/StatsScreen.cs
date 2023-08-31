using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsScreen : MonoBehaviour
{

    public TMP_Text playerHealth;
    public TMP_Text playerStamina;

    public TMP_Text enemyHealth;
    public TMP_Text enemyStamina;

    // Start is called before the first frame update
    void Start()
    {
        StatsSetup();
    }

    private void StatsSetup()
    {
        playerHealth.text = $"{(PlayerPrefs.GetInt("Health") / 100) + (UFE.config.player1Character.lifePoints / 100)}";
        playerStamina.text = $"{PlayerPrefs.GetInt("playerStamina") + (UFE.config.player1Character.maxGaugePoints / 100) + 5}";
        enemyHealth.text = $"{UFE.config.player2Character.lifePoints / 100 + 5}";
        enemyStamina.text = $"{UFE.config.player2Character.maxGaugePoints / 100 + 4}";
    }

    public void BuyHealth()
    {
        if(PlayerPrefs.GetInt("coin") >=1000 && PlayerPrefs.GetInt("Health") <= 1500) { 
            PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") + 100);
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - 1000);
        }
        StatsSetup();
    }

    public void BuyStamina()
    { 
        if (PlayerPrefs.GetInt("coin") >= 1000 && PlayerPrefs.GetInt("playerStamina") <= 10)
        {
            PlayerPrefs.SetInt("playerStamina", PlayerPrefs.GetInt("playerStamina") + 1);
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - 1000);
        }
        StatsSetup();
    }
}
