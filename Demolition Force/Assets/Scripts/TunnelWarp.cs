//Attach script to tunnel prefab
//This script allows for the player to go into a tunnel and be transported to another

using UnityEngine;
using System.Collections;

public class TunnelWarp : MonoBehaviour {

	public Transform linkPoint;
	TunnelCollider tunnelCol;
	TankCameraControl cameraControl;

	void Start () {
		tunnelCol = gameObject.transform.GetChild (0).GetComponent<TunnelCollider> ();
		tunnelCol.warpEvent += WarpPlayer;
		
	}
	
	void Update () {
		
	}
	void WarpPlayer(){
		//tunnelCol.warpEvent -= WarpPlayer;
		tunnelCol.warpedPlayer.transform.position = linkPoint.position;
		tunnelCol.warpedPlayer.transform.rotation = linkPoint.rotation;
		tunnelCol.warpedPlayer.GetComponent<Rigidbody> ().velocity = linkPoint.transform.forward * tunnelCol.warpedPlayer.GetComponent<Rigidbody> ().velocity.magnitude;
		tunnelCol.warpedPlayer.GetComponent<TankCameraControlNetworking> ().ResetCam ();// *** FIX THIS

		//print(tunnelCol.warpedPlayer.GetComponent<Rigidbody> ().velocity);
		//tunnelCol.warpedPlayer.GetComponent<Rigidbody>().AddForce(linkPoint.transform.forward * tunnelCol.warpedPlayer.GetComponent<Rigidbody>().velocity.magnitude * 100f);

		//print (tunnelCol.warpedPlayer.transform.eulerAngles);
		//Vector3 spawnRotation = new Vector3(linkPoint.transform.eulerAngles.x,linkPoint.transform.eulerAngles.y,linkPoint.transform.eulerAngles.z);
		//print (spawnRotation);
		//tunnelCol.warpedPlayer.transform.eulerAngles = spawnRotation;
	}

}
