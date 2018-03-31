// chau tran
// determine which button should be attivate and which should not
// game controller

// logic : only highlight the button that user can upgrade to (have enough money and is something the player doesn't
// have). The rest should be un-interactable
// the upgrade buttons will be unlocked when the player has upgraded the previous upgrade.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeManagement : MonoBehaviour {

	public Button turretButton1;
	public Button turretButton2;
	public Button turretButton3;
	public Button damageButton1;
	public Button damageButton2;
	public Button damageButton3;
	public Button armorButton1;
	public Button armorButton2;
	public Button armorButton3;

	Image tur2padlock;
	Image tur3padlock;
	Image arm2padlock;
	Image arm3padlock;
	Image dmg2padlock;
	Image dmg3padlock;

	void Awake()
	{
		if (turretButton1 == null || armorButton3 == null) {
			turretButton1 = GameObject.Find ("Turret_btn1").GetComponent<Button> ();
			turretButton2 = GameObject.Find ("Turret_btn2").GetComponent<Button> ();
			turretButton3 = GameObject.Find ("Turret_btn3").GetComponent<Button> ();
			damageButton1 = GameObject.Find ("Ammo_btn1").GetComponent<Button> ();
			damageButton2 = GameObject.Find ("Ammo_btn1").GetComponent<Button> ();
			damageButton3 = GameObject.Find ("Ammo_btn1").GetComponent<Button> ();
			armorButton1 = GameObject.Find ("Armor_btn1").GetComponent<Button> ();
			armorButton2 = GameObject.Find ("Armor_btn1").GetComponent<Button> ();
			armorButton3 = GameObject.Find ("Armor_btn1").GetComponent<Button> ();
		}

		tur2padlock = turretButton2.transform.Find ("padlock_img").GetComponent<Image> ();
		tur3padlock = turretButton3.transform.Find ("padlock_img").GetComponent<Image> ();
		dmg2padlock = damageButton2.transform.Find ("padlock_img").GetComponent<Image> ();
		dmg3padlock = damageButton3.transform.Find ("padlock_img").GetComponent<Image> ();
		arm2padlock = armorButton2.transform.Find ("padlock_img").GetComponent<Image> ();
		arm3padlock = armorButton3.transform.Find ("padlock_img").GetComponent<Image> ();
	}

	void Start ()
	{
		// greyout all
		AllGreyOut ();
		// lock all
		LockAll();

		// check it
		LogicCheck ();
	}
	public void LogicCheck()
	{
		// have from 500 to 1000
		if (PlayerPrefsManager.GetGubs () >= 500 && PlayerPrefsManager.GetGubs () < 1000) {
			if (PlayerPrefsManager.GetTurretLevel () == 0)
				turretButton1.interactable = true;
			if (PlayerPrefsManager.GetDamageLevel () == 0)
				damageButton1.interactable = true;
			if (PlayerPrefsManager.GetArmorLevel () == 0)
				armorButton1.interactable = true;
		} else if (PlayerPrefsManager.GetGubs () >= 1000 && PlayerPrefsManager.GetGubs () < 1500) {
			if (PlayerPrefsManager.GetTurretLevel () == 0)
				turretButton1.interactable = true;
			if (PlayerPrefsManager.GetTurretLevel () == 1) {
				turretButton2.interactable = true;
				tur2padlock.enabled = false;
			}
			if (PlayerPrefsManager.GetDamageLevel () == 0)
				damageButton1.interactable = true;
			if (PlayerPrefsManager.GetDamageLevel () == 1) {
				damageButton2.interactable = true;
				dmg2padlock.enabled = false;
			}
			if (PlayerPrefsManager.GetArmorLevel () == 0)
				armorButton1.interactable = true;
			if (PlayerPrefsManager.GetArmorLevel () == 1) {
				armorButton2.interactable = true;
				arm2padlock.enabled = false;
			}
		} else if (PlayerPrefsManager.GetGubs () >= 1500) {
			if (PlayerPrefsManager.GetTurretLevel () == 0)
				turretButton1.interactable = true;
			if (PlayerPrefsManager.GetTurretLevel () == 1) {
				turretButton2.interactable = true;
				tur2padlock.enabled = false;
			}
			if (PlayerPrefsManager.GetTurretLevel () == 2) {
				turretButton3.interactable = true;
				tur2padlock.enabled = false;
				tur3padlock.enabled = false;
			}

			if (PlayerPrefsManager.GetDamageLevel () == 0)
				damageButton1.interactable = true;
			if (PlayerPrefsManager.GetDamageLevel () == 1) {
				damageButton2.interactable = true;
				dmg2padlock.enabled = false;
			}
			if (PlayerPrefsManager.GetDamageLevel () == 2) {
				damageButton3.interactable = true;
				dmg2padlock.enabled = false;
				dmg3padlock.enabled = false;
			}

			if (PlayerPrefsManager.GetArmorLevel () == 0)
				armorButton1.interactable = true;
			if (PlayerPrefsManager.GetArmorLevel () == 1) {
				armorButton2.interactable = true;
				arm2padlock.enabled = false;
			}
			if (PlayerPrefsManager.GetArmorLevel () == 2) {
				armorButton3.interactable = true;
				arm2padlock.enabled = false;
				arm3padlock.enabled = false;
			}
		} else
			AllGreyOut ();
	}

	void AllGreyOut ()
	{
		turretButton1.interactable = false;
		turretButton2.interactable = false;
		turretButton3.interactable = false;
		damageButton1.interactable = false;
		damageButton2.interactable = false;
		damageButton3.interactable = false;
		armorButton1.interactable = false;
		armorButton2.interactable = false;
		armorButton3.interactable = false;
	}
	void LockAll()
	{
		tur2padlock.enabled = true;
		tur3padlock.enabled = true;
		dmg2padlock.enabled = true;
		dmg3padlock.enabled = true;
		arm2padlock.enabled = true;
		arm3padlock.enabled = true;
	}
}
