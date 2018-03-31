//Sam Silverman
//Code compiles option values and sets them to player preference managment 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMananger : MonoBehaviour {

	public Slider sliderMasterVolume;
	public Slider sliderMusicVolume;
	public Slider sliderSfxVolume;
	public Slider sliderSensitvityX;
	public Slider sliderSensitvityY;
	public Toggle toggleInversionX;
	public Toggle toggleInversionY;
	public Toggle[] resolutionToggles;
	public int[] screenWidths;
	int activeScreenResIndex;



	void Start () {

		sliderSensitvityX.value = PlayerPrefsManager.GetSensitivityX ();
		sliderSensitvityY.value = PlayerPrefsManager.GetSensitivityY ();
		toggleInversionX.isOn = PlayerPrefsManager.GetInvertedX ();
		toggleInversionY.isOn = PlayerPrefsManager.GetInvertedY ();
		sliderMusicVolume.value = PlayerPrefsManager.GetMusicVolume ();
		sliderSfxVolume.value = PlayerPrefsManager.GetSFXVolume ();
		sliderMasterVolume.value = PlayerPrefsManager.GetMasterVolume ();

	}
		
	void Update(){
		AudioManager.instance.SetVolume (sliderSfxVolume.value, AudioManager.AudioChannel.Sfx);
		AudioManager.instance.SetVolume (sliderMasterVolume.value, AudioManager.AudioChannel.Master);
		AudioManager.instance.SetVolume (sliderMusicVolume.value, AudioManager.AudioChannel.Music);
	}

	public void SetScreenResolution(int i){
		if (resolutionToggles [i].isOn) {
			activeScreenResIndex = i;
			float aspectRatio = 16 / 9f;
			Screen.SetResolution (screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
		}
	}
	public void SetFullScreen(bool isFullscreen){
		for (int i = 0; i < resolutionToggles.Length; i++) {
			resolutionToggles [i].interactable = !isFullscreen;
		}

		if (isFullscreen) {
			Resolution[] allResolutions = Screen.resolutions;
			Resolution maxResolution = allResolutions[allResolutions.Length - 1];
			Screen.SetResolution(maxResolution.width, maxResolution.height, true);
		} else {
			SetScreenResolution (activeScreenResIndex);
		}
	}


	public void ApplyChanges(){
		print("changes applied");
		PlayerPrefsManager.SetSensitivityX(sliderSensitvityX.value);
		PlayerPrefsManager.SetSensitivityY(sliderSensitvityY.value);
		PlayerPrefsManager.SetInvertedX(toggleInversionX.isOn);
		PlayerPrefsManager.SetInvertedY(toggleInversionY.isOn);
		PlayerPrefsManager.SetMusicVolume(sliderMusicVolume.value);
		PlayerPrefsManager.SetSFXVolume(sliderSfxVolume.value);
		PlayerPrefsManager.SetMasterVolume(sliderMasterVolume.value);
	}
	public void SetDefault(){
		sliderMasterVolume.value = 1f;
		sliderSensitvityX.value = 1f;
		sliderSensitvityY.value = 1f;
		toggleInversionX.isOn = false;
		toggleInversionY.isOn = false;
		sliderSfxVolume.value = 1f;
		sliderMusicVolume.value = 1f;

	}
	public void ClearGameData(){
		
	}

}
