//Sam Silverman
//This script is here just to make sure player preference are not set to null when game starts
//This is just an "in case" script to cover our behinds
//It is here to check if is new or if data has been wiped

using UnityEngine;
using System.Collections;

public class PlayerPrefsSetManager : MonoBehaviour {

	void Start () {
		//IF PLAYER HAS STARTED GAME FOR FIRST TIME, SET PREFS TO DEFAULT
		if(PlayerPrefsManager.GetFirstPlay() == 0){


			PlayerPrefsManager.SetInvertedX(false);
			PlayerPrefsManager.SetInvertedY(false);
			PlayerPrefsManager.SetSensitivityX(4f);
			PlayerPrefsManager.SetSensitivityY(1.4f);
			PlayerPrefsManager.SetFirstPlay(1);
			PlayerPrefsManager.SetMusicVolume(1f);
			PlayerPrefsManager.SetSFXVolume(1f);
			PlayerPrefsManager.SetGubs (0);
			PlayerPrefsManager.SetMasterVolume (1f);
		} else {
			//do nothing
			}
	}


}
