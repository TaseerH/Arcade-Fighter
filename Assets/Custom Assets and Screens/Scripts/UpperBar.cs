using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UpperBar : MonoBehaviour
{
    public TMP_Text coin;

    // Update is called once per frame
    void Update()
    {
        coin.text = PlayerPrefs.GetInt("coin").ToString("N0");
    }

    public void CharacterSelection()
    {
        UFE.EndGame(true);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
