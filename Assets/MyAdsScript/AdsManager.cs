using UnityEngine;
using System;
using GoogleMobileAds.Api;
using System.Collections;

public enum BannerPos
{
    Top = 0,
    Bottom = 1
};

public enum RectBannerPos
{
    TopLeft,
    BottomLeft
};

public class AdsManager : MonoBehaviour
{
    #region Instance

    // Static singleton instance
    private static AdsManager instance;
    public static AdsManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("AdsManager");
                instance = obj.AddComponent<AdsManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    #endregion

    public BannerPos BannerPosition;
    public RectBannerPos RectBannerPosition;
    public string AdmobBannerID = null;
    public string RectBannerID = null;
    public string AdmobIntersID = null;
    public string myAdmobRewardedID = null;

    private BannerView bannerView1 = null;
    private BannerView rectBannerView = null;
    private InterstitialAd interstitial = null;
    private RewardedAd rewardedAd = null;

    static bool isInitialized = false;

    public static bool DoubleRewards;

    float currTimescale;

    int rewardType = -1;

    public delegate void RewardAdAction();
    public static event RewardAdAction OnRewardFreeCoins;
    public static event RewardAdAction OnRewardDoubleCoins;

    public static event RewardAdAction OnRewardFreeCoinsFailed;

    public void InitializeAds()
    {

        if (!isInitialized)
        {
            isInitialized = true;
            try
            {
                MobileAds.Initialize(initStatus => { });
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }

            StartCoroutine(RequestBanner(1));
            StartCoroutine(RequestRectBanner(2));
            StartCoroutine(RequestInterstitial(4));
            StartCoroutine(RequestRewardBasedVideo(6));
        }
    }

    public void OpenAdInspector()
    {
        MobileAds.OpenAdInspector(error => {
            // Error will be set if there was an issue and the inspector was not displayed.
        });
    }

    /// <summary>
    /// Use this Method GiveRewards() to give rewards to user 
    /// 0 is for double reward on level complete
    /// 1 is for skip level
    /// 2 is for free cash e.g. 2000 cash
    /// </summary>

    void GiveRewards()
    {

#if (UNITY_IOS)
         Time.timeScale = currTimescale;   
#endif

        Debug.Log("GiveRewards");

        switch (rewardType)
        {
            case 0:
                if (OnRewardFreeCoins != null) { OnRewardFreeCoins.Invoke(); }
                break;
            case 1:
                if (OnRewardDoubleCoins != null)
                {
                    OnRewardDoubleCoins.Invoke();
                }
                break;

            case 2:

                break;

            default:
                break;
        }
        rewardType = -1;
    }

    public void HideAllAds()
    {
        HideBanner();
        HideAdmobBannerRectangle();
    }

    #region Admob Banner Ads
    private IEnumerator RequestBanner(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("RequestBanner");
        try
        {
            string adUnitId1 = AdmobBannerID;

            AdSize adaptiveSize =
                    AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
            
            //// use this for standard banner
            //adaptiveSize = AdSize.Banner;


            if (BannerPosition == BannerPos.Top)
            {
                bannerView1 = new BannerView(adUnitId1, adaptiveSize, AdPosition.Top);
            }
            else
            {
                bannerView1 = new BannerView(adUnitId1, adaptiveSize, AdPosition.Bottom);
            }

            // Create an e1mpty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the banner with the request.
            bannerView1.LoadAd(request);
            bannerView1.Hide();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }

    }


    private IEnumerator RequestRectBanner(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("RequestRectBanner");
        try
        {
            string adUnitId2 = RectBannerID;
            
            if(RectBannerPosition.Equals(RectBannerPos.TopLeft))
            {
                rectBannerView = new BannerView(adUnitId2, AdSize.MediumRectangle, AdPosition.TopLeft);
            }
            else
            {
                rectBannerView = new BannerView(adUnitId2, AdSize.MediumRectangle, AdPosition.BottomLeft);
            }

            // Create an e1mpty ad request.
            AdRequest request2 = new AdRequest.Builder().Build();
            rectBannerView.LoadAd(request2);
            rectBannerView.Hide();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            try
            {
                if (bannerView1 != null)
                    bannerView1.Show();
                else
                    StartCoroutine(RequestBanner(1));
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }
    }
    public void ShowBannerRectangle()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            try
            {
                if (rectBannerView != null)
                    rectBannerView.Show();
                else
                    StartCoroutine(RequestRectBanner(1));
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }
    }

