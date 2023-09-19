using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    public GameObject mainBtn;
    public GameObject secondBtn;
    public GameObject thirdBtn;
    public GameObject fourthBtn;


    public GameObject swipeTutorial;
    public GameObject mainBtnTutorial;

    private int joy1 = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("selectedLevel") == 1)
        {
            swipeBtnActivate();
        } else
        {
            this.gameObject.SetActive(false); 
        }
    }


    public void swipeBtnActivate()
    {
        swipeTutorial.SetActive(true);
        mainBtn.SetActive(false);
        secondBtn.SetActive(false);
        thirdBtn.SetActive(false);
        fourthBtn.SetActive(false);
        mainBtnTutorial.SetActive(false);
    }
    public void mainBtnActivate()
    {
        if (PlayerPrefs.GetInt("selectedLevel") == 1 && joy1 == 0 )
        {
            swipeTutorial.SetActive(false);
            mainBtn.SetActive(true);
            secondBtn.SetActive(false);
            thirdBtn.SetActive(false);
            fourthBtn.SetActive(false);
            mainBtnTutorial.SetActive(true);
            joy1 = 1;
        }
        
    }

    public void aftermainBtnActivate()
    {
        if (PlayerPrefs.GetInt("selectedLevel") == 1)
        {
            swipeTutorial.SetActive(false);
            mainBtn.SetActive(true);
            secondBtn.SetActive(true);
            thirdBtn.SetActive(true);
            fourthBtn.SetActive(true);
            mainBtnTutorial.SetActive(false);
        }

        
    }


}
