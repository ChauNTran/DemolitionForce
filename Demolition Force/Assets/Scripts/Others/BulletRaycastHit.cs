// chau tran
// purpose: deal damage using raycast

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRaycastHit : MonoBehaviour {

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
	private bool ignoreShield = false;	// ignore shield when the bullet shoot FROM THE PLAYER

	public void SetDamageRate(float newDamageRate)
	{
		this.DamageRate = newDamageRate;
	}
	public void SetIgnoreShieldTrue()
	{
		this.ignoreShield = true;
	}
	void OnEnable()
	{
		rigid = GetComponent<Rigidbody> ();
	}
	void FixedUpdate()
	{
		//print ("projectile in fix update");
		maxTravelDistanceByFrame = rigid.velocity.magnitude * Time.deltaTime;// how far the every frame
		Debug.DrawRay (transform.position, transform.forward * maxTravelDistanceByFrame, Color.red);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, maxTravelDistanceByFrame)) {
			print (hit.collider.gameObject.layer);
			if (hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 16) { // Tank layer, does use collider but layer **correction, does use collider
				print ("hit tank" + "--- part: " + hit.collider.gameObject.name);
				TankHealth enemyHealthScript = hit.collider.gameObject.transform.root.gameObject.GetComponent<TankHealth> ();
				enemyHealthScript.TankDamage (DamageRate);

				print (enemyHealthScript);
				//GameUI.RpcShowKillUI(projectileOwner.playerName,hit.collider.gameObject.transform.root.gameObject.GetComponentInParent<PlayerMultiplayer>().playerName);
			} else if (hit.collider.gameObject.layer == 9) {// red barrel
				print ("hit barrel");
				//destroy the projectile when it hit any collider
				hit.collider.gameObject.GetComponent<BarrelExplodes> ().BarrelExplosion ();
			} else if (hit.collider.gameObject.layer == 10) {// crate
				print ("hit box");
				hit.collider.gameObject.GetComponent<DestroyableSettings> ().DoDamage (DamageRate);
				//destroy the projectile when it hit any collider

			} else if (hit.collider.gameObject.layer == 0) {
				//destroy the projectile when it hit any collider
				print ("hit defualt object");
			}

			// Play explosion on both server and client
			PlayExplosion (hit.point);
			//DetachParticles ();
			Destroy (this.gameObject);
		}
	}

	void PlayExplosion (Vector3 hitPosition)
	{
		GameObject explosion = Instantiate(Resources.Load(explosionTitle), hitPosition, Quaternion.identity) as GameObject;
		//NetworkServer.Spawn (explosion);
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
