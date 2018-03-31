using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BarrelExplodesNetworking : NetworkBehaviour {

	[SerializeField]private string explosionTitle;
	[SerializeField]private float damageRange = 10f;
	[SerializeField]private float damagePerMeter = 15f;

	//call from the projectile on the server
	public void BarrelExplosion_Networking()
	{
		print("barrel explodes");
		//deal damage
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, damageRange);
		foreach (Collider col in hitColliders) {
			if (col.gameObject != this.gameObject) {// if the collider is not this barrel collider
				if (col.gameObject.tag == "Player") {
					print ("barrel blasts player");
					float distanceToTank;
					distanceToTank = (col.transform.position - this.transform.position).magnitude;
					print (distanceToTank);
					if (distanceToTank <= damageRange) {
						float damageRate = (damageRange - distanceToTank) * damagePerMeter;
						col.gameObject.GetComponent<TankHealthNetworking> ().TakeDamage (Mathf.Round (damageRate));	// round so that it hide the tail numbers
						// show the HP UI
					}
				} else if (col.gameObject.tag == "Barrel") {
					col.gameObject.GetComponent<BarrelExplodesNetworking> ().BarrelExplosion_Networking ();
					print ("barrel blasts other barrel");
				}
			}
		}
		//instatiate the explosion on all the client
		if (!string.IsNullOrEmpty (explosionTitle))
			ExplosionEffects ();
		NetworkServer.Destroy (this.gameObject);
	}
		
	void ExplosionEffects()
	{
		print ("make explosion");
		Instantiate (Resources.Load (explosionTitle), transform.position, Quaternion.identity);
	}
}
