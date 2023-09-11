using System.Collections;
using System.Collections.Generic;
using System;
//using GoogleMobileAdsMediationTestSuite.Api;
using UnityEngine.UI;
using UnityEngine;

public class AdMediationTestDemo : MonoBehaviour
{
    [SerializeField] Button testSuiteBtn;
    [SerializeField] Button bannerBtn;
    [SerializeField] Button rectBtn;
    [SerializeField] Button intersBtn;
    [SerializeField] Button rewardedBtn;

    float currentTime;
    bool showBanner, showRect;
    bool adActive;

    private void OnEnable()
    {
        //MediationTestSuite.OnMediationTestSuiteDismissed += this.HandleMediationTestSuiteDismissed;
    }

    private void OnDisable()
    {
        //MediationTestSuite.OnMediationTestSuiteDismissed -= this.HandleMediationTestSuiteDismissed;
    }

    void Start()
    {        
        enableBtns(false);
    }

    private void Update()
    {
        if(!adActive)
        {
            currentTime += Time.unscaledDeltaTime;
            if (currentTime >= 5)
            {
                enableBtns(true);
                adActive = true;
            }
        }
        
    }

    public void showBannerAd()
    {
        showBanner = !showBanner;
        if (showBanner) { AdsManager.Instance.ShowBanner(); }
        else { AdsManager.Instance.HideBanner(); }
    }

    public void showRectAd()
    {
        showRect = !showRect;
        if (showRect) { AdsManager.Instance.ShowBannerRectangle(); }
        else { AdsManager.Instance.HideAdmobBannerRectangle(); }        
    }

    public void showInters()
    {
        AdsManager.Instance.ShowAdmobInterstitial();
    }

    public void showRewarded()
    {
        AdsManager.Instance.ShowAdmobRewarded(0);
    }

    public void showAdInspector()
    {
        AdsManager.Instance.OpenAdInspector();
    }

    public void showMediationTestSuite()
    {
        Debug.Log("ShowMediationTestSuite");
        //MediationTestSuite.Show("ca-app-pub-5453965874578277~9388727173");
        //MediationTestSuite.Show();      
    }

    public void HandleMediationTestSuiteDismissed(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleMediationTestSuiteDismissed event received");
        Debug.Log("Test Suite Dismissed");

    }

    void enableBtns(bool isEnabled)
    {
        testSuiteBtn.interactable = isEnabled;
        bannerBtn.interactable = isEnabled;
        rectBtn.interactable = isEnabled;
        intersBtn.interactable = isEnabled;
        rewardedBtn.interactable = isEnabled;
    }

}
