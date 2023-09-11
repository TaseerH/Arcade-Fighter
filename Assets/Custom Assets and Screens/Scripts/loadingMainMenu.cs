using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingMainMenu : MonoBehaviour
{

    //public GameObject MainMenu;
    public Slider slider;
    public float fillTime = 7f; // Time in seconds to fill the slider completely
                                //public string levelToLoad = "NextLevel"; // Name of the level to load

    private float currentTime = 0f;
    private bool isSliderFilled = false;

    public GameObject characters;
    public GameObject[] particles;

    // Start is called before the first frame update
    void Start()
    {
        characters.SetActive(false);
        particles[0].SetActive(false);
        particles[1].SetActive(false);
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
        //MainMenu.SetActive(true);
        characters.SetActive(true);

        particles[0].SetActive(true);
        particles[1].SetActive(true);
        this.gameObject.SetActive(false);
    }
}
