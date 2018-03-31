using UnityEngine;
using System.Collections;

public class CameraVehicle : MonoBehaviour {

	public Transform car;
	public float distance = 6.4f;
	public float height = 1.4f;
	public float rotationDamping = 3.0f;
	public float heightDamping = 2.0f;
	public float zoomRatio = 0.5f;
	private Vector3 rotationVector;

	public Camera mainCamera;
	float defaultFOV;
	//Rigidbody rigidbody;

	void Start () {
		
		defaultFOV = mainCamera.fieldOfView;
		//rigidbody = car.GetComponent<Rigidbody> ();
	}

	void LateUpdate () {
		var wantedAngle = rotationVector.y;
		var wantedHeight = car.position.y + height;
		var myAngle = transform.eulerAngles.y;
		var myHeight = transform.position.y;
		myAngle = Mathf.LerpAngle (myAngle, wantedAngle, rotationDamping * Time.deltaTime);
		myHeight = Mathf.Lerp (myHeight, wantedHeight, heightDamping * Time.deltaTime);
		var currentRotation = Quaternion.Euler (0, myAngle, 0);
		transform.position = car.position; //normalize Position
		transform.position -= currentRotation*Vector3.forward*distance;

		var myHeightVector = new Vector3 (transform.position.x, myHeight, transform.position.z);
		transform.position = myHeightVector;
		transform.LookAt (car);
	}
	void FixedUpdate(){
		var localVelocity = car.InverseTransformDirection (car.GetComponent<Rigidbody> ().velocity);
		if (localVelocity.z < -0.5f) {//REVERSE CAMERA VIEW
			rotationVector.y = car.eulerAngles.y + 180;
		} else {
			rotationVector.y = car.eulerAngles.y;
		}
		var acc = car.GetComponent<Rigidbody>().velocity.magnitude;
		mainCamera.fieldOfView = defaultFOV + acc * zoomRatio;
	}
}
