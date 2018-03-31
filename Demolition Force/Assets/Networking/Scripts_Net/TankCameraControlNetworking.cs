//SAMUEL D SILVERMAN

//THIS SCRIPT CONTROLS THE CAMERA CONTROLS FOR THE PLAYER VEHICLE. THIS SCRIPT ALLOWS FOR THE PLAYER TO 
//MOVE THE MOUSE TO CONTROL ORBIT OF THE CAMERA AND CROSSHAIR.

//THIS SCRIPT IS IS CONNECTED TO TANK CONTROLLER SO INPUT MOVEMENT IS INTERNALIZED FORM INPUT.

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TankCameraControlNetworking : NetworkBehaviour {

	//Max and Min Y-Rotational angle of camera/crosshair/main turret.
	//HOW HIGH
	private const float Y_ANGLE_MIN = -10.0f;
	//HOW LOW
	private const float Y_ANGLE_MAX = 25.0f;

	//The target axis of which the camera rotates around
	public Transform target;

	[HideInInspector]
	public Transform camTransform;

	//CAMERA POSTION VARIABLES
	[Range(5.0f, 12.0f)]
	public float distance = 10.0f;
	public float crosshairHeight;
	[Range(1.0f, 10.0f)]
	public float sensitivityX = 4.0f;
	[Range(1.0f, 10.0f)]
	public float sensitivityY = 1.4f;
	public bool xInverted = false;
	public bool yInverted = false;
	[HideInInspector]
	public bool resetCamera = false;

	private Vector3 actualizedPos;
	private float currentX = 0.0f;
	private float currentY = 0.0f;
	private float currentXdegrees;
	private float remainderDegrees;
	private float degreeGoal;
	private Camera cam;

	void Start () {
        cam = transform.Find("Camera").GetComponent<Camera>();
		camTransform = cam.transform;
		ResetCam ();
	}

	void Update()
	{
		CameraRevert ();
//		RestrictedCurrentX ();
	}
	void CameraRevert(){
		//update the current degrees

		if (resetCamera != false)
		{

			currentX = Mathf.Lerp (currentX,this.transform.rotation.eulerAngles.y, 4f * Time.deltaTime);
			if(Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.3 || Mathf.Abs(Input.GetAxis("Mouse X")) > 0.3)
				resetCamera = false;

			if (Mathf.Abs(currentX - this.transform.rotation.eulerAngles.y) < 1f)
			{
				currentX = this.transform.rotation.eulerAngles.y;
				resetCamera = false;
			}
		}
	}

	void RestrictedCurrentX()
	{
		if (currentX > 180)
			currentX = currentX - 360;
		else if (currentX < -180)
			currentX = currentX + 360;
	}

	void LateUpdate(){

		Vector3 dir = new Vector3 (0, 0, -distance);
		Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);
		camTransform.position = target.position + rotation * dir;


		//ADD Camera Height
		Vector3 actualizedPos = target.transform.position;
		actualizedPos.y = target.transform.position.y + crosshairHeight;
		camTransform.LookAt (actualizedPos);//look at taget position + camera height

	}
	//THIS FUNCTION IS CALL WITH TANKCONTROLLER SCRIPT TO UPDATE CAMERA POSITIONING WITH INPUT
	public void UpdateCameraMovement(float cameraX, float cameraY){

		//whether inverted is pressed or not
		if (PlayerPrefsManager.GetInvertedX()) { 
			currentX += cameraX * sensitivityX * -1;
		} else {
			currentX += cameraX * sensitivityX;
		}
		if (PlayerPrefsManager.GetInvertedY()) {
			currentY += cameraY * sensitivityY * -1;
		} else {
			currentY += cameraY * sensitivityY;
		}
		//clamp camera y axis rotation
		currentY = Mathf.Clamp (currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
	}
	public void ResetCam()
	{
		resetCamera = true;
	}
	public void ZoomInOut(float zoomRate)
	{
		distance += zoomRate * 2f;
		distance = Mathf.Clamp (distance, 5f, 12f);
	}
}
