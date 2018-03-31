using UnityEngine;
using System.Collections;

public class settingPlayerPrefs : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		PlayerPrefsManager.SetDamageLevel (0);
		PlayerPrefsManager.SetTurretLevel (0);
		PlayerPrefsManager.SetArmorLevel (0);
		PlayerPrefsManager.SetGubs (2000);
	}

}
