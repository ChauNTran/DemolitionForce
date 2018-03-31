// chau tran
// where to put: 	put in the cannon balls (bullet)
// purpose : 		Do stuff when the bullet hits something

using UnityEngine;
using System.Collections;

public class DestroyWhenCollide : MonoBehaviour {

	public enum ProjectileType
	{
		turretProjectile, gatlingProjectile
	}
	public ProjectileType projectileType;
	public string explosionTitle;
	private float DamageRate = 10f;
	private bool ignoreShield = false;	// ignore shield when the bullet shoot FROM THE PLAYER

	//public float projectileSpeed = 100;

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

	void OnTriggerEnter(Collider other)
	{
		switch(projectileType)
		{
		case ProjectileType.turretProjectile:
			transform.GetChild(0).gameObject.GetComponent<StopEmission> ().stopEmission ();
			transform.GetChild(0).parent = null;
			break;
		case ProjectileType.gatlingProjectile:

			break;
		}

		if (other.gameObject.tag == "Destroyable") {  // crate , brick and anything that take damage from bullet
			other.gameObject.GetComponent<DestroyableSettings> ().DoDamage(DamageRate);
			other.gameObject.GetComponent<DestroyableSettings> ().hit = true;

		} else if (other.gameObject.tag == "Barrel") {
			other.gameObject.GetComponent<BarrelExplodes> ().BarrelExplosion ();
			Destroy (other.gameObject);
		} else if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponentInParent<TankHealth> ().TankDamage (DamageRate); // damage will be change to something else
		} else if (other.gameObject.tag == "Tank") {

			other.gameObject.GetComponentInParent<TankHealth> ().TankDamage (DamageRate);

			float e_health = other.gameObject.GetComponent<TankHealth> ().getTankHealth();
			float e_max_health = other.gameObject.GetComponent<TankHealth> ().tankFullHealth;
			if(e_health > 0)
			GameObject.Find ("Game UI").GetComponent<HealthUI> ().EnemyHealthBarUpdate(e_health,e_max_health);
		}
		else if (other.gameObject.tag == "Shield") {
			if (ignoreShield)
				return;
		}
		// make explosion
		if(explosionTitle != "")
		Instantiate(Resources.Load(explosionTitle), this.transform.position, Quaternion.identity);

		Destroy (this.gameObject);
	}


}
