using UnityEngine;
using System.Collections;
using UFE3D;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryModeTextureScreen : StoryModeScreen {
	#region public instance properties
	public AudioClip onLoadSound;
	public AudioClip music;
	public bool skippable = true;
	public bool stopPreviousSoundEffectsOnLoad = false;
	public float delayBeforePlayingMusic = 0.1f;
	public float delayBeforeGoingToNextScreen = 3f;
	public float minDelayBeforeSkipping = 0.1f;
	#endregion

	public Slider slider;
	public float fillTime = 7f; // Time in seconds to fill the slider completely
	//public string levelToLoad = "NextLevel"; // Name of the level to load

	private float currentTime = 0f;
	private bool isSliderFilled = false;



    #region public override methods

    private void Start()
    {
		AdsManager.Instance.HideBanner();
		AdsManager.Instance.ShowAdmobInterstitial();
		AdsManager.Instance.ShowBannerRectangle();
	}

    public override void OnShow (){
		base.OnShow ();
		
		if (this.music != null){
			UFE.DelayLocalAction(delegate(){UFE.PlayMusic(this.music);}, this.delayBeforePlayingMusic);
		}
		
		if (this.stopPreviousSoundEffectsOnLoad){
			UFE.StopSounds();
		}
		
		if (this.onLoadSound != null){
			UFE.DelayLocalAction(delegate(){UFE.PlaySound(this.onLoadSound);}, this.delayBeforePlayingMusic);
		}
		
		this.StartCoroutine(this.ShowScreen());
	}


	private void Update()
	{
		// Check if the slider is not yet filled completely
		if (!isSliderFilled)
		{
			currentTime += Time.deltaTime;

			// Calculate the fill percentage based on current time and fillTime
			float fillPercentage = currentTime / fillTime;
			slider.value = fillPercentage;

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
		AdsManager.Instance.HideAdmobBannerRectangle();
		this.GoToNextScreen();
    }

	public void Nextlevel()
    {
		AdsManager.Instance.HideAdmobBannerRectangle();
		int currentLevel = PlayerPrefs.GetInt("selectedLevel");
		PlayerPrefs.SetInt($"level{currentLevel}", 1);
		PlayerPrefs.SetInt("selectedLevel", currentLevel + 1);


		this.GoToNextScreen();
    }

	public void restart()
	{
		AdsManager.Instance.HideAdmobBannerRectangle();
		int currentLevel = PlayerPrefs.GetInt("selectedLevel");
		PlayerPrefs.SetInt($"level{currentLevel}", 1);
		UFE.RestartMatch();
	}


	public void GoToMainMenu()
    {
		AdsManager.Instance.HideAllAds();
		UFE.EndGame(true);
		
		SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

	


	public virtual IEnumerator ShowScreen(){
		float startTime = Time.realtimeSinceStartup;
		float time = 0f;
		
		while(
			time < this.delayBeforeGoingToNextScreen && 
			!(skippable && Input.anyKeyDown && time > this.minDelayBeforeSkipping)
		){
			yield return null;
			time = Time.realtimeSinceStartup - startTime;
		}
		
		this.GoToNextScreen();
	}
	#endregion
}
