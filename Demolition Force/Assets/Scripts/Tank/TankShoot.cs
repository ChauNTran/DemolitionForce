//	chau tran
// where to put:	in the tank
//	purpose:		to shoot objects when the player click "Fire1"

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TurretInfo{
	
	public Transform shootingPosition ;
	public Transform turret;
	public Rigidbody bulletPrefab;
	public float shootingSpeed ;
}

[System.Serializable]
public class TurretInfoSecondary{

	public Transform shootingPosition ;
	public Transform turret;
	public Rigidbody bulletPrefab;
	public GameObject gatlingGunEffect;
	public float shootingSpeed;
}

public class TankShoot : MonoBehaviour {

	public List<TurretInfo> turretInfos;
	public List<TurretInfoSecondary> turretInfos_secondary;

	public bool holdAndShoot = false;


//	public AudioClip ShootingSoundEffect;
	private AudioSource audiSource;
	public AudioSource audiSource2;
	private Rigidbody rigid;

	protected float damageRate = 10f;
	protected float damageRate_Secondary = 1f;
	void Start()
	{
		rigid = GetComponent<Rigidbody> ();
		audiSource = GetComponent<AudioSource> ();
	}
//	// fire rate
//	public void SetFireRate(float waitTime)
//	{
//		shootWaitTime_primary = waitTime;
//	}
//	public void SetFireRate_Secondary (float waitTime)
//	{
//		shootWaitTime_secondary = waitTime;
//	}
//	public float GetFireRate_primary()
//	{
//		return shootWaitTime_primary;
//	}
//	public float GetFireRate_Secondary()
//	{
//		return shootWaitTime_secondary;
//	}
//	// damage rate
	public void SetDamageRate(float damage)
	{
		damageRate = damage;
	}
	public void SetDamageRate_Secondary (float damage)
	{
		damageRate_Secondary = damage;
	}
	// damage rate for power up
	public void AddDamageRate(float damage)
	{
		damageRate += damage;
	}
	public void AddDamageRate_Secondary(float damage)
	{
		damageRate_Secondary += damage;
	}

	public void Shoot()
	{
		foreach(TurretInfo turretInfo in turretInfos)
		{
			Quaternion missileRotation = Quaternion.Euler (turretInfo.turret.rotation.eulerAngles.x,turretInfo.turret.rotation.eulerAngles.y,turretInfo.turret.rotation.eulerAngles.z);
			Rigidbody rocketInstance = Instantiate(turretInfo.bulletPrefab, turretInfo.shootingPosition.position, missileRotation) as Rigidbody;
			rocketInstance.gameObject.GetComponent<BulletRaycastHit>().SetDamageRate(damageRate);
			if (this.gameObject.tag == "Player")
				rocketInstance.gameObject.GetComponent<BulletRaycastHit> ().SetIgnoreShieldTrue ();
			
			rocketInstance.velocity = turretInfo.shootingPosition.forward * turretInfo.shootingSpeed; //test

            Instantiate(Resources.Load("Explosions/explosion_muzzleFlash"), turretInfo.shootingPosition.position, Quaternion.identity);
		}

	}
	public void ShootSecondary()
	{
		foreach(TurretInfoSecondary turretInfo_secondary in turretInfos_secondary)
		{
			
			Quaternion missileRotation = Quaternion.Euler (turretInfo_secondary.turret.rotation.eulerAngles.x,turretInfo_secondary.turret.rotation.eulerAngles.y,turretInfo_secondary.turret.rotation.eulerAngles.z);
			Rigidbody rocketInstance_secondary = Instantiate(turretInfo_secondary.bulletPrefab, turretInfo_secondary.shootingPosition.position, missileRotation) as Rigidbody;
			rocketInstance_secondary.gameObject.GetComponent<BulletRaycastHit>().SetDamageRate(damageRate_Secondary);

			//	if shootting from the player ignore the shield
			if (this.gameObject.tag == "Player")
				rocketInstance_secondary.gameObject.GetComponent<BulletRaycastHit> ().SetIgnoreShieldTrue ();
			rocketInstance_secondary.velocity = turretInfo_secondary.shootingPosition.forward * turretInfo_secondary.shootingSpeed + rigid.velocity;

		}
	}
	public void PlaySecondaryGunSound()
	{
		foreach (TurretInfoSecondary turretInfo_secondary in turretInfos_secondary) {
			turretInfo_secondary.gatlingGunEffect.SetActive (true);
		}
//		audiSource2.Play ();
	}

	public void StopSecondaryGunSound()
	{
		foreach (TurretInfoSecondary turretInfo_secondary in turretInfos_secondary) {
			turretInfo_secondary.gatlingGunEffect.SetActive (false);
		}
//		audiSource2.Stop ();
	}
}