    public void HideBanner()
    {
        if (bannerView1 != null)
        {
            try
            {
                bannerView1.Hide();
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }
    }
    public void HideAdmobBannerRectangle()
    {
        if (rectBannerView != null)
        {
            try
            {
                rectBannerView.Hide();
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }
    }

    public void DestroyAdmobBannerTop()
    {
        try
        {
            if (bannerView1 != null)
                bannerView1.Destroy();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }
    public void DestroyAdmobRectangle()
    {
        try
        {
            if (rectBannerView != null)
                rectBannerView.Destroy();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }


    #endregion

    #region Admob Interstitial Ads

    private IEnumerator RequestInterstitial(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("RequestInterstitial");
        try
        {
            string adUnitId = AdmobIntersID;

            // Clean up interstitial before using it
            if (interstitial != null)
            {
                interstitial.Destroy();
                interstitial = null;
            }

            // Initialize an InterstitialAd.
            
            //interstitial = new InterstitialAd(adUnitId);

            // Called when an ad request has successfully loaded.
            //this.interstitial.OnAdLoaded += HandleOnAdLoaded;
            //// Called when an ad request failed to load.
            //this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            //// Called when an ad is shown.
            //this.interstitial.OnAdOpening += HandleOnAdOpened;
            //// Called when the ad is closed.
            //this.interstitial.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            //this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.

            //interstitial.LoadAd(request);

            InterstitialAd.Load(adUnitId, request,
          (InterstitialAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("interstitial ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              interstitial = ad;
          });


        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    public void ShowAdmobInterstitial()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            try
            {
                if (interstitial != null && interstitial.CanShowAd())
                {
                    interstitial.Show();
                    
                    StartCoroutine(RequestInterstitial(5));
                }
                else
                {
                    //show unity ad on admob fail
                    //Debug.Log("Showing Ad: " + UnityinterID);
                    //Advertisement.Show(UnityinterID, this);
                    
                    StartCoroutine(RequestInterstitial(5));
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }
    }

    public void DestroyInterstitial()
    {
        try
        {
            if (interstitial != null)
                interstitial.Destroy();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    private void RegisterReloadHandler(InterstitialAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
    {
            Debug.Log("Interstitial Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.

            //StartCoroutine(RequestInterstitial(2));
            
            //LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            //StartCoroutine(RequestInterstitial(2));            
        };
    }

    //Events........

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    #endregion

    #region Admob Rewarded Ads

    private IEnumerator RequestRewardBasedVideo(float time)
    {

        yield return new WaitForSeconds(time);
        Debug.Log("RequestRewardBasedVideo");
        try
        {
            string adUnitId = myAdmobRewardedID;


            if (rewardedAd != null)
            {
                rewardedAd.Destroy();
                rewardedAd = null;
            }

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.

            // send the request to load the ad.
            RewardedAd.Load(adUnitId, request,
                (RewardedAd ad, LoadAdError error) =>
                {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Rewarded ad loaded with response : "
                              + ad.GetResponseInfo());

                    rewardedAd = ad;
                });

        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    public bool RewardIsAvailable
    {

        get
        {
            try
            {
                if (rewardedAd != null)
                    return rewardedAd.CanShowAd();
                else
                    if (OnRewardFreeCoinsFailed != null) { OnRewardFreeCoinsFailed.Invoke(); }
                return false;
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
                return false;
            }
        }
    }

    public void ShowAdmobRewarded(int value)
    {
        try
        {
            rewardType = value;
            if (rewardedAd != null && rewardedAd.CanShowAd())
            {

#if (UNITY_IOS)
                currTimescale = Time.timeScale;
                Time.timeScale = 0;
#endif

                const string rewardMsg =
                "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

                rewardedAd.Show((Reward reward) =>
                {
                    // TODO: Reward the user.
                    Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                    GiveRewards();
                });

                StartCoroutine(RequestRewardBasedVideo(10));
            }
            else
            {
                //show unity ad on admob fail
                //if (unityRewardReady)
                //{
                //    unityRewardReady = false;
                //    // Then show the ad:
                //    Advertisement.Show(UnityRewadedID, this);
                //}

                StartCoroutine(RequestRewardBasedVideo(5));
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }

    }


    //// EVENTS...

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            if (OnRewardFreeCoinsFailed != null) { OnRewardFreeCoinsFailed.Invoke(); }
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            if (OnRewardFreeCoinsFailed != null) { OnRewardFreeCoinsFailed.Invoke(); }
        };
    }

    private void RegisterReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += ()=>
    {
            Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            //StartCoroutine(RequestRewardBasedVideo(2));
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            //StartCoroutine(RequestRewardBasedVideo(2));
        };
    }

    #endregion
}
