using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagerSFX : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManagerSFX instace;

    void Awake()
    {
        

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;

            s.source.loop = s.loop;

            //s.source.outputAudioMixerGroup ;

            //s.source.playOnAwake = false;
        }
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

        //Debug.Log("Playing Sound " + s.source.clip);
        s.source.Play();

        
    }

    public void toggle()
    {   

        foreach(Sound s in sounds)
        {
            if (PlayerPrefs.GetInt("sound") == 0)
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
