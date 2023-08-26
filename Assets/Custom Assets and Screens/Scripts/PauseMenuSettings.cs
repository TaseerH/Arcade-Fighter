using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSettings : MonoBehaviour
{
    public void SoundOn()
    {
        PlayerPrefs.SetInt("sound", 1);
        UFE.SetSoundFX(true);
    }

    public void SoundOff()
    {
        PlayerPrefs.SetInt("sound", 0);
        UFE.SetSoundFX(false);
    }

    public void MusicOn()
    {
        PlayerPrefs.SetInt("music", 1);
        UFE.SetMusic(true);
    }

    public void MusicOff()
    {
        PlayerPrefs.SetInt("music", 0);
        UFE.SetMusic(false);
    }
}
