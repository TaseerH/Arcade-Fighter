using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject privacyPolicyMain = null;
    public GameObject LoadingMainMenuScreen = null;

    public GameObject miniPrivacy = null;

    public TMP_Text[] Currency = null;
    public GameObject PlayButtons = null;
    public GameObject Characters = null;
    public GameObject StoryModeCanvas = null;


    public GameObject reviewPanel = null;
    public GameObject storePage = null;
    public GameObject Settings = null;
    public GameObject QuitPanel = null;
    public Button knockOutMode = null;

    public GameObject adWatchPanelsuccess = null;
    public GameObject adWatchPanelfail = null;

    public Button HealthButton = null;
    public Button knockOutModeBtn;

    public Sprite knockOn;
    public Sprite knockOff;

    public bool knockOut;
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

        if (PlayerPrefs.GetInt("KnockOut_Unlock") == 1)
        {
            //Debug.Log("Knockout mode is:" + PlayerPrefs.GetInt("KnockOut_Unlock"));
            knockOut = true;
            knockoutLevels();
        }
        else
        {
            //Debug.Log("Knockout mode is:" + PlayerPrefs.GetInt("KnockOut_Unlock"));
            knockOut = false;
            knockoutLevels();
        }

    }



    private void Start()
    {
        if (PlayerPrefs.GetInt("freshcoin") == 0)
        {
            PlayerPrefs.SetInt("coin", 500);
            //PlayerPrefs.SetInt("KnockOut_Unlock", 0);
        }
        if (PlayerPrefs.GetInt("freshinstall") == 0)
        {
            //PlayerPrefs.SetInt("coin", 500);
            PlayerPrefs.SetInt("KnockOut_Unlock", 0);
            MainPrivacyMenu();
        } else {
            acceptPrivacyPolicy();
        }

        if (PlayerPrefs.GetInt("KnockOut_Unlock") == 1)
        {
            knockOut = true;
            knockoutLevels();
        } else
        {
            knockOut = false;
            knockoutLevels();
        }

        //Debug.Log(health.text + character.text);
        if (PlayerPrefs.GetInt("Health") >= 1500 || PlayerPrefs.GetInt("coin") < 500)
        {
            HealthButton.interactable = (false);
        }

        


    }

    private void knockoutLevels()
    {
        if(knockOut == true)
        {
            knockOutModeBtn.image.sprite = knockOn;
        } else if (knockOut == false)
        {
            knockOutModeBtn.image.sprite = knockOff;
        }
    }

    public void knockout()
    {
        if (PlayerPrefs.GetInt("KnockOut_Unlock") == 1)
        {
            knockOut = true;
            knockoutLevels();
        }
        else
        {
            knockOut = false;
            knockoutLevels();
        }
    }

    public void MainPrivacyMenu()
    {
        privacyPolicyMain.SetActive(true);
        Characters.SetActive(false);
    }

    public void acceptPrivacyPolicy()
    {

        LoadingMainMenuScreen.SetActive(true);
        //Characters.SetActive(true);
        privacyPolicyMain.SetActive(false);
    }

    public void readMore()
    {
        Characters.SetActive(false);
        Application.OpenURL("https://play.google.com/store/apps/dev?id=8542001137219996574&hl=en&gl=US");
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
        PlayerPrefs.SetInt("freshcoin", 1);
        //PlayerPrefs.SetInt("freshinstall", 1);
;    }

    public void GoToCharacterLevelSelection(int mode)
    {
        if(mode == 1 && PlayerPrefs.GetInt("KnockOut_Unlock") == 0)
        {
            return;
        }
        
        PlayerPrefs.SetInt("mode", mode);
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


    public void miniPrivacyOpen()
    {
        miniPrivacy.SetActive(true);
    }

    public void miniPrivacyClose()
    {
        miniPrivacy.SetActive(false);
    }
}
