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

    public GameObject[] particles = null;


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
    public Button knockOutModeBtn = null;

    public Sprite knockOn = null;
    public Sprite knockOff = null;

    public bool knockOut = false;
    private void Update()
    {
        for (int i = 0; i < Currency.Length; i++)
        {
            Currency[i].SetText(PlayerPrefs.GetInt("coin").ToString("N0"));
            //Debug.Log("Coins formatted are: " + PlayerPrefs.GetInt("coin").ToString("N0"));
        }


        if (Currency == null || PlayButtons == null || StoryModeCanvas == null || knockOutMode == null || reviewPanel == null || privacyPolicyMain == null || LoadingMainMenuScreen == null)
        {
            return;
        }

        
        

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

            knockoutLevels();
        

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


        if (PlayerPrefs.GetInt("firstTimeKnock") == 1)
        {
            LoadingMainMenuScreen.SetActive(false);
            
            StoryModeScreen();
            PlayerPrefs.SetInt("firstTimeKnock", 2);
            Characters.SetActive(false);

            particles[0].SetActive(false);
            particles[1].SetActive(false);
        }


        if (PlayerPrefs.GetInt("backFromLevelSelection") == 1)
        {

            acceptPrivacyPolicy();

            PlayerPrefs.SetInt("backFromLevelSelection", 0);
            LoadingMainMenuScreen.SetActive(false);
            Characters.SetActive(true);

            particles[0].SetActive(true);
            particles[1].SetActive(true);
            BackButtonStoryModeScreen();

        }

        knockoutLevels();
       

        //Debug.Log(health.text + character.text);
        if (PlayerPrefs.GetInt("Health") >= 1500 || PlayerPrefs.GetInt("coin") < 500)
        {
            HealthButton.interactable = (false);
        }

        


    }

    private void knockoutLevels()
    {
        if(PlayerPrefs.GetInt("KnockOut_Unlock") == 1)
        {
            knockOutModeBtn.image.sprite = knockOn;
        } else
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

        Characters.SetActive(false);

        particles[0].SetActive(false);
        particles[1].SetActive(false);
        privacyPolicyMain.SetActive(true);
        
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

        particles[0].SetActive(false);
        particles[1].SetActive(false);
        Application.OpenURL("https://play.google.com/store/apps/dev?id=8542001137219996574&hl=en&gl=US");
    }

    public void selectLevel(int level)
    {
        PlayerPrefs.SetInt("selectedLevel", level);
        
        SceneManager.LoadScene(1, LoadSceneMode.Single);

        
    }

    public void BackToMainMenu()
    {
        PlayerPrefs.SetInt("backFromLevelSelection", 1);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void DoFreshInstall()
    {
        PlayerPrefs.SetInt("freshinstall", 0);
    }

    public void addwatch()
    {
        AdsManager.Instance.ShowAdmobRewarded(0);
        //adwatchSuccess();
    }

    private void OnEnable()
    {
        AdsManager.OnRewardFreeCoins += AdsManager_OnRewardFreeCoins;
        AdsManager.OnRewardFreeCoinsFailed += AdsManager_OnRewardFreeCoinsFailed;
    }

    private void AdsManager_OnRewardFreeCoinsFailed()
    {
        adwatchFail();
    }

    private void OnDisable()
    {
        AdsManager.OnRewardFreeCoins -= AdsManager_OnRewardFreeCoins;
        AdsManager.OnRewardFreeCoinsFailed -= AdsManager_OnRewardFreeCoinsFailed;
    }

    private void AdsManager_OnRewardFreeCoins()
    {
        adwatchSuccess();
    }

    public void adwatchSuccess()
    {
        adWatchPanelsuccess.SetActive(true);
        Characters.SetActive(false);

        particles[0].SetActive(false);
        particles[1].SetActive(false);
    }

    public void adwatchFail()
    {
        adWatchPanelfail.SetActive(true);
        Characters.SetActive(false);

        particles[0].SetActive(false);
        particles[1].SetActive(false);
    }

    public void adFail()
    {
        adWatchPanelfail.SetActive(false);
        Characters.SetActive(true);

        particles[0].SetActive(true);
        particles[1].SetActive(true);
    }

    public void BuyCoin()
    {
        PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 5000);
        adWatchPanelsuccess.SetActive(false);
        Characters.SetActive(true);

        particles[0].SetActive(true);
        particles[1].SetActive(true);
        PlayerPrefs.SetInt("freshcoin", 1);
        //PlayerPrefs.SetInt("freshinstall", 1);
;    }

    public void GoToCharacterLevelSelection(int mode)
    {
        if (mode == 1 && PlayerPrefs.GetInt("KnockOut_Unlock") == 0)
        {
            return;
        }
        else
        {
            PlayerPrefs.SetInt("mode", mode);
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
    }

    public void StoryModeScreen()
    {
        PlayButtons.SetActive(false);

        particles[0].SetActive(false);
        particles[1].SetActive(false);
        Characters.SetActive(false);
        StoryModeCanvas.SetActive(true);
    }

    public void BackButtonStoryModeScreen()
    {
        PlayButtons.SetActive(true);
        Characters.SetActive(true);

        particles[0].SetActive(true);
        particles[1].SetActive(true);
        StoryModeCanvas.SetActive(false);
    }

    public void BackButtonStoreScreen()
    {
        storePage.SetActive(false);
        Characters.SetActive(true);

        particles[0].SetActive(true);
        particles[1].SetActive(true);
    }

    public void StorePage()
    {
        storePage.SetActive(true);
        Characters.SetActive(false);

        particles[0].SetActive(false);
        particles[1].SetActive(false);
    }

    public void BackButtonSettingsScreen()
    {
        Settings.SetActive(false);
        Characters.SetActive(true);


        particles[0].SetActive(true);
        particles[1].SetActive(true);
    }

    public void SettingsPage()
    {
        Settings.SetActive(true);

        particles[0].SetActive(false);
        particles[1].SetActive(false);

        Characters.SetActive(false);
    }

    public void QuitPanelActive()
    {
        QuitPanel.SetActive(true);

        particles[0].SetActive(false);
        particles[1].SetActive(false);
        Characters.SetActive(false);
    }

    public void QuitPanelDeactive()
    {
        QuitPanel.SetActive(false);
        Characters.SetActive(true);

        particles[0].SetActive(true);
        particles[1].SetActive(true);
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

        particles[0].SetActive(false);
        particles[1].SetActive(false);

    }

    public void closereview()
    {
        reviewPanel.SetActive(false);
        Characters.SetActive(true);

        particles[0].SetActive(true);
        particles[1].SetActive(true);
    }

    public void fiveStars()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.colossi.survival.samurai");
        reviewPanel.SetActive(false);
        Characters.SetActive(true);

        particles[0].SetActive(true);
        particles[1].SetActive(true);
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
