using UnityEngine;
using System.Collections;

public class CameraController2 : MonoBehaviour {

	public Transform target; //camera rotates around this
	public float cameraAngle;
	//public float cameraHeight;
	public float cameraDistance;
	public float xSensitivity = 2f;
	public float ySensitivity = 2f;

	private Vector3 actualizedPos;

	void LateUpdate(){

		//Debug.DrawRay (transform.position, transform.forward, 100f, Color.green, 0f, false);
		Debug.DrawRay (transform.position, transform.forward * 100f, Color.green);

		if(target != null)
		{
			actualizedPos = target.transform.position; //Add camera angle to target so camera looks ahead of tank
			actualizedPos.y += cameraAngle;

			transform.LookAt(actualizedPos);
			//NOTE REMBER TO CLAMP THESE VALUES SOMEHOW
			transform.RotateAround(actualizedPos,Vector3.up, Input.GetAxis ("Mouse X") * xSensitivity);
			transform.RotateAround(actualizedPos,Vector3.right, Input.GetAxis ("Mouse Y") * ySensitivity);
		}
	}

}
