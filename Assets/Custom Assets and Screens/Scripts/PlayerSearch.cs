using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearch : MonoBehaviour
{

    public GameObject searchimage;
    public GameObject battleStart;

    public GameObject player2;

    public float fillTime = 7f; // Time in seconds to fill the slider completely

    private float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        searchimage.SetActive(true);
        battleStart.SetActive(false);
        player2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        // Calculate the fill percentage based on current time and fillTime
        float fillPercentage = currentTime / fillTime;
        //slider.value = fillPercentage;

        // Check if the slider is filled completely
        if (fillPercentage >= 1f)
        {
            //isSliderFilled = true;
            opponentFound();
        }
    }

    private void opponentFound()
    {
        searchimage.SetActive(false);
        battleStart.SetActive(true);
        player2.SetActive(true);
    }
}
