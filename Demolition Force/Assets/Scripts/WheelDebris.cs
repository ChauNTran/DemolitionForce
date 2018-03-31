using UnityEngine;
using System.Collections;

public class WheelDebris : MonoBehaviour {

	public ParticleSystem wheelDustPS;
	private ParticleSystem.EmissionModule wheelDustEM;
	public float currentSpeed;

	TankMotor tankMotor;

	// Use this for initialization
	void Start () {
		wheelDustEM = wheelDustPS.emission;

		tankMotor = this.GetComponentInParent<TankMotor>();

	}

	// Update is called once per frame
	void Update () {

		currentSpeed = tankMotor.currentSpeed;

		if (currentSpeed > 0) {
			wheelDustEM.rate = new ParticleSystem.MinMaxCurve (currentSpeed / 2);
		}
	}
}
