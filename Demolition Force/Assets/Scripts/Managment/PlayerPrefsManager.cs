//Sam Silverman
//Player prefs manager is a save fucntion script that stores personal prefs such as
//volume, controls, and more

using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_FIRST_PLAY = "firstPlay";

	const string MASTER_INVERTED_X_KEY = "invertedX";
	const string MASTER_INVERTED_Y_KEY = "invertedY";

	const string MASTER_SENSITIVITY_X_KEY = "sensitivityX";
	const string MASTER_SENSITIVITY_Y_KEY = "sensitivityY";

	const string MASTER_VOLUME_KEY = "masterVolume";
	const string MASTER_VOLUME_MUSIC_KEY = "musicVolume";
	const string MASTER_VOLUME_SFX_KEY = "sfxVolume";

	const string GUBS_TOTAL = "gubsTotal";

	const string TURRET_LEVEL = "turretLevel";
	const string DAMAGE_LEVEL = "damageLevel";
	const string ARMOR_LEVEL = "armorLevel";


	// PLAYER STATS
		// Turret
	public static void SetTurretLevel(int turretLevel)
	{
		PlayerPrefs.SetInt (TURRET_LEVEL, turretLevel);
	}
	public static int GetTurretLevel(){
		return PlayerPrefs.GetInt (TURRET_LEVEL);
	}
		// Damage
	public static void SetDamageLevel(int damageLevel)
	{
		PlayerPrefs.SetInt (DAMAGE_LEVEL, damageLevel);
	}
	public static int GetDamageLevel(){
		return PlayerPrefs.GetInt (DAMAGE_LEVEL);
	}
		// Armor
	public static void SetArmorLevel(int armorLevel)
	{
		PlayerPrefs.SetInt (ARMOR_LEVEL, armorLevel);
	}
	public static int GetArmorLevel(){
		return PlayerPrefs.GetInt (ARMOR_LEVEL);
	}
	//	GUBS PREFS
	public static void SetGubs(int numberOfGubs)
	{
		PlayerPrefs.SetInt (GUBS_TOTAL, numberOfGubs);
	}
	public static int GetGubs()
	{
		return PlayerPrefs.GetInt (GUBS_TOTAL);
	}
	public static void AddGubs(int numberToAdd)
	{
		numberToAdd += GetGubs ();
		SetGubs (numberToAdd);
	}
	//MASTER VOLUME
	public static void SetMasterVolume(float volume){
		if(volume >= 0 && volume <= 1){
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		}else{
			Debug.LogError("Mastervolume out of range");
		}
	}
	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}
	//MASTER MUSIC VOLUME PREFS
	public static void SetMusicVolume(float volume){
		if(volume >= 0 && volume <= 1){
			PlayerPrefs.SetFloat(MASTER_VOLUME_MUSIC_KEY, volume);
		}else{
			Debug.LogError("Master music volume out of range");
		}
	}
	public static float GetMusicVolume(){
		return PlayerPrefs.GetFloat(MASTER_VOLUME_MUSIC_KEY);
	}
	//MASTER SFX VOLUME PREFS
	public static void SetSFXVolume(float volume){
		if(volume >= 0 && volume <= 1){
			PlayerPrefs.SetFloat(MASTER_VOLUME_SFX_KEY, volume);
		}else{
			Debug.LogError("Master music volume out of range");
		}
	}
	public static float GetSFXVolume(){
		return PlayerPrefs.GetFloat(MASTER_VOLUME_SFX_KEY);
	}

	//PLAYER PREFS FOR INVERTED OPTION X
	public static void SetInvertedX(bool inverted){

		if(inverted){
			PlayerPrefs.SetInt (MASTER_INVERTED_X_KEY, 1);
		}else if(!inverted){
			PlayerPrefs.SetInt(MASTER_INVERTED_X_KEY, 0);
		}

	}
	public static bool GetInvertedX(){

		if(PlayerPrefs.GetInt(MASTER_INVERTED_X_KEY) == 1){
			return true;
		}else if(PlayerPrefs.GetInt(MASTER_INVERTED_X_KEY) == 0){
			return false;
		}	
		return false; 
	}
	//PLAYER PREFS FOR INVERTED OPTION Y
	public static void SetInvertedY(bool inverted){

		if(inverted){
			PlayerPrefs.SetInt (MASTER_INVERTED_Y_KEY, 1);
		}else if(!inverted){
			PlayerPrefs.SetInt(MASTER_INVERTED_Y_KEY, 0);
		}
	}
	public static bool GetInvertedY(){

		if(PlayerPrefs.GetInt(MASTER_INVERTED_Y_KEY) == 1){
			return true;
		}else if(PlayerPrefs.GetInt(MASTER_INVERTED_Y_KEY) == 0){
			return false;
		}	 
		return false;
	}
	//SENSITIVITY PLAYER PREFS OPTION X
	public static void SetSensitivityX(float sensitivity){
		if(sensitivity <= 10 && sensitivity >= 1){
		PlayerPrefs.SetFloat(MASTER_SENSITIVITY_X_KEY, sensitivity);
		}else{
			Debug.LogError("Sensitivity out of range");
		}
	}
	public static float GetSensitivityX(){
		return PlayerPrefs.GetFloat(MASTER_SENSITIVITY_X_KEY);
	}
	//SENSITIVITY PLAYER PREFS OPTION X
	public static void SetSensitivityY(float sensitivity){
		if(sensitivity <= 10 || sensitivity >= 1){
		PlayerPrefs.SetFloat(MASTER_SENSITIVITY_Y_KEY, sensitivity);
		}else{
			Debug.LogError("Sensitivity out of range");
		}
	}
	public static float GetSensitivityY(){
		return PlayerPrefs.GetFloat(MASTER_SENSITIVITY_Y_KEY);
	}

	//CHECKS TO SEE IF THIS IS FIRST TIME GAME IS PLAYED, SO TO SET DEFUALT SETTINGS
	public static void SetFirstPlay(int first){
		PlayerPrefs.SetInt(MASTER_FIRST_PLAY, first);
	}
	public static int GetFirstPlay(){
		return PlayerPrefs.GetInt(MASTER_FIRST_PLAY);
	}
}

