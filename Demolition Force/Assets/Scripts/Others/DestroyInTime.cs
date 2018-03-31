// chau tran
// where to put: 	in the pieces prefabs
// purpose : 		make the pieces disappear after time

using UnityEngine;
using System.Collections;

public class DestroyInTime : MonoBehaviour {

	public float damageRange = 10f;
	public float damagePerMeter = 15f;
	float timer = 0f;
	public bool doDamage = false;
	bool doitonce = false;
	void Update () 
	{
		timer += Time.deltaTime;
		if(timer >= 2.2f)
		{
			Destroy (this.gameObject);
		}
		if (doDamage && doitonce != true)
			ExplosionDamage ();
	}
	void ExplosionDamage()
	{
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, damageRange);
		foreach (Collider col in hitColliders){
			if (col.gameObject.tag == "Tank") {

				float distanceToTank;
				distanceToTank = (col.transform.position - this.transform.position).magnitude;
				print (distanceToTank);
				if(distanceToTank <= damageRange){
				float damageRate = (damageRange - distanceToTank) * damagePerMeter;
				col.gameObject.GetComponent<TankHealth> ().TankDamage(Mathf.Round(damageRate));	// round so that it hide the tail numbers
				// show the HP UI
				float e_health = col.gameObject.GetComponent<TankHealth> ().getTankHealth();
				float e_max_health = col.gameObject.GetComponent<TankHealth> ().tankFullHealth;
				if(e_health > 0)
					GameObject.Find ("Game UI").GetComponent<HealthUI> ().EnemyHealthBarUpdate(e_health,e_max_health);
				}
			}
			else if(col.gameObject.tag == "Barrel")
			{
				col.gameObject.GetComponent<BarrelExplodes> ().BarrelExplosion ();
				Destroy (col.gameObject);
			}
		}
		doitonce = true;
	}
}
