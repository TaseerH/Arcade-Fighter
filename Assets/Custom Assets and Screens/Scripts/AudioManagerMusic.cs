using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagerMusic : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManagerMusic instace;

    void Awake()
    {
        if(instace == null)
        {
            instace = this;
        } else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;

            s.source.loop = s.loop;
        }

        Play("Theme");

    }

    private void LateUpdate()
    {
        toggle();
    }

    public void Play( string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found:" + name);
            return;
        }


        s.source.Play();

        
    }

    public void toggle()
    {
        foreach(Sound s in sounds)
        {
            if (PlayerPrefs.GetInt("music") == 0)
            {
                s.source.mute = true;
            }
            else
            {
                s.source.mute = false;
            }
        }
    }

}
