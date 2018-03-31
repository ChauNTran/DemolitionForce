using UnityEngine;
using System.Collections;

public class TestTurretMain : MonoBehaviour {

	Camera mainCam;
	[HideInInspector]
	public static GameObject objectBeingLookedAt;
	void Start () {
		mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 lookDirection = mainCam.transform.forward;
		Debug.DrawRay (mainCam.transform.position,lookDirection *100f);
		RaycastHit hit;
		Physics.Raycast (mainCam.transform.position,lookDirection * 100f,out hit);
		objectBeingLookedAt = hit.collider.gameObject;
		transform.LookAt (hit.point);
		transform.localRotation = Quaternion.Euler(new Vector3(transform.localEulerAngles.x,0,0));
	}

}
