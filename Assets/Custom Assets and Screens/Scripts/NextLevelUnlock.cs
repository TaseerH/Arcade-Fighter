using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UFE3D;
using UnityEngine.SceneManagement;

public class NextLevelUnlock : MonoBehaviour
{
    public Text coinReward;
    private int reward;
    private int currentCoins;

    public GameObject NextButton;

    public GameObject rewardSuccess;
    public GameObject rewardFail;
    public TMP_Text successText;
    public Text storyComplete;


    public GameObject knockUnlock;

    public GameObject unlockAllPanel;
    public GameObject unlockAllCharactersPanel;
    public GameObject rateUsPanel;
    public GameObject removeAdsPanel;


    private void Start()
    {

        

        AdsManager.Instance.HideBanner();
        //AdsManager.Instance.HideAdmobBannerRectangle();
        AdsManager.OnRewardDoubleCoins += AdsManager_OnRewardDoubleCoins;
        AdsManager.OnRewardFreeCoinsFailed += AdsManager_OnRewardFreeCoinsFailed;


        if (PlayerPrefs.GetInt("selectedLevel") == 20)
        {
            Button btn = NextButton.GetComponent<Button>();
            btn.interactable = false;
            storyComplete.text = "Congrats! You have completed the story!";
        }


        currentCoins = PlayerPrefs.GetInt("coin");
        Debug.Log("Current Ammount of Coins are " + currentCoins);

        reward = 5000;

        PlayerPrefs.SetInt("freshcoin", 1);
        int currentLevel1 = PlayerPrefs.GetInt("selectedLevel");

        if (PlayerPrefs.GetInt($"level{currentLevel1}") == 1)
        {
            reward = 1000;
        }

        if(PlayerPrefs.GetInt($"level{currentLevel1}") == 21)
        {
            reward = 5000;
        }

        PlayerPrefs.SetInt("coin", currentCoins + reward);

        coinReward.text = $"Rewarded {reward} Coins";


        Debug.Log("Current Knockout mode status: " + PlayerPrefs.GetInt("firstTimeKnock"));

        if (PlayerPrefs.GetInt("selectedLevel") == 5 && PlayerPrefs.GetInt("firstTimeKnock") == 0)
        {

            Debug.Log("In Unlock KnockOut Conditional");

            

            PlayerPrefs.SetInt("KnockOut_Unlock", 1);

            int currentLevel = PlayerPrefs.GetInt("selectedLevel");
            PlayerPrefs.SetInt($"level{currentLevel}", 1);
            AdsManager.Instance.HideAdmobBannerRectangle();
            AdsManager.Instance.HideAllAds();

            AdsManager.Instance.HideAdmobBannerRectangle();
            AdsManager.Instance.HideBanner();

            knockUnlock.SetActive(true);
        }

        int currentLevelF = PlayerPrefs.GetInt("selectedLevel");
        PlayerPrefs.SetInt($"level{currentLevelF}", 1);
        

        switch (PlayerPrefs.GetInt("selectedLevel"))
        {
            case 2:
                unlockAllPanel.SetActive(true);
                AdsManager.Instance.HideAdmobBannerRectangle();
                break;
            case 4:
                rateUsPanel.SetActive(true);
                AdsManager.Instance.HideAdmobBannerRectangle();
                break;
            case 6:
                unlockAllPanel.SetActive(true);
                AdsManager.Instance.HideAdmobBannerRectangle();
                break;
            case 9:
                unlockAllCharactersPanel.SetActive(true);
                AdsManager.Instance.HideAdmobBannerRectangle();
                break;
            case 12:
                removeAdsPanel.SetActive(true);
                AdsManager.Instance.HideAdmobBannerRectangle();
                break;
            case 15:
                unlockAllPanel.SetActive(true);
                AdsManager.Instance.HideAdmobBannerRectangle();
                break;
            case 18:
                unlockAllPanel.SetActive(true);
                AdsManager.Instance.HideAdmobBannerRectangle();
                break;
            case 22:
                unlockAllPanel.SetActive(true);
                AdsManager.Instance.HideAdmobBannerRectangle();
                break;
            case 24:
                rateUsPanel.SetActive(true);
                AdsManager.Instance.HideAdmobBannerRectangle();
                break;
            case 26:
                unlockAllPanel.SetActive(true);
                AdsManager.Instance.HideAdmobBannerRectangle();
                break;
            case 29:
                unlockAllCharactersPanel.SetActive(true);
                AdsManager.Instance.HideAdmobBannerRectangle();
                break;

        }

        PlayerPrefs.SetInt("selectedLevel", currentLevelF + 1);

    }

    public void showAdRect()
    {
        AdsManager.Instance.ShowBannerRectangle();
    }

    private void AdsManager_OnRewardFreeCoinsFailed()
    {
        videoRewardUnsuccessful();
    }

    private void AdsManager_OnRewardDoubleCoins()
    {
        callRewardAddition();
    }

    public void rewardAddition()
    {
        AdsManager.Instance.ShowAdmobRewarded(1);
    }


    public void callRewardAddition()
    {
        AdsManager.Instance.HideAdmobBannerRectangle();
        rewardSuccess.SetActive(true);
        successText.text = "Congratulations you 2X'd your reward";
    }


    public void VideoRewardSuccess()
    {
        AdsManager.Instance.ShowBannerRectangle();
        reward *= 2;

        PlayerPrefs.SetInt("coin", currentCoins + reward);

        coinReward.text = $"Rewarded {reward} Coins";
        
        rewardSuccess.SetActive(false);

        

    }

    public void videoRewardUnsuccessful()
    {
        //AdsManager.Instance.ShowBannerRectangle();
        AdsManager.Instance.HideAdmobBannerRectangle();
        rewardSuccess.SetActive(false);
        rewardFail.SetActive(true);
    }

    public void rewardFailOk()
    {
        AdsManager.Instance.ShowBannerRectangle();
        rewardFail.SetActive(false);
    }

    public void knockOutUnlocked()
    {

        AdsManager.Instance.HideAdmobBannerRectangle();
        AdsManager.Instance.HideAllAds();
        
        Debug.Log("Coins before going to knockout" + PlayerPrefs.GetInt("coin"));
        UFE.EndGame(true);
        PlayerPrefs.SetInt("firstTimeKnock", PlayerPrefs.GetInt("firstTimeKnock") + 1);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void unlockAllbtn()
    {
        
            //purchaseSuccess.SetActive(true);
            PlayerPrefs.SetInt("allCharactersUnlocked", 1);
            PlayerPrefs.SetInt("allLevelsUnlocked", 1);
            PlayerPrefs.SetInt("KnockOut_Unlock", 1);
            //manager.knockout();
       
    }


    public void removeAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
    }

    public void unlockAllCharacters()
    {
        PlayerPrefs.SetInt("allCharactersUnlocked", 1);
    }

    public void rateUsBtn()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.nexthope.kungfu.gym.fighting.game");
    }

}
