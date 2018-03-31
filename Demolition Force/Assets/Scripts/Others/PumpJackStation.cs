using UnityEngine;
using System.Collections;

public class PumpJackStation : MonoBehaviour {
	
	//the distance player needs to be near pump to activate
	public float pumpStationRadius;

	Animator oilPumpJackAnim;

	void Start(){
		GetComponent<SphereCollider> ().radius = pumpStationRadius;
		oilPumpJackAnim = GetComponent<Animator> ();
	}

	//UPDATE IF PLAYERS HEALTH OR OIL RESERVE IS FULL - STOP PUMPING

	//IF PLAYER ENTERS OIL PUMP RADIUS
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			oilPumpJackAnim.SetBool ("isPumping", true);
		}
	}
	//IF PLAYER LEAVES OIL PUMP RADIUS
	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			oilPumpJackAnim.SetBool ("isPumping", false);
		}
	}
}
