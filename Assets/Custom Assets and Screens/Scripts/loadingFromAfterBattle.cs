using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingFromAfterBattle : MonoBehaviour
{


    //public GameObject MainMenu;
    public Slider slider;
    public float fillTime = 7f; // Time in seconds to fill the slider completely
                                //public string levelToLoad = "NextLevel"; // Name of the level to load

    private float currentTime = 0f;
    private bool isSliderFilled = false;

    public bool nextbtnload;
    public StoryModeTextureScreen next;

    private void OnEnable()
    {
        AdsManager.Instance.ShowBannerRectangle();
        AdsManager.Instance.ShowAdmobInterstitial();
    }


    // Update is called once per frame
    void Update()
    {
        // Check if the slider is not yet filled completely
        if (!isSliderFilled)
        {
            currentTime += Time.deltaTime;

            // Calculate the fill percentage based on current time and fillTime
            float fillPercentage = currentTime / fillTime;
            slider.value = fillPercentage;

            // Check if the slider is filled completely
            if (fillPercentage >= 1f)
            {
                isSliderFilled = true;
                NextScreen();
            }
        }
    }


    private void NextScreen()
    {
        AdsManager.Instance.HideAdmobBannerRectangle();

        this.gameObject.SetActive(false);
        //MainMenu.SetActive(true);
        if (nextbtnload)
        {
            next.GoToNextScreen();
        } else
        {
            UFE.RestartMatch();
        }
        

    }
}
