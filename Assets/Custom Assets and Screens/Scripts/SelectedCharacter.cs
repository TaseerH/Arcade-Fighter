using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacter : MonoBehaviour
{
    public GameObject[] selectableCharacters;
    public int character;
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetInt("freshinstall") == 0)
        {
            PlayerPrefs.SetInt("character", 0);
        }

        character = PlayerPrefs.GetInt("character");
        Debug.Log(character);
        for (int i = 0; i <= selectableCharacters.Length; i++)
        {
            if (character == i) {
                selectableCharacters[i].SetActive(true);
            } 
        }
    }

}
