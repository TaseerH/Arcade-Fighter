using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UFE3D;
using TMPro;
using UnityEngine.SceneManagement;

public class DefaultMainMenuScreen : MainMenuScreen
{
	#region public instance fields
	public AudioClip onLoadSound;
	public AudioClip music;
	public AudioClip selectSound;
	public AudioClip cancelSound;
	public AudioClip moveCursorSound;
	public bool stopPreviousSoundEffectsOnLoad = false;
	public float delayBeforePlayingMusic = 0.1f;

	public Button buttonNetwork;
	public Button buttonBluetooth;
	#endregion

	//public TMP_InputField health;
	//public TMP_InputField character;

	#region public override methods
	public override void DoFixedUpdate(
		IDictionary<InputReferences, InputEvents> player1PreviousInputs,
		IDictionary<InputReferences, InputEvents> player1CurrentInputs,
		IDictionary<InputReferences, InputEvents> player2PreviousInputs,
		IDictionary<InputReferences, InputEvents> player2CurrentInputs
	){
		base.DoFixedUpdate(player1PreviousInputs, player1CurrentInputs, player2PreviousInputs, player2CurrentInputs);

		this.DefaultNavigationSystem(
			player1PreviousInputs,
			player1CurrentInputs,
			player2PreviousInputs,
			player2CurrentInputs,
			this.moveCursorSound,
			this.selectSound,
			this.cancelSound
		);
	}

	public void health_character ()
    {
		//Debug.Log(health.text + character.text);
		//PlayerPrefs.SetInt("Health", int.Parse(health.text));

		//UFE.config.storyMode.selectableCharactersInStoryMode.Add(int.Parse(character.text));
	}

    private void Start()
    {

		if(PlayerPrefs.GetInt("music") == 1)
        {
			UFE.SetMusic(true);
        } else
        {
			UFE.SetMusic(false);
        }

		if(PlayerPrefs.GetInt("sound") == 1)
        {
			UFE.SetSoundFX(true);
        } else
        {
			UFE.SetSoundFX(false);
        }

		UFE.StartStoryMode();
		int loadLevelNumber = PlayerPrefs.GetInt("selectedLevel") - 1;
		Debug.Log("Level Being Loaded is = " + loadLevelNumber);
		
		UFE._StartStoryModeBattle( loadLevelNumber, 0.5f);
	}


    public override void OnShow (){
		base.OnShow ();

		foreach (var item in UFE.config.storyMode.selectableCharactersInStoryMode)
		{
			Debug.Log(item);
		}


		
		

		this.HighlightOption(this.FindFirstSelectable());

		if (this.music != null){
			UFE.DelayLocalAction(delegate(){UFE.PlayMusic(this.music);}, this.delayBeforePlayingMusic);
		}
		
		if (this.stopPreviousSoundEffectsOnLoad){
			UFE.StopSounds();
		}
		
		if (this.onLoadSound != null){
			UFE.DelayLocalAction(delegate(){UFE.PlaySound(this.onLoadSound);}, this.delayBeforePlayingMusic);
		}

		if (buttonNetwork != null) {
			buttonNetwork.interactable = UFE.isNetworkAddonInstalled || UFE.isBluetoothAddonInstalled;
		}

		if (buttonBluetooth != null){
            buttonBluetooth.interactable = UFE.isBluetoothAddonInstalled;
        }
	}
	#endregion
}
