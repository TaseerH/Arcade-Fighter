using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreUnlocks : MonoBehaviour
{

    public Button Health = null;

    public Button coinBtn;
    public Button packBtn;
    public Button bundleBtn;

    public Sprite coinBtnActiveSprite;
    public Sprite packBtnActiveSprite;
    public Sprite bundlebtnActiveSprite;

    public Sprite coinBtnInActiveSprite;
    public Sprite packBtnInActiveSprite;
    public Sprite bundlebtnInActiveSprite;

    public GameObject coinPanel;
    public GameObject packPanel;
    public GameObject bundlePanel;

    public GameObject purchaseSuccess;

    

    private void Start()
    {
        CoinPacks();
        PlayerPrefs.SetInt("freshcoin", 1);
        //PlayerPrefs.SetInt("freshinstall", 1);
    }

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

    public void CoinPacks()
    {
        coinPanel.SetActive(true); packPanel.SetActive(false); bundlePanel.SetActive(false);
        coinBtn.image.sprite = coinBtnActiveSprite;
        packBtn.image.sprite = packBtnInActiveSprite;
        bundleBtn.image.sprite = bundlebtnInActiveSprite;
    }
    public void BundlePacks()
    {
        coinPanel.SetActive(false); packPanel.SetActive(false); bundlePanel.SetActive(true);
        coinBtn.image.sprite = coinBtnInActiveSprite;
        packBtn.image.sprite = packBtnInActiveSprite;
        bundleBtn.image.sprite = bundlebtnActiveSprite;
    }
    public void PackPacks()
    {
        coinPanel.SetActive(false); packPanel.SetActive(true); bundlePanel.SetActive(false);
        coinBtn.image.sprite = coinBtnInActiveSprite;
        packBtn.image.sprite = packBtnActiveSprite;
        bundleBtn.image.sprite = bundlebtnInActiveSprite;
    }

    public void CoinPackBuy(int coins)
    {
        purchaseSuccess.SetActive(true);
        PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + coins);
    }

    public void unlockAllChacracters()
    {
        purchaseSuccess.SetActive(true);
        PlayerPrefs.SetInt("allCharactersUnlocked", 1);
        //unlockAllChacractersScript.unlockAllCharacters();
    }

    public void unlockAllLevels()
    {
        purchaseSuccess.SetActive(true);
        PlayerPrefs.SetInt("allLevelsUnlocked", 1);
        //unlockAllLevelsScript.unlockAllLevels();
    }
    
    public void CoinPackOkay()
    {
        purchaseSuccess.SetActive(false);
    }
}
