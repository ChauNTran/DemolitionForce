using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LookAtCameraNameUI : NetworkBehaviour {


	public Transform camera;

	void Start () {
		if (FindLocalPlayer () != null) {
			if (FindLocalPlayer ().gameObject.transform.GetChild (3).transform != null)
				camera = FindLocalPlayer ().gameObject.transform.GetChild (3).transform;
		}
	}
	[ClientCallback]
	void Update () {
		//if (FindLocalPlayer ().gameObject.transform.FindChild ("Camera") != null) 
		if (FindLocalPlayer () != null) 
		{
			camera = FindLocalPlayer ().gameObject.transform.Find ("Camera").gameObject.transform;
		}
		if (camera == null) {
			return;
		}

		transform.rotation = Quaternion.LookRotation (transform.position - camera.position);
	}
	public GameObject FindLocalPlayer()
	{
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) 
		{
			if (obj.GetComponent<NetworkIdentity> ().isLocalPlayer) 
			{
				if (obj != null) {
					return obj;
				}
			}
		}
		return null;
	}

}
