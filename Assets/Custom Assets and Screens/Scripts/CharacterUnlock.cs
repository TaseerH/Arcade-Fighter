using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUnlock : MonoBehaviour
{
    public TMP_Text Currency;
    public GameObject[] characters;
    public int j = 500;

    // Start is called before the first frame update
    void Start()
    {

        Currency.text = PlayerPrefs.GetInt("coin").ToString();

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
                j += 500;
            }
        }

        PlayerPrefs.SetInt("freshinstall", 1);
        if (PlayerPrefs.GetInt("freshinstall") == 1)
        {
            Debug.Log("Fresh Install is 1");
        }
    }

    public void DoFreshInstall ()
    {
        PlayerPrefs.SetInt("freshinstall", 0);
    }
}
