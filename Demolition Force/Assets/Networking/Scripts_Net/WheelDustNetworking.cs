using UnityEngine;
using System.Collections;

public class WheelDustNetworking : MonoBehaviour {

	public ParticleSystem wheelDustPS;
	private ParticleSystem.EmissionModule wheelDustEM;
	public float currentSpeed;

	TankMotorNetworking tankMotor;
	AITankControl aiMotor;
	string checkObj;

	// Use this for initialization
	void Start () {
		wheelDustPS = GetComponent<ParticleSystem> ();
		wheelDustEM = wheelDustPS.emission;

		//since this script is on enemies as well it must acces not only the player tank but also the enmey tank
		checkObj = gameObject.transform.parent.parent.gameObject.tag;

		if (checkObj == "Player") {
			tankMotor = gameObject.transform.parent.parent.gameObject.GetComponent<TankMotorNetworking> ();
		}else if(checkObj == "Tank"){
			aiMotor = gameObject.transform.parent.parent.gameObject.GetComponent<AITankControl> ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (checkObj == "Player")
		currentSpeed = tankMotor.currentSpeed;
		else if(checkObj == "Tank")
			currentSpeed = aiMotor.currentSpeed;
		

		if (currentSpeed > 25) {
			wheelDustEM.rate = new ParticleSystem.MinMaxCurve (currentSpeed / 4);
		} else
			wheelDustEM.rate = 0;
	}
}
