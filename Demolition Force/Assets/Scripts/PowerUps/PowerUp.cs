// sam and chau
// where to put:	

using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	//rotation variables
	public float rotateSpeed = 10f;
	Vector3 rotation;
	//Power up type
	public enum PowerUpType {health,speed,shield,hpCapacityBoost,boostDamage,boostDamage2}
	public Color powerUpColor;
	public PowerUpType powerUpType;

	public float shieldTime = 10f;
	public float damageTime = 10f;

	PowerUI powerUpUI;
	void Start () {
		rotation = new Vector3 (0f, 15f, 0f);
		GetComponent<Renderer> ().material.color = powerUpColor;
		powerUpUI = GameObject.Find ("Game UI").GetComponent<PowerUI> ();
	}
	
	void Update () {
		RotatePowerUp ();
	}

	void RotatePowerUp(){
		transform.Rotate (rotation, Time.deltaTime * rotateSpeed);
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") 
		{
			GameObject ps;
			switch (powerUpType) 
			{
			case PowerUpType.health:
				//INCREASE HEALTH
				other.GetComponent<TankHealth> ().FullHeath ();
				ps = Instantiate (Resources.Load ("Health_effect_cylinder"), transform.position, Quaternion.identity) as GameObject;
				ps.transform.SetParent (GameObject.FindGameObjectWithTag ("Player").transform);
				ps.transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position + new Vector3 (0, 2f, 0);
				break;
			case PowerUpType.speed:
				//Increase Speed
				other.GetComponent<TankMotor>().SpeedUp(4000f);
				break;
			case PowerUpType.shield:
				//Player activate shield
				other.GetComponent<TankHealth> ().ShieldActivate ();
				powerUpUI.ShieldUIActivate (shieldTime);
				ps = Instantiate (Resources.Load ("Shield_effect_cylinder"), transform.position + new Vector3(0,3f,0), Quaternion.identity) as GameObject;
				ps.transform.SetParent (GameObject.FindGameObjectWithTag ("Player").transform);
				ps.transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position + new Vector3 (0, 3f, 0);
				break;
			case PowerUpType.boostDamage:
				//Increase Attack
				other.GetComponent<PlayerTankShoot> ().AddDamageRate (10F);	// add 10 to damage rate
				other.GetComponent<PlayerTankShoot> ().AddDamageRate_Secondary(4f);
				powerUpUI.DamageUIActivate (damageTime);
				break;
			case PowerUpType.boostDamage2:
				//Increase Attack

				break;
			case PowerUpType.hpCapacityBoost:
				// Add more capcity to the Player Health
				other.GetComponent<TankHealth>().AddHealthCapacity(20f);
				break;
			}

			Destroy (gameObject);
		}
	
	}
}
