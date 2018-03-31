using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroyWhenRaycastHit : NetworkBehaviour
{
	public enum ProjectileType
	{
		turretProjectile, gatlingProjectile
	}
	public ProjectileType projectileType;

	public string explosionTitle;
	private Rigidbody rigid;
	private float maxTravelDistanceByFrame;
	private float DamageRate = 10f;
	private bool hit;
	public GameUINetworking GameUI;
	public PlayerMultiplayer projectileOwner;

	public void SetDamageRate(float newDamageRate)
	{
		this.DamageRate = newDamageRate;
	}
	void OnEnable()
	{
		rigid = GetComponent<Rigidbody> ();
		GameUI = GameObject.FindGameObjectWithTag ("Game UI").GetComponent<GameUINetworking> ();
		
	}
	[ServerCallback]
	void Update()
	{

		//print ("projectile in fix update");
		maxTravelDistanceByFrame = rigid.velocity.magnitude * Time.deltaTime;// how far the every frame
	
		Debug.DrawRay (transform.position, transform.forward * maxTravelDistanceByFrame, Color.red);

		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, maxTravelDistanceByFrame))
		{
			
			if (hit.collider.gameObject.layer ==  8 || hit.collider.gameObject.layer == 16) { // Tank layer, does use collider but layer **correction, does use collider
				print ("hit tank" + "--- part: " + hit.collider.gameObject.name);
					TankHealthNetworking enemyHealthScript = hit.collider.gameObject.transform.root.gameObject.GetComponent<TankHealthNetworking> ();
					enemyHealthScript.whoKilledMe = projectileOwner;
					enemyHealthScript.TakeDamage (DamageRate);

					//print (projectileOwner);
					//print (enemyHealthScript);
					
					
					if (enemyHealthScript.isDead) {
						if (enemyHealthScript.deadOnce == false) {
							projectileOwner.AddKillCount ();
							enemyHealthScript.deadOnce = true;

							
							
						}

					GameControllerNetworking.Instance.UpdateScoreBoard ();
					print (GameUI);
					print (projectileOwner.playerName);
					print (hit.collider.gameObject.transform.root.gameObject.GetComponentInParent<PlayerMultiplayer> ().playerName);
					//GameUI.RpcShowKillUI(projectileOwner.playerName,hit.collider.gameObject.transform.root.gameObject.GetComponentInParent<PlayerMultiplayer>().playerName);
					GameControllerNetworking.Instance.RpcShowKillUI (projectileOwner.playerName,hit.collider.gameObject.transform.root.gameObject.GetComponentInParent<PlayerMultiplayer>().playerName);
					}
				} else if (hit.collider.gameObject.layer == 9) {// red barrel
					print ("hit barrel");
					//destroy the projectile when it hit any collider

					hit.collider.gameObject.GetComponent<BarrelExplodesNetworking> ().BarrelExplosion_Networking ();
				} else if (hit.collider.gameObject.layer == 10) {// crate
					print ("hit box");
					hit.collider.gameObject.GetComponent<LootBoxNetworking> ().GetHit ();
					//destroy the projectile when it hit any collider

				} else if (hit.collider.gameObject.layer == 0) {
					//destroy the projectile when it hit any collider
				print("hit defualt object");
				}

			// Play explosion on both server and client
			//RpcPlayExplosion (hit.point);
			PlayExplosion (hit.point);
			//DetachParticles ();
			RpcDetachParticles ();

				NetworkServer.Destroy (this.gameObject);	
				
		}


	}
	[ClientRpc]
	void RpcPlayExplosion (Vector3 hitPosition)
	{
		GameObject explosion = Instantiate(Resources.Load(explosionTitle), hitPosition, Quaternion.identity) as GameObject;
		//NetworkServer.Spawn (explosion);
	}
	void PlayExplosion (Vector3 hitPosition)
	{
		GameObject explosion = Instantiate(Resources.Load(explosionTitle), hitPosition, Quaternion.identity) as GameObject;
		//NetworkServer.Spawn (explosion);
	}
	[ClientRpc]
	void RpcDetachParticles()
	{
		 //for smoke trail
			switch (projectileType) {
			case ProjectileType.turretProjectile:
				gameObject.transform.Find ("vfx_smokeTrail").gameObject.GetComponent<StopEmission> ().stopEmission ();
				transform.GetChild (0).parent = null;
				break;
			case ProjectileType.gatlingProjectile:

				break;
			}
		}
	void DetachParticles()
	{
		//for smoke trail
		switch (projectileType) {
		case ProjectileType.turretProjectile:
			gameObject.transform.Find ("vfx_smokeTrail").gameObject.GetComponent<StopEmission> ().stopEmission ();
			transform.GetChild (0).parent = null;
			break;
		case ProjectileType.gatlingProjectile:

			break;
		}
	}

}
