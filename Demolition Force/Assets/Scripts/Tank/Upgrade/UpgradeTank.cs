// chau tran
// purpose: store the upgrade functions
// where to put: Game Controller object

using UnityEngine;
using System.Collections;

public class UpgradeTank : MonoBehaviour {

	public Transform TurretPosition;
	public MeshRenderer shellRenderer1;
	public MeshRenderer shellRenderer2;

	public GameObject[] turretModels;
	public Material[] mats;
//	public GameObject[] bodyModels;

	void Start()
	{
		if (PlayerPrefsManager.GetTurretLevel () == 0) {
			Instantiate (turretModels[0],TurretPosition.position,TurretPosition.rotation,TurretPosition);
		} else if (PlayerPrefsManager.GetTurretLevel () == 1) {
			Instantiate (turretModels[1],TurretPosition.position,TurretPosition.rotation,TurretPosition);
		}else if (PlayerPrefsManager.GetTurretLevel () == 2) {
			Instantiate (turretModels[2],TurretPosition.position,TurretPosition.rotation,TurretPosition);
		}
		else if (PlayerPrefsManager.GetTurretLevel () == 3) {
			Instantiate (turretModels[3],TurretPosition.position,TurretPosition.rotation,TurretPosition);
		}

		if (PlayerPrefsManager.GetDamageLevel () == 0) {
			shellRenderer1.material = mats [0];
			shellRenderer2.material = mats [0];
		} else if (PlayerPrefsManager.GetDamageLevel () == 1) {
			shellRenderer1.material = mats [1];
			shellRenderer2.material = mats [1];
		}else if (PlayerPrefsManager.GetDamageLevel () == 2) {
			shellRenderer1.material = mats [2];
			shellRenderer2.material = mats [2];
		}else if (PlayerPrefsManager.GetDamageLevel () == 3) {
			shellRenderer1.material = mats [3];
			shellRenderer2.material = mats [3];
		}
	}
	// visualizing upgrade
	// turret
	public void TurretMeshLvl1()
	{
		TurretPosition.GetChild (0).gameObject.SetActive (false);
		TurretPosition.DetachChildren ();
		Instantiate (turretModels[1],TurretPosition.position,TurretPosition.rotation,TurretPosition);
	}
	public void TurretMeshLvl2()
	{
		TurretPosition.GetChild (0).gameObject.SetActive (false);
		TurretPosition.DetachChildren ();
		Instantiate (turretModels[2],TurretPosition.position,TurretPosition.rotation,TurretPosition);
	}
	public void TurretMeshLvl3()
	{
		TurretPosition.GetChild (0).gameObject.SetActive (false);
		TurretPosition.DetachChildren ();
		Instantiate (turretModels[3],TurretPosition.position,TurretPosition.rotation,TurretPosition);
	}
	// shell
	public void ShellLvl1()
	{
		shellRenderer1.material = mats [1];
		shellRenderer2.material = mats [1];
	}
	public void ShellLvl2()
	{
		shellRenderer1.material = mats [2];
		shellRenderer2.material = mats [2];
	}
	public void ShellLvl3()
	{
		shellRenderer1.material = mats [3];
		shellRenderer2.material = mats [3];
	}

	// turret
	public void UpgradeTurret1()
	{
		PlayerPrefsManager.SetTurretLevel (1);
		PlayerPrefsManager.AddGubs (-500);
	}
	public void UpgradeTurret2()
	{
		PlayerPrefsManager.SetTurretLevel (2);
		PlayerPrefsManager.AddGubs (-1000);
	}
	public void UpgradeTurret3()
	{
		PlayerPrefsManager.SetTurretLevel (3);
		PlayerPrefsManager.AddGubs (-1500);
	}
	// damage
	public void UpgradeDamage1()
	{
		PlayerPrefsManager.SetDamageLevel (1);
		PlayerPrefsManager.AddGubs (-500);
	}
	public void UpgradeDamage2()
	{
		PlayerPrefsManager.SetDamageLevel (2);
		PlayerPrefsManager.AddGubs (-1000);
	}
	public void UpgradeDamage3()
	{
		PlayerPrefsManager.SetDamageLevel (3);
		PlayerPrefsManager.AddGubs (-1500);
	}
	// armor
	public void UpgradeArmor1()
	{
		PlayerPrefsManager.SetArmorLevel (1);
		PlayerPrefsManager.AddGubs (-500);
	}
	public void UpgradeArmor2()
	{
		PlayerPrefsManager.SetArmorLevel (2);
		PlayerPrefsManager.AddGubs (-1000);
	}
	public void UpgradeArmor3()
	{
		PlayerPrefsManager.SetArmorLevel (3);
		PlayerPrefsManager.AddGubs (-1500);
	}
		
}
