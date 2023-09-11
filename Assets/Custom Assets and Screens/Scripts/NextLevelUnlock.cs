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


    private void OnEnable()
    {
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
        PlayerPrefs.SetInt("coin", currentCoins + reward);

        coinReward.text = $"Rewarded {reward} Coins";

        if (PlayerPrefs.GetInt("selectedLevel") == 5 && PlayerPrefs.GetInt("firstTimeKnock") == 0)
        {
            PlayerPrefs.SetInt("KnockOut_Unlock", 1);

            int currentLevel = PlayerPrefs.GetInt("selectedLevel");
            PlayerPrefs.SetInt($"level{currentLevel}", 1);

            knockUnlock.SetActive(true);
        }

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
        rewardSuccess.gameObject.SetActive(true);
        successText.text = "Congratulations you 2X'd your reward";
    }


    public void VideoRewardSuccess()
    {
        reward *= 2;

        PlayerPrefs.SetInt("coin", currentCoins + reward);

        coinReward.text = $"Rewarded {reward} Coins";

        rewardSuccess.gameObject.SetActive(false);

    }

    public void videoRewardUnsuccessful()
    {
        rewardSuccess.gameObject.SetActive(false);
        rewardFail.gameObject.SetActive(true);
    }

    public void rewardFailOk()
    {
        rewardFail.gameObject.SetActive(false);
    }

    public void knockOutUnlocked()
    {
        Debug.Log("Coins before going to knockout" + PlayerPrefs.GetInt("coin"));
        UFE.EndGame(true);
        PlayerPrefs.SetInt("firstTimeKnock", PlayerPrefs.GetInt("firstTimeKnock") + 1);
        SceneManager.LoadScene(0);
    }

}
