using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adRemoval : MonoBehaviour
{
    private void OnEnable()
    {
        AdsManager.Instance.HideAdmobBannerRectangle();
        AdsManager.Instance.HideBanner();
        AdsManager.Instance.HideAllAds();
    }
}
