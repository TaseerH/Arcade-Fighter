using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelectionScript : MonoBehaviour
{

    public Button[] buttons;

    public Sprite levelLocked;
    public Sprite activeButton;
    public Sprite unlockedLevelSprite;
    GameObject activeLevel;
    Image activeLevelImageComponent;
    GameObject myImageComponent;
    Image image;
    TMP_Text button_text;


    private int allLevelsUnlock;

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log($"The Letter J is {j}");
        if (PlayerPrefs.GetInt("freshinstall") == 0)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == 0)
                {

                    PlayerPrefs.SetInt($"level{i}", 1);

                }
                else
                {
                    //PlayerPrefs.SetInt($"characterprice{i}", j);
                    PlayerPrefs.SetInt($"level{i}", 0);
                }
                Debug.Log($"Levels are {PlayerPrefs.GetInt($"level{i}")}");
                //j += 500;
            }

        }


        levelSystem();
    }


    private void Update()
    {
        levelSystem();
    }

    public void levelSystem()
    {
        
        for (int i = 0; i < buttons.Length; i++)
        {
            if (PlayerPrefs.GetInt($"level{i}") == 1)
            {
                myImageComponent = GameObject.Find($"Level {i + 1}");
                buttons[i].interactable = true;
                button_text = myImageComponent.GetComponentInChildren<TMP_Text>();
                button_text.text = $"Level {i + 1}";

                if(allLevelsUnlock == 1)
                {
                    image = myImageComponent.GetComponent<Image>();
                    image.sprite = unlockedLevelSprite;
                }


            }
            else if (PlayerPrefs.GetInt($"level{i}") == 0)
            {

                if (PlayerPrefs.GetInt($"level{i - 1}") == 1)
                {
                    activeLevel = GameObject.Find($"Level {i}");
                    activeLevelImageComponent = activeLevel.GetComponent<Image>();
                    activeLevelImageComponent.sprite = activeButton;
                }


                myImageComponent = GameObject.Find($"Level {i + 1}");
                image = myImageComponent.GetComponent<Image>();
                button_text = myImageComponent.GetComponentInChildren<TMP_Text>();
                button_text.text = "";
                buttons[i].interactable = false;
                image.sprite = levelLocked;
            }
        }
    }


    public void unlockAllLevels ()
    {
        for (int i = 0; i < buttons.Length; i++) {
            PlayerPrefs.SetInt($"level{i}", 1);    
        }

        allLevelsUnlock = 1;
        PlayerPrefs.SetInt("KnockOut_Unlock", 1);
        levelSystem();

    }


}
