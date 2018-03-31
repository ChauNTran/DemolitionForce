using System;
using UnityEngine;
using System.Collections;

public class TunnelCollider : MonoBehaviour {

	[HideInInspector]
	public bool isWarped;
	[HideInInspector]
	public GameObject warpedPlayer;
	//public delegate void WarpDelegate();
	//public WarpDelegate warpEvent;

	public event Action warpEvent;

	void Start(){
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {
			
			if (warpEvent != null) {
				//print ("hit");
				warpedPlayer = other.gameObject;
				//print ("yup");
				warpEvent ();
			}
		}
	}
}
