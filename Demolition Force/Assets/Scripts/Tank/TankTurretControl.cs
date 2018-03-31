//SAMUEL D SILVERMAN UPDATE OCT 5TH 2016
//THIS SCRIPTS OBEJCTIVE IS TO TAKE IN PLAYER MOUSE MOVEMENT INPUT AND DESIGNATE DATA TO
// TURRET BASE FOR Y-AXIS ROTATION, AND TURRET MAIN FOR Y AXIS ROTATION

using UnityEngine;
using System.Collections;

public class TankTurretControl : MonoBehaviour {

	
	[Range(0.0f, 13.0f)]
	public float turretRotSpeed = 10f;

	private float rayDistance = 100f;
	public Transform turretProtector;
	public Transform turretMain;
	//private Transform targetCameraRot;

	private Camera mainCamera;

	//Update turret main rotation variable
	[HideInInspector]
	public static GameObject objectBeingLookedAt;

	void Start () {
        mainCamera = Camera.main;
//        mainCamera = transform.FindChild("Camera").GetComponent<Camera>();
	}
	
	void Update () {
		//UPDATES TURRET PROTECTOR ROTATION ON Y AXIS
		if (turretMain != null && turretProtector != null) {
			UpdateTurretProtectorRotation ();
			UpdateTurretMainRotation ();
		}
	}

	public void SetUpTurret(Transform t_protector, Transform t_main)
	{
		turretProtector = t_protector;
		turretMain = t_main;
	}

	void UpdateTurretProtectorRotation(){


		Ray ray = new Ray (mainCamera.transform.position, mainCamera.transform.forward); 
		//end point of ray
		Vector3 endOfRayPoint = ray.origin + (ray.direction * rayDistance);

		Vector3 lookDirection = mainCamera.transform.forward;
		//draws ray from camera
		//Debug.DrawRay (mainCamera.transform.position,lookDirection *rayDistance);

		RaycastHit hit;

		if (Physics.Raycast (mainCamera.transform.position, lookDirection * rayDistance, out hit)) {
			objectBeingLookedAt = hit.collider.gameObject;


			Vector3 dir = hit.point - turretMain.transform.position;
			Quaternion rotation = Quaternion.LookRotation (dir);
			turretProtector.transform.rotation  = Quaternion.Lerp (turretProtector.transform.rotation, rotation, Time.deltaTime * turretRotSpeed);

		} 
		else 
		{

			Vector3 dir = endOfRayPoint - turretMain.transform.position;
			Quaternion rotation = Quaternion.LookRotation (dir);
			turretProtector.transform.rotation  = Quaternion.Lerp (turretProtector.transform.rotation, rotation, Time.deltaTime * turretRotSpeed);
		}


		//resets local rotation x & z to 0 so the turret stays planted on tank
		turretProtector.localRotation = Quaternion.Euler(new Vector3(0,turretProtector.transform.localEulerAngles.y,0));

	}
	void UpdateTurretMainRotation(){
		//target if target is ky

		//creates ray from camera
		Ray ray = new Ray (mainCamera.transform.position, mainCamera.transform.forward); 
		//end point of ray
		Vector3 endOfRayPoint = ray.origin + (ray.direction * rayDistance);

		Vector3 lookDirection = mainCamera.transform.forward;
		//draws ray from camera
		Debug.DrawRay (mainCamera.transform.position,lookDirection *rayDistance);

		RaycastHit hit;

		if (Physics.Raycast (mainCamera.transform.position, lookDirection * rayDistance, out hit)) {
			objectBeingLookedAt = hit.collider.gameObject;


			//turretMain.transform.LookAt (hit.point);

			Vector3 dir = hit.point - turretMain.transform.position;
			Quaternion rotation = Quaternion.LookRotation (dir);
			turretMain.transform.rotation  = Quaternion.Lerp (turretMain.transform.rotation, rotation, Time.deltaTime * turretRotSpeed);

			Debug.DrawRay (turretMain.transform.position, turretMain.transform.forward * 50f, Color.red);
		} 
		else 
		{
			//turretMain.transform.LookAt (endOfRayPoint);

			Vector3 dir = endOfRayPoint - turretMain.transform.position;
			Quaternion rotation = Quaternion.LookRotation (dir);
			turretMain.transform.rotation  = Quaternion.Lerp (turretMain.transform.rotation, rotation, Time.deltaTime * turretRotSpeed);
		}

		//keeps turret peice glued to vehicle
		turretMain.transform.localRotation = Quaternion.Euler(new Vector3(turretMain.transform.localEulerAngles.x,0,0));
	}


}
