using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelectionScriptKnockout : MonoBehaviour
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

    public GameObject successMessage;
    public GameObject FailedMessage;

    public GameObject Story;
    public GameObject Knockout;


    private int allLevelsUnlock;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("mode") == 1)
        {
            Story.transform.localPosition = new Vector3(0, 1000, 0);
        }
        else
        {
            Knockout.transform.localPosition = new Vector3(0, 1000, 0);
        }
        //Debug.Log($"The Letter J is {j}");
        if (PlayerPrefs.GetInt("freshinstall") == 0)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == 0)
                {

                    PlayerPrefs.SetInt($"level{i+21}", 1);

                }
                else
                {
                    //PlayerPrefs.SetInt($"characterprice{i}", j);
                    PlayerPrefs.SetInt($"level{i + 21}", 0);
                }
                Debug.Log($"Knock Levels are {PlayerPrefs.GetInt($"level{i + 21}")}");
                //j += 500;
            }

        }


        levelSystem();

        if (PlayerPrefs.GetInt("allLevelsUnlocked") == 1)
        {
            unlockAllLevelsSuccess();
        }
    }


    private void LateUpdate()
    {
        levelSystem();
    }

    public void levelSystem()
    {
        
        for (int i = 0; i < buttons.Length; i++)
        {
            Debug.Log($"Levels in Level System are {PlayerPrefs.GetInt($"level{i}")}");
            if (PlayerPrefs.GetInt($"level{i + 21}") == 1)
            {
                myImageComponent = GameObject.Find($"Knock Level {i + 1}");
                //Debug.Log("Loop 1" + myImageComponent);
                buttons[i].interactable = true;
                button_text = myImageComponent.GetComponentInChildren<TMP_Text>();
                button_text.text = $"VICTORY";

                if(allLevelsUnlock == 1)
                {
                    image = myImageComponent.GetComponent<Image>();
                    image.sprite = unlockedLevelSprite;
                }


            }
            else if (PlayerPrefs.GetInt($"level{i +21 }") == 0)
            {

                if (PlayerPrefs.GetInt($"level{i + 21 - 1}") == 1)
                {
                    activeLevel = GameObject.Find($"Knock Level {i}");
                    //Debug.Log("Loop 2" + activeLevel);
                    activeLevelImageComponent = activeLevel.GetComponent<Image>();
                    activeLevelImageComponent.sprite = activeButton;
                    TextMeshProUGUI activebtn_text = activeLevelImageComponent.GetComponentInChildren<TextMeshProUGUI>();
                    activebtn_text.text = $"<sprite=0> FIGHT";
                }


                myImageComponent = GameObject.Find($"Knock Level {i + 1}");
                //Debug.Log("Loop 3" + myImageComponent);
                image = myImageComponent.GetComponent<Image>();
                button_text = myImageComponent.GetComponentInChildren<TMP_Text>();
                button_text.text = "<sprite=1> LOCKED";
                buttons[i].interactable = false;
                image.sprite = levelLocked;
            }
        }
    }


    public void unlockAllLevels ()
    {
        if(PlayerPrefs.GetInt("KnockOut_Unlock") == 0)
        {
            successMessage.SetActive(true);

        } else { 
            FailedMessage.SetActive(true); 
        }

    }

    public void unlockAllLevelsSuccess()
    {
            for (int i = 0; i < buttons.Length; i++)
            {
                PlayerPrefs.SetInt($"level{i + 21}", 1);
            }

            allLevelsUnlock = 1;
            PlayerPrefs.SetInt("KnockOut_Unlock", 1);
            levelSystem();

            successMessage.SetActive(false);
        
    }

    public void unlockAllLevelsFailed()
    {
        FailedMessage.SetActive(false);
    }
}
