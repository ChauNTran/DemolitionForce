using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankTurretControl))]

public class PlayerTankShoot : TankShoot {

	public Transform TurretPosition;

	private float shootWaitTime_primary = 0.2f;	// also control fire rate
	private float shootWaitTime_secondary = 0.2f;

	//public float collisionDamage;

	// fire rate
	public void SetFireRate(float waitTime)
	{
		shootWaitTime_primary = waitTime;
	}
	public void SetFireRate_Secondary (float waitTime)
	{
		shootWaitTime_secondary = waitTime;
	}
	public float GetFireRate_primary()
	{
		return shootWaitTime_primary;
	}
	public float GetFireRate_Secondary()
	{
		return shootWaitTime_secondary;
	}
	// damage rate
//	public void SetDamageRate(float damage)
//	{
//		damageRate = damage;
//	}
//	public void SetDamageRate_Secondary (float damage)
//	{
//		damageRate_Secondary = damage;
//	}
//	// damage rate for power up
//	public void AddDamageRate(float damage)
//	{
//		damageRate += damage;
//	}
//	public void AddDamageRate_Secondary(float damage)
//	{
//		damageRate_Secondary += damage;
//	}

	public void ChangeTurretMesh(GameObject newTurretObject)
	{
		GameObject newTurret = Instantiate (newTurretObject, TurretPosition.position, Quaternion.identity, TurretPosition) as GameObject;
		foreach (TurretInfo turretInfo in turretInfos)
		{
			turretInfo.shootingPosition = newTurret.transform.Find ("Turret/Shooting Point");
			turretInfo.turret = newTurret.transform.Find ("Turret");
		}

		TankTurretControl tankTurretControl = GetComponent<TankTurretControl> ();
		tankTurretControl.SetUpTurret (newTurret.transform, newTurret.transform.Find ("Turret"));
	}

	public void ChangeBulletPrefab(GameObject newBulletPrefab)
	{
		foreach (TurretInfo turretInfo in turretInfos)
		{
			turretInfo.bulletPrefab = newBulletPrefab.GetComponent<Rigidbody>();
		}
	}
	//Collsion between vehicles and environment
	void OnCollisionEnter(Collision other)
	{
		if (other.transform != transform && other.contacts.Length != 0) {
			for (int i = 0; i < other.contacts.Length; i++) {
				//instantiate spark at other.contacts[i].point
				AudioManager.instance.PlaySound("Vehicle Collisions",other.contacts[i].point);
				if (other.gameObject.tag == "Player")
				{
					//deal collision damage
				}
			}
		}
	}
}
