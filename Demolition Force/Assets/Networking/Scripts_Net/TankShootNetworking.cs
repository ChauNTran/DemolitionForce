//	chau tran
// where to put:	in the tank
//	purpose:		to shoot objects when the player click "Fire1"

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

[RequireComponent(typeof(TankTurretControlNetworking))]

[System.Serializable]
public class TurretInfoNetworking{
	
	public Transform shootingPosition ;
	public Transform turret;
	public Rigidbody bulletPrefab;
	public float shootingSpeed ;// meter/second
}

[System.Serializable]
public class TurretInfoSecondaryNetworking{

	public Transform shootingPosition ;
	public Transform turret;
	public Rigidbody bulletPrefab;
	public GameObject gatlingGunEffect;
	public float shootingSpeed;
}
public enum SecondaryWeaponType 
{
	regular,
	flameThrower
}

public class TankShootNetworking : NetworkBehaviour {

	public List<TurretInfoNetworking> turretInfos;
	public List<TurretInfoSecondaryNetworking> turretInfos_secondary;

	public bool holdAndShoot = false;
	public Transform TurretPosition;

//	public AudioClip ShootingSoundEffect;
	private Rigidbody rigid;

	SecondaryWeaponType sdWpnTyp;

	[SerializeField][SyncVar]protected float damageRate = 10f;
	[SerializeField][SyncVar]protected float damageRate_Secondary = 1f;
	[SerializeField]private float shootWaitTime_primary = 0.2f;	// also control fire rate
	[SerializeField]private float shootWaitTime_secondary = 0.1f;

	//collision variables for collision attack
	public float maxCollisionDamage;
	private float collisionCoolDown = 3f;
	private bool coolDownReady = true;

	public SecondaryWeaponType scdWT;
	public GameObject[] flamethrowers;
	public List<ParticleCollisionEvent> collisionEvents;
	public GameUINetworking gameUIScipt;

	public float flameCoolDownTime = 15f; //howe long until you cna shoot flames again
	public float flameTimeOut = 7f; //how long the flame spouts fire for
	float flameCounter;
	bool flameOnOff = true;
	[SyncVar]bool flameCoolDownReady = true;
	public float flameThrowerDamage = 1f;
	float damage_timer;
	float damage_main;
	float damage_second;

	public float GetFireRate_primary()
	{
		return shootWaitTime_primary;
	}
	public float GetFireRate_Secondary()
	{
		return shootWaitTime_secondary;
	}
	[ClientRpc]
	public void RpcAddDamageRate(float main, float second, float timer)
	{
		damage_main = main;
		damage_second = damage_second;
		damage_timer = timer;
		damageRate += damage_main;
		damageRate_Secondary += damage_second;
		damage_timer = timer;
		if (isLocalPlayer) {
			StartCoroutine (Damage_Countdown());
		}
	}
	[Command]
	public void CmdResetDamageRate()
	{
		RpcResetDamageRate ();
	}
	[ClientRpc]
	public void RpcResetDamageRate()
	{
		damageRate -= damage_main;
		damageRate_Secondary -= damage_second;
	}
	IEnumerator Damage_Countdown()
	{
		gameUIScipt.SetDamageCounterText (damage_timer);
		yield return new WaitForSeconds (1f);
		damage_timer -= 1f;

		if (damage_timer <=0f) {
			damage_timer = 0f;
			CmdResetDamageRate();
			gameUIScipt.DamageCounterReset();
	
		}
		else
			StartCoroutine (Damage_Countdown());
	}

	void Start()
	{
		collisionEvents = new List<ParticleCollisionEvent> ();//for flame thrower particle
		rigid = GetComponent<Rigidbody> ();
		gameUIScipt = GameObject.FindGameObjectWithTag ("Game UI").GetComponent<GameUINetworking> ();
	}
	[ServerCallback]
	void Update()
	{
		FlameThrowerCheck ();
	}

	public void Shoot()
	{
		foreach (TurretInfoNetworking turretInfo in turretInfos) 
		{
			Quaternion missileRotation = Quaternion.Euler (turretInfo.turret.rotation.eulerAngles.x,turretInfo.turret.rotation.eulerAngles.y,turretInfo.turret.rotation.eulerAngles.z);

			CmdShoot (missileRotation,turretInfo.shootingPosition.position,turretInfo.shootingPosition.rotation,turretInfo.shootingPosition.forward);
		}
	}

