//When player tank runs into tar pit they are slowed down.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudTarScript : MonoBehaviour {

	public enum EnvironmentType 
	{
		mud,
		tar
	}
		
	public EnvironmentType environmentType;
	float slowDragAmount;

	void Start(){
		if (environmentType == EnvironmentType.mud) {
			slowDragAmount = 1f;
		} else if (environmentType == EnvironmentType.tar) {
			slowDragAmount = 2.75f;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.transform.root.tag == "Player") 
		{
			{
				TarSlowEnter (other);
				MudSlowEnter (other);
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.transform.root.tag == "Player") 
		{
			TarSlowExit (other);	
			MudSlowExit (other);
		}
	}
	//FUNCTION CALLED WHEN MUD IS COLLIDED WITH
	void MudSlowEnter(Collider other){
		if (environmentType == EnvironmentType.mud) {
			other.gameObject.transform.root.GetComponent<TankMotorNetworking> ().isStuckMud = true;
			other.gameObject.transform.root.GetComponent<TankMotorNetworking> ().SetSlip (slowDragAmount, slowDragAmount);
		}
	}
	void MudSlowExit(Collider other){
		if (environmentType == EnvironmentType.mud) 
		{
			other.gameObject.transform.root.GetComponent<TankMotorNetworking> ().isStuckMud = false;
			other.gameObject.transform.root.GetComponent<TankMotorNetworking> ().SetSlip (other.gameObject.transform.root.GetComponent<TankMotorNetworking> ().forwardFriction, other.gameObject.transform.root.GetComponent<TankMotorNetworking> ().sidewayFriction);
		}
	}

	//FUNCTION CALLED WHEN TAR IS COLLIDED WITH
	void TarSlowEnter(Collider other){
		if (environmentType == EnvironmentType.tar) {
			other.gameObject.transform.root.GetComponent<TankMotorNetworking> ().isStuckTar = true;
			other.gameObject.transform.root.GetComponent<Rigidbody> ().drag = slowDragAmount;
		}
	}
	void TarSlowExit(Collider other){
		if (environmentType == EnvironmentType.tar) 
		{
			other.gameObject.transform.root.GetComponent<TankMotorNetworking> ().isStuckTar = false;
			other.gameObject.transform.root.GetComponent<Rigidbody> ().drag = 2f;
		}
	}
}

