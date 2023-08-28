using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public TMP_Text[] Currency;
    public GameObject PlayButtons;
    public GameObject Characters;
    public GameObject StoryModeCanvas;
    

    public GameObject reviewPanel;
    public GameObject storePage;
    public GameObject Settings;
    public GameObject QuitPanel;
    public Button knockOutMode;

    public GameObject adWatchPanelsuccess;
    public GameObject adWatchPanelfail;

    public Button HealthButton;

    private void FixedUpdate()
    {
        if (Currency == null || PlayButtons == null || StoryModeCanvas == null || knockOutMode == null || reviewPanel == null)
        {
            return;
        }
        Currency[0].SetText(PlayerPrefs.GetInt("coin").ToString());
        Currency[1].SetText(PlayerPrefs.GetInt("coin").ToString());

        if (PlayerPrefs.GetInt("Health") >= 1500)
        {
            HealthButton.interactable = (false);
        }
        else if (PlayerPrefs.GetInt("coin") < 500)
        {
            HealthButton.interactable = (false);
        } else
        {
            HealthButton.interactable = true;
        }

        

    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("freshinstall") == 0)
        {
            PlayerPrefs.SetInt("coin", 500);
            PlayerPrefs.SetInt("KnockOut_Unlock", 0);
        }

        if(PlayerPrefs.GetInt("KnockOut_Unlock") == 1)
        {
            knockOutMode.interactable = true;
        } else
        {
            knockOutMode.interactable = false;
        }

        //Debug.Log(health.text + character.text);
        if (PlayerPrefs.GetInt("Health") >= 1500 || PlayerPrefs.GetInt("coin") < 500)
        {
            HealthButton.interactable = (false);
        }

        


    }

    public void selectLevel(int level)
    {
        PlayerPrefs.SetInt("selectedLevel", level);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void DoFreshInstall()
    {
        PlayerPrefs.SetInt("freshinstall", 0);
    }

    public void addwatch()
    {
        adwatchSuccess();
    }

    public void adwatchSuccess()
    {
        adWatchPanelsuccess.SetActive(true);
        Characters.SetActive(false);
    }

    public void adwatchFail()
    {
        adWatchPanelfail.SetActive(true);
        Characters.SetActive(false);
    }

    public void adFail()
    {
        adWatchPanelfail.SetActive(false);
        Characters.SetActive(true);
    }

    public void BuyCoin()
    {
        PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 5000);
        adWatchPanelsuccess.SetActive(false);
        Characters.SetActive(true);
;    }

    public void GoToCharacterLevelSelection()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void StoryModeScreen()
    {
        PlayButtons.SetActive(false);
        Characters.SetActive(false);
        StoryModeCanvas.SetActive(true);
    }

    public void BackButtonStoryModeScreen()
    {
        PlayButtons.SetActive(true);
        Characters.SetActive(true);
        StoryModeCanvas.SetActive(false);
    }

    public void BackButtonStoreScreen()
    {
        storePage.SetActive(false);
        Characters.SetActive(true);
    }

    public void StorePage()
    {
        storePage.SetActive(true);
        Characters.SetActive(false);
    }

    public void BackButtonSettingsScreen()
    {
        Settings.SetActive(false);
        Characters.SetActive(true);
    }

    public void SettingsPage()
    {
        Settings.SetActive(true);
        Characters.SetActive(false);
    }

    public void QuitPanelActive()
    {
        QuitPanel.SetActive(true);
        Characters.SetActive(false);
    }

    public void QuitPanelDeactive()
    {
        QuitPanel.SetActive(false);
        Characters.SetActive(true);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void moreGames()
    {
        Application.OpenURL("https://play.google.com/store/games");
    }

    public void giveReview()
    {
        reviewPanel.SetActive(true);
        Characters.SetActive(false);
    }

    public void closereview()
    {
        reviewPanel.SetActive(false);
        Characters.SetActive(true);
    }

    public void fiveStars()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.colossi.survival.samurai");
        reviewPanel.SetActive(false);
        Characters.SetActive(true);
    }

    public void BuyHealth()
    {
        if (PlayerPrefs.GetInt("Health") <= 1500 && PlayerPrefs.GetInt("coin") >= 500)
        {
            PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") + 100);
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - 500);

        }
        else
        {
            HealthButton.interactable = (false);
        }
    }

}
