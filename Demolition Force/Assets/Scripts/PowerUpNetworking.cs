using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerUpNetworking : NetworkBehaviour {

	//rotation variables
	public float rotateSpeed = 10f;
	Vector3 rotation;
	//Power up type
	public enum PowerUpType {health,speed,shield,hpCapacityBoost,boostDamage,boostDamage2}
	public Color powerUpColor;
	public PowerUpType powerUpType;

	public float shieldTime = 10f;
	public float damageTime = 10f;

//	PowerUI powerUpUI;
	void Start () {
		rotation = new Vector3 (0f, 15f, 0f);
		GetComponent<Renderer> ().material.color = powerUpColor;
//		powerUpUI = GameObject.Find ("Game UI").GetComponent<PowerUI> ();
	}

	void Update () {
		RotatePowerUp ();
	}

	void RotatePowerUp(){
		transform.Rotate (rotation, Time.deltaTime * rotateSpeed);
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Player") 
		{
			if (isServer) {
//			GameObject ps;
				switch (powerUpType) {
				case PowerUpType.health:
				//INCREASE HEALTH
				other.GetComponent<TankHealthNetworking> ().CmdFullHeath ();
//				ps = Instantiate (Resources.Load ("Health_effect_cylinder"), transform.position, Quaternion.identity) as GameObject;
//				ps.transform.SetParent (GameObject.FindGameObjectWithTag ("Player").transform);
//				ps.transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position + new Vector3 (0, 2f, 0);
					break;
				case PowerUpType.speed:
				//Increase Speed
					other.GetComponent<TankMotor> ().SpeedUp (4000f);
					break;
				case PowerUpType.shield:
				//Player activate shield
					other.GetComponent<TankHealthNetworking> ().ShieldActivate (shieldTime);
//				powerUpUI.ShieldUIActivate (shieldTime);
//				ps = Instantiate (Resources.Load ("Shield_effect_cylinder"), transform.position + new Vector3(0,3f,0), Quaternion.identity) as GameObject;
//				ps.transform.SetParent (GameObject.FindGameObjectWithTag ("Player").transform);
//				ps.transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position + new Vector3 (0, 3f, 0);
					break;
				case PowerUpType.boostDamage:
				//Increase Attack
					other.GetComponent<TankShootNetworking> ().RpcAddDamageRate (10F,4f,damageTime);	// add 10 to damage rate
//				powerUpUI.DamageUIActivate (damageTime);
					break;
				case PowerUpType.boostDamage2:
				//Increase Attack

					break;
				case PowerUpType.hpCapacityBoost:
				// Add more capcity to the Player Health
					other.GetComponent<TankHealthNetworking> ().AddHealthCapacity (20f);
					break;
				}
			}

			Destroy (gameObject);
		}

	}
}
