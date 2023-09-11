using UnityEngine;
using System.Collections;
using System;
using GoogleMobileAds.Api.Mediation.AppLovin;

#if (UNITY_IOS && !UNITY_EDITOR)
using System.Runtime.InteropServices;
#endif

public class InitializeAdsManager : MonoBehaviour
{

    [SerializeField] BannerPos myBannerPosition = BannerPos.Top;
    [SerializeField] RectBannerPos myRectBannerPosition = RectBannerPos.BottomLeft;

    //public string myAdmobAppID = null;
    public string myAdmobBannerID1 = null;
    public string myAdmobBannerID2 = null;
    public string myAdmobIntersID = null;
    public string myAdmobRewardedID = null;

    //string testAdmobAppID =      "ca-app-pub-5453965874578277~4128806100";
    string testAdmobBannerID2 = "ca-app-pub-3940256099942544/6300978111";
    string testAdmobIntersID = "ca-app-pub-3940256099942544/1033173712";
    string testAdmobRewardedID = "ca-app-pub-3940256099942544/5224354917";

    #if UNITY_ANDROID
        string testAdmobBannerID1 = "ca-app-pub-3940256099942544/6300978111";
    #elif UNITY_IPHONE
        string testAdmobBannerID1 = "ca-app-pub-3940256099942544/2934735716";
    #else
        string testAdmobBannerID1 = "unexpected_platform";
    #endif

    public static bool check = false;
    public bool TestAds = false;


    void Start()
    {

        if (!PlayerPrefs.HasKey("RemoveAds"))
        {
            PlayerPrefs.SetInt("RemoveAds", 0);
        }

        AdsManager adsManager = AdsManager.Instance;
        adsManager.BannerPosition = myBannerPosition;
        adsManager.RectBannerPosition = myRectBannerPosition;

        if (!TestAds)
        {
            //adsManager.AdmobAppID = myAdmobAppID;
            adsManager.AdmobBannerID = myAdmobBannerID1;
            adsManager.RectBannerID = myAdmobBannerID2;
            adsManager.AdmobIntersID = myAdmobIntersID;
            adsManager.myAdmobRewardedID = myAdmobRewardedID;
        }
        else
        {
            //adsManager.AdmobAppID = testAdmobAppID;
            adsManager.AdmobBannerID = testAdmobBannerID1;
            adsManager.RectBannerID = testAdmobBannerID2;
            adsManager.AdmobIntersID = testAdmobIntersID;
            adsManager.myAdmobRewardedID = testAdmobRewardedID;
        }
        
        AppLovin.SetHasUserConsent(true);
        AppLovin.SetDoNotSell(true);
        AppLovin.SetIsAgeRestrictedUser(true);

        adsManager.InitializeAds();

#if (UNITY_IOS && !UNITY_EDITOR)
        StartCoroutine(enableMetaAdvertizerTrackingFlagForIOS());
#endif

    }

    // Uncomment for testing

    void OnGUI()
    {

    }

    public void ShowBanner()
    {
        Debug.Log("ShowBanner");
        AdsManager.Instance.ShowBanner();
    }

    public void ShowBannerRectangle()
    {
        Debug.Log("ShowBannerRectangle");
        AdsManager.Instance.ShowBannerRectangle();
    }

    public void HideBanner()
    {
        Debug.Log("HideBanner");
        AdsManager.Instance.HideBanner();
    }

    public void HideBannerRectangle()
    {
        Debug.Log("HideBannerRectangle");
        AdsManager.Instance.HideAdmobBannerRectangle();
    }

    public void HideAllAds()
    {
        Debug.Log("HideAllAds");
        AdsManager.Instance.HideAllAds();
    }

    public void RemoveAds()
    {
        Debug.Log("Removeads");
        PlayerPrefs.SetInt("RemoveAds", 1);
        AdsManager.Instance.HideAllAds();
    }

#if (UNITY_IOS && !UNITY_EDITOR)
    IEnumerator enableMetaAdvertizerTrackingFlagForIOS()
    {
        yield return new WaitForSeconds(0.5f);
        AudienceNetwork.AdSettings.SetAdvertiserTrackingEnabled(true);
    }
#endif

}


#if (UNITY_IOS && !UNITY_EDITOR)

namespace AudienceNetwork
{
    public static class AdSettings
    {
        [DllImport("__Internal")] 
        private static extern void FBAdSettingsBridgeSetAdvertiserTrackingEnabled(bool advertiserTrackingEnabled);

        public static void SetAdvertiserTrackingEnabled(bool advertiserTrackingEnabled)
        {
            FBAdSettingsBridgeSetAdvertiserTrackingEnabled(advertiserTrackingEnabled);
        }
    }
}

// In order to run Meta Ads, add following to the info.plist :=> https://developers.facebook.com/docs/setting-up/platform-setup/ios/SKAdNetwork

#endif
