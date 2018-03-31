using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankShoot : TankShoot {
	
	private const float DEFAULT_FIRERATE =	2f;

	private float fireRate;

	void Start()
	{
		fireRate = DEFAULT_FIRERATE;
	}

	public float GetFireRate()
	{
		return fireRate;
	}
}
