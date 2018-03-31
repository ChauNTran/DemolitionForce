//Sam s. Notes
//This script uses players Class changing UI to access local player to send cmd change vehicle class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class VehicleClassChangeUI_Network : NetworkBehaviour {

//	GameObject localPlayer;



	public void ChangeClass(int classNum)
	{
		//get local player
		//call smd change class function
		if (FindLocalPlayer () != null) {
			FindLocalPlayer ().GetComponent<PlayerMultiplayer> ().CmdChangeVehicleClass (classNum, FindLocalPlayer ().GetComponent<NetworkIdentity> ().netId);
			gameObject.GetComponent<CanvasGroup> ().alpha = 0;
			gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			gameObject.GetComponent<CanvasGroup> ().interactable = false;

		}
	}

	//CREATE FIND LCOAL PLAYER FUNCTION
	public GameObject FindLocalPlayer()
	{
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) 
		{
			if (obj.GetComponent<NetworkIdentity> ().isLocalPlayer) 
			{
				return obj;
			}
		}
		return null;
	}

	public void SetActiveClassChangeUI(bool state)
	{
		//this.gameObject.SetActive (state);
		this.gameObject.transform.GetChild (0).gameObject.SetActive (state);
		this.gameObject.transform.GetChild (1).gameObject.SetActive (state);

	}
}
