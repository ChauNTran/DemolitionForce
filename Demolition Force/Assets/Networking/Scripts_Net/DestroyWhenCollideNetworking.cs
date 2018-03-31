// chau tran
// where to put: 	put in the cannon balls (bullet)
// purpose : 		Do stuff when the bullet hits something

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DestroyWhenCollideNetworking : NetworkBehaviour {

	public enum ProjectileType
	{
		turretProjectile, gatlingProjectile, cannonBall
	}
	public ProjectileType projectileType;
	public string explosionTitle;
	private float DamageRate = 10f;
	private bool ignoreShield = false;	// ignore shield when the bullet shoot FROM THE PLAYER
	//public float projectileSpeed = 100;

	//Referenced to send bullet info from shot player to bullet owner to determine score:
	public PlayerMultiplayer projectileOwner;

	void Start()
	{
		DestroyObject (gameObject, 10f);
	}


	public void SetDamageRate(float newDamageRate)
	{
		this.DamageRate = newDamageRate;
	}
	public void SetIgnoreShieldTrue()
	{
		this.ignoreShield = true;
	}


	// only happen on the server
	void OnTriggerEnter(Collider other)
	{
		if (isServer) {
			switch (projectileType) {
			case ProjectileType.turretProjectile:
				gameObject.transform.Find ("vfx_smokeTrail").gameObject.GetComponent<StopEmission> ().stopEmission ();
				transform.GetChild (0).parent = null;
				break;
			case ProjectileType.gatlingProjectile:

				break;
			case ProjectileType.cannonBall:
				SetDamageRate (50f);
				break;
			}

			if (other.gameObject.tag == "Destroyable") {  // crate , brick and anything that take damage from bullet
				other.gameObject.GetComponent<DestroyableSettings> ().DoDamage (DamageRate);
				other.gameObject.GetComponent<DestroyableSettings> ().hit = true;

			} else if (other.gameObject.tag == "Barrel") {
				other.gameObject.GetComponent<BarrelExplodesNetworking> ().BarrelExplosion_Networking ();
				Destroy (other.gameObject);
			} else if (other.gameObject.tag == "Player") {
				TankHealthNetworking enemy = other.transform.GetComponent<TankHealthNetworking> ();
				//other.gameObject.GetComponentInParent<TankHealthNetworking> ().CmdTankDamage (DamageRate); // damage will be change to something else
				other.gameObject.GetComponentInParent<TankHealthNetworking> ().TakeDamage (DamageRate); //Different damage call, may be better
				//See if enemy dies, if so increment score.
				if (enemy.isDead)
				{
					projectileOwner.AddKillCount ();
				}
			} else if (other.gameObject.tag == "Shield") {
				if (ignoreShield)
					return;
			}
		}
		// make explosion
		if (explosionTitle != "") {
			PlayExplosion ();
			Destroy (this.gameObject);
		}
	}

	//*** causes warning
	// play the explosion on all client
	void PlayExplosion()
	{
		Instantiate(Resources.Load(explosionTitle), this.transform.position, Quaternion.identity);
	}
}