	[Command]// send from client to server
	public void CmdShoot(Quaternion missileRot, Vector3 shootingPos,Quaternion shootRotation,Vector3 dir)
	{
		//foreach(TurretInfoNetworking turretInfo in turretInfos)
		//{
			// spawn the bullet with raycast
			//Quaternion missileRotation = Quaternion.Euler (turretInfo.turret.rotation.eulerAngles.x,turretInfo.turret.rotation.eulerAngles.y,turretInfo.turret.rotation.eulerAngles.z);
			Rigidbody rocketInstance = Instantiate(turretInfos[0].bulletPrefab, shootingPos, missileRot) as Rigidbody;
			rocketInstance.velocity = dir * turretInfos[0].shootingSpeed; //still shoot with velocity
			var projectileObj = rocketInstance.gameObject.GetComponent<DestroyWhenRaycastHit>();
			projectileObj.projectileOwner = GetComponent<PlayerMultiplayer> ();

			rocketInstance.gameObject.GetComponent<DestroyWhenRaycastHit>().SetDamageRate(damageRate);
			NetworkServer.Spawn(rocketInstance.gameObject); 

			RpcFireShotFX (shootingPos,shootRotation);

			// spawn the bullet with the collider

//			DestroyWhenCollideNetworking projectileObj = null;
//			Quaternion missileRotation = Quaternion.Euler (turretInfo.turret.rotation.eulerAngles.x,turretInfo.turret.rotation.eulerAngles.y,turretInfo.turret.rotation.eulerAngles.z);
//			Rigidbody rocketInstance = Instantiate(turretInfo.bulletPrefab, turretInfo.shootingPosition.position, missileRotation) as Rigidbody;
//			rocketInstance.gameObject.GetComponent<DestroyWhenCollideNetworking>().SetDamageRate(damageRate_Secondary);
//
//			//	if shootting from the player ignore the shield
//			if (this.gameObject.tag == "Player")
//				rocketInstance.gameObject.GetComponent<DestroyWhenCollideNetworking> ().SetIgnoreShieldTrue ();
//
//			projectileObj = rocketInstance.gameObject.GetComponent<DestroyWhenCollideNetworking> ();
//			projectileObj.projectileOwner = GetComponent<PlayerMultiplayer> ();
//
//			rocketInstance.velocity = turretInfo.shootingPosition.forward * turretInfo.shootingSpeed + rigid.velocity;
//			NetworkServer.Spawn(rocketInstance.gameObject);
//			RpcFireShotFX ();
		//}
	}

	[ClientRpc]
	void RpcFireShotFX(Vector3 position,Quaternion rotation)
	{
		
		Instantiate(Resources.Load("Explosions/explosion_muzzleFlash"), position, rotation);

	}

	[Command]
	public void CmdShootSecondary()
	{
		if (scdWT == SecondaryWeaponType.regular) {
			foreach (TurretInfoSecondaryNetworking turretInfo_secondary in turretInfos_secondary) {
				DestroyWhenCollideNetworking projectileObj = null;
				Quaternion missileRotation = Quaternion.Euler (turretInfo_secondary.turret.rotation.eulerAngles.x, turretInfo_secondary.turret.rotation.eulerAngles.y, turretInfo_secondary.turret.rotation.eulerAngles.z);
				Rigidbody rocketInstance_secondary = Instantiate (turretInfo_secondary.bulletPrefab, turretInfo_secondary.shootingPosition.position, missileRotation) as Rigidbody;
				rocketInstance_secondary.gameObject.GetComponent<DestroyWhenCollideNetworking> ().SetDamageRate (damageRate_Secondary);

				//	if shootting from the player ignore the shield
				if (this.gameObject.tag == "Player")
					rocketInstance_secondary.gameObject.GetComponent<DestroyWhenCollideNetworking> ().SetIgnoreShieldTrue ();
			
				projectileObj = rocketInstance_secondary.gameObject.GetComponent<DestroyWhenCollideNetworking> ();
				projectileObj.projectileOwner = GetComponent<PlayerMultiplayer> ();

				rocketInstance_secondary.velocity = turretInfo_secondary.shootingPosition.forward * turretInfo_secondary.shootingSpeed + rigid.velocity;
				NetworkServer.Spawn (rocketInstance_secondary.gameObject); 
			}
		}else if(scdWT == SecondaryWeaponType.flameThrower){
			if (flameCoolDownReady) {
				flameCoolDownReady = false;
				flamethrowers [0].SetActive (true);
				flamethrowers [1].SetActive (true);
				RpcActivateFlameThrower ();
			}
		}
	}
	//Flame thrower fucntions to activate and deactivate flame thrower over the network
	[ClientRpc]
	void RpcActivateFlameThrower()
	{
		flamethrowers [0].SetActive (true);
		flamethrowers [0].GetComponent<ParticleSystem> ().Play ();
		flamethrowers [1].SetActive (true);
		flamethrowers [1].GetComponent<ParticleSystem> ().Play ();

	}
	[ClientRpc]
	void RpcDeactivateFlameThrower()
	{
		flamethrowers [1].GetComponent<ParticleSystem> ().Stop ();
		flamethrowers [0].GetComponent<ParticleSystem> ().Stop ();

	}
	void FlameThrowerCheck()
	{
		if (!flameCoolDownReady) 
		{
			flameCounter += Time.deltaTime;
			if (flameCounter > flameTimeOut && flameOnOff ) 
			{
				flameOnOff = false;
				RpcDeactivateFlameThrower ();
				flamethrowers [1].GetComponent<ParticleSystem> ().Stop ();
				flamethrowers [0].GetComponent<ParticleSystem> ().Stop ();
			}
			else if (flameCounter > flameCoolDownTime) 
			{
				flameCounter = 0;
				flameCoolDownReady = true;
				flameOnOff = true;
			}
		}
	}

//	void OnParticleTrigger(GameObject other)
//	{
//		if (isServer) 
//		{
//			//print ("colliding on server");
//			//GameObject go = other.transform.root.gameObject;
//			if(other.transform.root.gameObject.tag == "Player")
				//print(other.gameObject.transform.root.gameObject.GetComponent<PlayerMultiplayer>().playerName); 

//				TankHealthNetworking enemyHealth = other.transform.root.gameObject.GetComponent<TankHealthNetworking> ();
//				enemyHealth.whoKilledMe = this.GetComponent<PlayerMultiplayer> ();
//				enemyHealth.TakeDamage (flameThrowerDamage);
//				if (enemyHealth.isDead) {
//					if (enemyHealth.deadOnce == false) {
//						GetComponent<PlayerMultiplayer>().AddKillCount ();
//						enemyHealth.deadOnce = true;
//
//					}
//					GameControllerNetworking.Instance.UpdateScoreBoard ();
//					GameControllerNetworking.Instance.RpcShowKillUI (GetComponent<PlayerMultiplayer>().playerName,go.transform.root.gameObject.GetComponentInParent<PlayerMultiplayer>().playerName);
//				}
			
		
	
//	}
	//Checks Collision to set up cars for collision damage
	void OnCollisionEnter(Collision other)
	{
		if (other.transform != transform && other.contacts.Length != 0) 
		{ //check to get collision point
			for (int i = 0; i < other.contacts.Length; i++) 
			{
				//instantiate spark at other.contacts[i].point ***

				AudioManager.instance.PlaySound ("Vehicle Collisions", other.contacts [i].point);		//plays collision sound ** add new sound affects
	
				if (other.contacts [i].thisCollider.name == "BumperCollider") //if the bumper collider is collided with
				{
					CollisionAttack (other, i);
				}
			}
		}
	}

