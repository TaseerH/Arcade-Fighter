using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreUnlocks : MonoBehaviour
{

    public Button Health = null;

    public void BuyHealth()
    {
        //Debug.Log(health.text + character.text);
        if (PlayerPrefs.GetInt("Health") <= 1500 && PlayerPrefs.GetInt("coin") >= 1000)
        {
            PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") + 100);
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - 1000);

        }
        else
        {
            Health.interactable = (false);
        }

    }
  
}
