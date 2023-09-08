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


    public TMP_Text healthPrice;
    public TMP_Text staminaPrice;

    public GameObject storePage;

    // Start is called before the first frame update
    void Start()
    {
        StatsSetup();
    }

    public void StorePage()
    {
        storePage.SetActive(true);
    }


    private void StatsSetup()
    {

        Debug.Log("Stamina is: " + PlayerPrefs.GetInt("playerStamina"));



        healthPrice.text = $"{1000 + PlayerPrefs.GetInt("healthinc")}";
        staminaPrice.text = $"{1000 + PlayerPrefs.GetInt("staminc")}";
        playerHealth.text = $"{(PlayerPrefs.GetInt("Health") / 100) + (UFE.config.player1Character.lifePoints / 100)}";
        playerStamina.text = $"{PlayerPrefs.GetInt("playerStamina") + (UFE.config.player1Character.maxGaugePoints / 100) + 5}";
        
        if (PlayerPrefs.GetInt("selectedLevel") > 20)
        {
            enemyStamina.text = $"{UFE.config.player2Character.maxGaugePoints / 100 + PlayerPrefs.GetInt("selectedLevel") - 10}";
            enemyHealth.text = $"{UFE.config.player2Character.lifePoints / 100 + 10}";
        }
        else
        {
            enemyStamina.text = $"{UFE.config.player2Character.maxGaugePoints / 100 + PlayerPrefs.GetInt("selectedLevel")}";
            enemyHealth.text = $"{UFE.config.player2Character.lifePoints / 100 + 5}";
        }
        
    }

    public void BuyHealth()
    {
        if(PlayerPrefs.GetInt("coin") >=(1000 + PlayerPrefs.GetInt("healthinc")) && PlayerPrefs.GetInt("Health") <= 1500) {
            
            PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") + 100);
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - (1000 + PlayerPrefs.GetInt("healthinc")));
            
            PlayerPrefs.SetInt("healthinc", PlayerPrefs.GetInt("healthinc") + 100);
        }
        StatsSetup();
    }

    public void BuyStamina()
    { 
        if (PlayerPrefs.GetInt("coin") >= (1000 + PlayerPrefs.GetInt("staminc")) && PlayerPrefs.GetInt("playerStamina") <= 10)
        {
            
            PlayerPrefs.SetInt("playerStamina", PlayerPrefs.GetInt("playerStamina") + 1);
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - (1000 + PlayerPrefs.GetInt("staminc")));
            
            PlayerPrefs.SetInt("staminc", PlayerPrefs.GetInt("staminc") + 100);
        }
        StatsSetup();
    }
}
