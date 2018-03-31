
//SAMUEL D SILVERMAN UPDATE OCT 5TH 2016
//THIS SCRIPTS OBEJCTIVE IS TO TAKE IN PLAYER MOUSE MOVEMENT INPUT AND DESIGNATE DATA TO
// TURRET BASE FOR Y-AXIS ROTATION, AND TURRET MAIN FOR Y AXIS ROTATION

using UnityEngine;
using System.Collections;

public class TankTurretControlNetworking : MonoBehaviour {


	[Range(0.0f, 26.0f)]
	public float turretRotSpeed = 10f;

	private float rayDistance = 100f;
	public Transform turretProtector;
	public Transform turretMain;
	//private Transform targetCameraRot;

	private Transform targetForProtector;
	private Camera cam;
	GameObject hitObject;
	//Update turret main rotation variable
	[HideInInspector]
	public static GameObject objectBeingLookedAt;
	private GameObject visualObstacle;
	[Tooltip("creates an aim offset to rectify turrets aim")]
	public Vector3 turretRectOffset;//this variable helps the fast tank to aim from a center position to target
	void Start () {
		cam = transform.Find("Camera").GetComponent<Camera>();
//		cam = Camera.main;
		targetForProtector = cam.transform;
	}

	void Update()
	{
		CheckBlockItem ();
	}
	void CheckBlockItem()
	{
		Vector3 camToTurret = turretProtector.position - cam.transform.position;
		Ray ray = new Ray (cam.transform.position, camToTurret);
		Debug.DrawRay (cam.transform.position, camToTurret, Color.green);
		RaycastHit hit;


		if (hitObject != null)
			hitObject.layer = 0;
		
		if (Physics.Raycast (cam.transform.position, camToTurret, out hit)) {
			if(hit.collider.gameObject != this.gameObject && hit.collider.gameObject.tag != "Shield")// if this is not the tank itself
				hitObject = hit.collider.gameObject;
			if (hitObject!= null) 
				if(hitObject.tag == "Boundary")
				{
					hitObject.layer = 2;
				}
		}
	}

	void LateUpdate () {
		//UPDATES TURRET PROTECTOR ROTATION ON Y AXIS
		if (turretMain != null && turretProtector != null)
		{
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
		
		Ray ray = new Ray (cam.transform.position, cam.transform.forward); 
		//end point of ray
		Vector3 endOfRayPoint = ray.origin + (ray.direction * rayDistance);

		Vector3 lookDirection = cam.transform.forward;
		//draws ray from camera

		RaycastHit hit;

		if (Physics.Raycast (cam.transform.position, lookDirection * rayDistance, out hit)) {
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

		turretProtector.localRotation = Quaternion.Euler(new Vector3(0,turretProtector.transform.localEulerAngles.y,0));

	}
	void UpdateTurretMainRotation(){
		//target if target is ky

		//creates ray from camera
		Ray ray = new Ray (cam.transform.position, cam.transform.forward); 
		//end point of ray
		Vector3 endOfRayPoint = ray.origin + (ray.direction * rayDistance);

		Vector3 lookDirection = cam.transform.forward;
		//draws ray from camera
		Debug.DrawRay (cam.transform.position,lookDirection *rayDistance);

		RaycastHit hit;

		if (Physics.Raycast (cam.transform.position, lookDirection * rayDistance, out hit)) {
//			if(hit.collider.gameObject.tag != "Shield")

			objectBeingLookedAt = hit.collider.gameObject;
			Vector3 dir = hit.point - (turretMain.transform.position + turretRectOffset);
			Quaternion rotation = Quaternion.LookRotation (dir);
			turretMain.transform.rotation  = Quaternion.Lerp (turretMain.transform.rotation, rotation, Time.deltaTime * turretRotSpeed);

			Debug.DrawRay ((turretMain.transform.position + turretRectOffset) , turretMain.transform.forward * 50f, Color.red);
		} 
		else 
		{
			Vector3 dir = endOfRayPoint - (turretMain.transform.position + turretRectOffset);
			Quaternion rotation = Quaternion.LookRotation (dir);
			turretMain.transform.rotation  = Quaternion.Lerp (turretMain.transform.rotation, rotation, Time.deltaTime * turretRotSpeed);
		}

		//keeps turret peice glued to vehicle
		turretMain.transform.localRotation = Quaternion.Euler(new Vector3(turretMain.transform.localEulerAngles.x,0,0));
	}

	void CheckIfCameraIsBlocked()	// to see if there is anything in between the camera and the tank itself
	{
		Vector3 offsetTankPosition = this.transform.position + new Vector3 (0, 1, 0);
		Vector3 rayDirection = offsetTankPosition - cam.transform.position;
		Debug.DrawRay (cam.transform.position, rayDirection, Color.cyan);
		RaycastHit hit;
		if (Physics.Raycast (cam.transform.position, rayDirection, out hit))
		if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Shield") {
			visualObstacle = hit.collider.gameObject;
			visualObstacle.layer = 11;
		}
	}
}
