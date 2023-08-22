using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    public TMP_Text Currency;
    public UFE3D.GlobalInfo globalConfigFile;
    public UFE3D.CharacterInfo[] playercharacters;
    public GameObject[] characters;
    public int selectedCharacter = 0;
    public int j = 1000;

    public Button Play;
    public Button Buy;
    public TMP_Text buyText;

    private void FixedUpdate()
    {
        Currency.SetText(PlayerPrefs.GetInt("coin").ToString());
    
    }

    private void Start()
    {

        characters[PlayerPrefs.GetInt("character")].SetActive(true);
        selectedCharacter = PlayerPrefs.GetInt("character");


        Debug.Log($"Selected Level was {PlayerPrefs.GetInt("selectedLevel")}");
        
        Buy.gameObject.SetActive(false);
        

        Debug.Log($"The Letter J is {j}");
        if (PlayerPrefs.GetInt("freshinstall") == 0)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                if (i == 0)
                {
                    PlayerPrefs.SetInt($"character{i}", 1);
                }
                else
                {
                    PlayerPrefs.SetInt($"characterprice{i}", j);
                    PlayerPrefs.SetInt($"character{i}", 0);
                }
                

                Debug.Log($"Characters are {PlayerPrefs.GetInt($"character{i}")}");
                j += 1000;
            }

            PlayerPrefs.SetInt("playerHealth", 1);
            PlayerPrefs.SetInt("playerStamina", 0);
        }

        PlayerPrefs.SetInt("freshinstall", 1);
        if (PlayerPrefs.GetInt("freshinstall") == 1)
        {
            Debug.Log("Fresh Install is 1");
        }
    }

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);

        if (PlayerPrefs.GetInt($"character{selectedCharacter}") == 0)
        {
            Play.gameObject.SetActive(false);
            Buy.gameObject.SetActive(true);
            buyText.text = $"Buy For {PlayerPrefs.GetInt($"characterprice{selectedCharacter}")}";
        } else
        {
            Play.gameObject.SetActive(true);
            Buy.gameObject.SetActive(false);
        }


    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
        
        if (PlayerPrefs.GetInt($"character{selectedCharacter}") == 0)
        {
            Play.gameObject.SetActive(false);
            Buy.gameObject.SetActive(true);
            buyText.text = $"Buy For {PlayerPrefs.GetInt($"characterprice{selectedCharacter}")}";
        }
        else
        {
            Play.gameObject.SetActive(true);
            Buy.gameObject.SetActive(false);
        }
    }

    public void LoadUFECharactersAndStage()
    {
        PlayerPrefs.SetInt("character", selectedCharacter);
        globalConfigFile.deploymentOptions.deploymentType = UFE3D.DeploymentType.FullInterface;



        globalConfigFile.deploymentOptions.activeCharacters[0] = playercharacters[PlayerPrefs.GetInt("character")];
        //globalConfigFile.deploymentOptions.activeCharacters[1] = P2SelectedChar;
        globalConfigFile.deploymentOptions.AIControlled[0] = false;
        //globalConfigFile.deploymentOptions.AIControlled[1] = true;

        //Debug.Log("Selected Stage is " + selectedStage);

        for (int i = 0; i < globalConfigFile.stages.Length; i++)
        {
            Debug.Log("Stages available are " + globalConfigFile.stages[i]);
        }

        //UFE.SetStage(selectedStage);
        //globalConfigFile.selectedStage = globalConfigFile.stages[selectedStage];

        SceneManager.LoadScene("Demo_Fighter2D");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void GoToLevels()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void BuyCharacter()
    {
        if(PlayerPrefs.GetInt("coin") >= PlayerPrefs.GetInt($"characterprice{selectedCharacter}"))
        {
            PlayerPrefs.SetInt($"character{selectedCharacter}", 1);
            Play.gameObject.SetActive(true);
            Buy.gameObject.SetActive(false);
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - PlayerPrefs.GetInt($"characterprice{selectedCharacter}"));
        }
        else
        {
            Debug.Log("Not enough coin");
        }
        
    }


    public void unlockAllCharacters()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            PlayerPrefs.SetInt($"character{i}", 1);
            Play.gameObject.SetActive(true);
            Buy.gameObject.SetActive(false);
        }
    }


    public void BuyCoin()
    {
        PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 500);
    }

    public void DoFreshInstall()
    {
        PlayerPrefs.SetInt("freshinstall", 0);
        PlayerPrefs.SetInt("Health", 0);
    }
}
