// chau tran
// where to put:	"Game Controller" object
// purpose:			set player stats like turret level, damage level, armor level and change the mesh of the tank

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretSet
{
	public string name;
	public GameObject turretObject;
	public float fireRate_main;
	public float fireRate_secondary;
}

[System.Serializable]
public class ArmorSet
{
	public string name;
	public GameObject armorObject;
	public float HealthCapacity;
}

[System.Serializable]
public class DamageSet
{
	public string name;
	public GameObject shellObject;
	public float damageRate_main;
	public float damageRate_secondary;
}

public class BuildingPlayer : MonoBehaviour {

	GameObject playerTankObject;
	PlayerTankShoot tankShootComponent;
	TankHealth tankHealthComponent;

	public Transform turretPosition;
	public Transform tankBody;

	public List<TurretSet> turretSets;
	public List<ArmorSet> armorSets;
	public List<DamageSet> damageSets;



	void Awake()
	{
		playerTankObject = GameObject.FindGameObjectWithTag ("Player");
		tankShootComponent = playerTankObject.GetComponent<PlayerTankShoot> ();
		tankHealthComponent = playerTankObject.GetComponent<TankHealth> ();
	}
	void Start()
	{
		SetupTurret ();
		SetupArmor ();
		SetupDamage ();
	}
	void SetupTurret()
	{	
		if (PlayerPrefsManager.GetTurretLevel () == 1) 
		{			
			tankShootComponent.SetFireRate (turretSets[1].fireRate_main);
			tankShootComponent.SetFireRate_Secondary (turretSets[1].fireRate_secondary);
			// change mesh
			tankShootComponent.ChangeTurretMesh(turretSets[1].turretObject);
		} 
		else if (PlayerPrefsManager.GetTurretLevel () == 2) 
		{
			tankShootComponent.SetFireRate (turretSets[2].fireRate_main);
			tankShootComponent.SetFireRate_Secondary (turretSets[2].fireRate_secondary);
			// change mesh
			tankShootComponent.ChangeTurretMesh(turretSets[2].turretObject);

		} 
		else if (PlayerPrefsManager.GetTurretLevel () == 3) 
		{
			tankShootComponent.SetFireRate (turretSets[3].fireRate_main);
			tankShootComponent.SetFireRate_Secondary (turretSets[3].fireRate_secondary);
			// change mesh
			tankShootComponent.ChangeTurretMesh(turretSets[3].turretObject);

		} 
		else if(PlayerPrefsManager.GetTurretLevel () == 0)
		{// no upgrade yet
			tankShootComponent.SetFireRate (turretSets[0].fireRate_main);
			tankShootComponent.SetFireRate_Secondary (turretSets[0].fireRate_secondary);
			// change mesh
			tankShootComponent.ChangeTurretMesh(turretSets[0].turretObject);
		}
	}
	void SetupArmor()
	{
		if (PlayerPrefsManager.GetArmorLevel () == 1) 
		{
			tankHealthComponent.SetTankFullHealth (armorSets [1].HealthCapacity);
		} 
		else if (PlayerPrefsManager.GetArmorLevel () == 2) 
		{
			tankHealthComponent.SetTankFullHealth (armorSets [2].HealthCapacity);
		} 
		else if (PlayerPrefsManager.GetArmorLevel () == 3) 
		{
			tankHealthComponent.SetTankFullHealth (armorSets [3].HealthCapacity);
		} 
		else if(PlayerPrefsManager.GetArmorLevel () == 0)
		{// no upgrade yet
			tankHealthComponent.SetTankFullHealth (armorSets [0].HealthCapacity);
		}
	}
	void SetupDamage()
	{
		if (PlayerPrefsManager.GetDamageLevel () == 1)
		{
			tankShootComponent.SetDamageRate (damageSets[1].damageRate_main);
			tankShootComponent.SetDamageRate_Secondary (damageSets[1].damageRate_secondary);

			tankShootComponent.ChangeBulletPrefab (damageSets[1].shellObject);
		}
		else if (PlayerPrefsManager.GetDamageLevel () == 2)
		{
			tankShootComponent.SetDamageRate (damageSets[2].damageRate_main);
			tankShootComponent.SetDamageRate_Secondary (damageSets[2].damageRate_secondary);

			tankShootComponent.ChangeBulletPrefab (damageSets[2].shellObject);
		}
		else if (PlayerPrefsManager.GetDamageLevel () == 3)
		{
			tankShootComponent.SetDamageRate (damageSets[3].damageRate_main);
			tankShootComponent.SetDamageRate_Secondary (damageSets[3].damageRate_secondary);

			tankShootComponent.ChangeBulletPrefab (damageSets[3].shellObject);
		}
		else if(PlayerPrefsManager.GetDamageLevel () == 0)
		{// no upgrade yet
			tankShootComponent.SetDamageRate (damageSets[0].damageRate_main);
			tankShootComponent.SetDamageRate_Secondary (damageSets[0].damageRate_secondary);

			tankShootComponent.ChangeBulletPrefab (damageSets[0].shellObject);
		}
	}
}
