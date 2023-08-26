using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{

    public GameObject soundOff;
    public GameObject soundOn;

    public GameObject musicOff;
    public GameObject musicOn;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("freshinstall") == 0)
        {
            PlayerPrefs.SetInt("sound", 1);
            PlayerPrefs.SetInt("music", 1);
        }
 
        if(PlayerPrefs.GetInt("sound") == 1)
        {
            SoundOn();
        } else
        {
            SoundOff();
        }
        if(PlayerPrefs.GetInt("music") == 1)
        {
            MusicOn();
        } else
        {
            MusicOff();
        }
    }

    public void SoundOn()
    {
        soundOff.SetActive(false);
        soundOn.SetActive(true);
        PlayerPrefs.SetInt("sound", 1);
        //UFE.SetSoundFX(true);
    }

    public void SoundOff()
    {
        soundOff.SetActive(true);
        soundOn.SetActive(false);
        PlayerPrefs.SetInt("sound", 0);
        //UFE.SetSoundFX(false);
    }

    public void MusicOn()
    {
        musicOff.SetActive(false);
        musicOn.SetActive(true);
        PlayerPrefs.SetInt("music", 1);
        //UFE.SetMusic(true);
    }

    public void MusicOff()
    {
        musicOff.SetActive(true);
        musicOn.SetActive(false);
        PlayerPrefs.SetInt("music", 0);
        //UFE.SetMusic(false);
    }
}
