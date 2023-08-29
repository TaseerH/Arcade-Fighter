using UnityEngine;
using System.Collections;
using UFE3D;
using UnityEngine.SceneManagement;

public class DefaultCongratulationsScreen : StoryModeScreen{
	#region public instance properties
	public AudioClip sound;
	public AudioClip music;
	public float delayBeforePlayingMusic = 0.1f;
	public float delayBeforeLoadingNextScreen = 3f;
	#endregion

	//public Slider slider;
	public float fillTime = 3f; // Time in seconds to fill the slider completely
								//public string levelToLoad = "NextLevel"; // Name of the level to load

	private float currentTime = 0f;
	private bool isSliderFilled = false;

	#region public override methods

	private void Update()
	{
		// Check if the slider is not yet filled completely
		if (!isSliderFilled)
		{
			currentTime += Time.deltaTime;

			// Calculate the fill percentage based on current time and fillTime
			float fillPercentage = currentTime / fillTime;
			//slider.value = fillPercentage;

			// Check if the slider is filled completely
			if (fillPercentage >= 1f)
			{
				isSliderFilled = true;
				NextScreen();
			}
		}
	}

	private void NextScreen()
	{
		UFE.EndGame(true);
		SceneManager.LoadScene(0);
	}

	public override void OnShow (){
		base.OnShow ();

		if (this.music != null){
			UFE.DelayLocalAction(delegate(){UFE.PlayMusic(this.music);}, this.delayBeforePlayingMusic);
		}
		
		if (this.sound != null){
			UFE.DelayLocalAction(delegate(){UFE.PlaySound(this.sound);}, this.delayBeforePlayingMusic);
		}

		//UFE.DelaySynchronizedAction(SceneManager.LoadScene(0), delayBeforeLoadingNextScreen);
	}
	#endregion
}
