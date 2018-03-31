using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleRollControl : MonoBehaviour {

	[SerializeField]private float rollForce = 50f;
	TankMotorNetworking tankMotorNet;
	TankMotor tankMotor;
	private bool networked;

	void Start () {
		//checks to see if this is a networked vehicle player, else it grabs the right motor script
		if (GetComponent<TankMotorNetworking> () != null)
		{
			tankMotorNet = GetComponent<TankMotorNetworking> ();
			networked = true;
		}
		if (GetComponent<TankMotor> () != null)
		{
			tankMotor = GetComponent<TankMotor> ();
			networked = false;
		}
	}

	//Rolls Vehicle , called from tank controller
	public void RollVehicle(bool left, bool right) //l left r right f forward b back
	{
		if (networked) 
		{
			foreach (AxleInfoNetworking axleWheels in tankMotorNet.axleInfo) {
				if (!axleWheels.leftWheel.isGrounded && !axleWheels.rightWheel.isGrounded) {
					int roll = 0;
					if(left){
						roll = 1;
					}else if(right)
					{
						roll = -1;
					}
					GetComponent<Rigidbody> ().AddTorque (transform.forward * rollForce * Time.deltaTime * roll, ForceMode.Acceleration);
					//GetComponent<Rigidbody> ().AddTorque (transform.forward * rollForce * Time.deltaTime * l,ForceMode.Acceleration);
					//GetComponent<Rigidbody> ().AddTorque(transform.right * rollForce * Time.deltaTime * f,ForceMode.Acceleration);
					//GetComponent<Rigidbody> ().AddTorque (transform.right* -rollForce * Time.deltaTime * b,ForceMode.Acceleration);
				}
			}
		} else if (!networked) 
		{
			foreach (AxleInfo axleWheels in tankMotor.axleInfo) {
				if (!axleWheels.leftWheel.isGrounded && !axleWheels.rightWheel.isGrounded) {
					int roll = 0;
					if(left){
						roll = 1;
					}else if(right)
					{
						roll = -1;
					}
					GetComponent<Rigidbody> ().AddTorque (transform.forward * rollForce * Time.deltaTime * roll, ForceMode.Acceleration);
					//GetComponent<Rigidbody> ().AddTorque (transform.forward * rollForce * Time.deltaTime * l,ForceMode.Acceleration);
					//GetComponent<Rigidbody> ().AddTorque(transform.right * rollForce * Time.deltaTime * f,ForceMode.Acceleration);
					//GetComponent<Rigidbody> ().AddTorque (transform.right* -rollForce * Time.deltaTime * b,ForceMode.Acceleration);
				}
			}
		}

	}
}