	public void CollisionAttack(Collision other, int point)
	{
//		if (other.transform != transform && other.contacts.Length != 0) { //check to get collision point
//			for (int i = 0; i < other.contacts.Length; i++) {
//				//instantiate spark at other.contacts[i].point
				AudioManager.instance.PlaySound ("Vehicle Collisions", other.contacts[point].point);
				if (isLocalPlayer) {
					CmdSendCurrentSpeed (gameObject.GetComponent<TankMotorNetworking>().currentSpeed);
							//plays collision sound

				}


				if (isServer)
				{

					if (other.gameObject.tag == "Player" && coolDownReady) 
					{
						coolDownReady = false;
						StartCoroutine (CollisionCoolDown ());//counts down cool down

						TankHealthNetworking enemyHealthScript = other.gameObject.GetComponent<TankHealthNetworking> ();

						//calculate damage ratio
						float collisionDamage;
						float collsionMultiplyer;
						
						
						//print ("current speed " + gameObject.GetComponent<TankMotorNetworking> ().currentSpeed);
						//print ("top speed " + gameObject.GetComponent<TankMotorNetworking> ().topSpeed);

						collsionMultiplyer = maxCollisionDamage / gameObject.GetComponent<TankMotorNetworking> ().topSpeed;
						collisionDamage = collsionMultiplyer * gameObject.GetComponent<TankMotorNetworking> ().currentSpeed;
						//print (collisionDamage);
						enemyHealthScript.TakeDamage ((int)collisionDamage);
						enemyHealthScript.whoKilledMe = gameObject.GetComponent<PlayerMultiplayer> ();

						if (enemyHealthScript.isDead) 
						{
							if (enemyHealthScript.deadOnce != true) 
							{
								GetComponent<PlayerMultiplayer> ().AddKillCount ();
								GameControllerNetworking.Instance.RpcShowKillUI (GetComponent<PlayerMultiplayer> ().playerName, other.gameObject.GetComponent<PlayerMultiplayer> ().playerName);
								enemyHealthScript.deadOnce = true;
							}
							GameControllerNetworking.Instance.UpdateScoreBoard ();
						}
					}
				}
			
		
	}
	//COLLISION ATTACKING...called from front bumper object

	[Command] //Sends current speed up
	void CmdSendCurrentSpeed(float currentSpeed){
		gameObject.GetComponent<TankMotorNetworking> ().currentSpeed = currentSpeed;

	}
	IEnumerator CollisionCoolDown()
	{
		float i = 0;

		while (i < collisionCoolDown)
		{
			i += 1f;
			yield return new WaitForSeconds (1f);
		}
		coolDownReady = true;

		yield return null;
	}
}