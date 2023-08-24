using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UFE3D;

public class NextLevelUnlock : MonoBehaviour
{
    public Text coinReward;
    private int reward;
    private int currentCoins;

    public GameObject rewardSuccess;
    public TMP_Text successText;

    private void OnEnable()
    {


        if (PlayerPrefs.GetInt("selectedLevel") == 5)
        {
            PlayerPrefs.SetInt("KnockOut_Unlock", 1);
        }

        currentCoins = PlayerPrefs.GetInt("coin");
        Debug.Log("Current Ammount of Coins are " + currentCoins);

        reward = 1000;

        PlayerPrefs.SetInt("coin", currentCoins + reward);

        coinReward.text = $"Rewarded {reward} Coins";
    }

    public void rewardAddition()
    {
        rewardSuccess.gameObject.SetActive(true);
        successText.text = "Congratulations you 2X'd your reward";
    }

    public void VideoRewardSuccess()
    {
        reward *= 2;

        PlayerPrefs.SetInt("coin", currentCoins + reward);

        coinReward.text = $"Congrats you added {reward} Coins to your reward";

        rewardSuccess.gameObject.SetActive(false);

    }
}
