using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float XSensitivity = 2f;
	public float YSensitivity = 2f;

	public Transform turretProtector;
	public Transform turretMain;

	private Quaternion t_protector;
	private Quaternion t_main;
	private Camera mainCam;
	void Start ()
	{
		t_protector = turretProtector.localRotation;
		t_main = turretMain.localRotation;
		mainCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}

	void FixedUpdate ()
	{
		LookRotation ();
		ShootRay ();
	}
	void LateUpdate()
	{
		Vector3 lookatPosition = turretMain.up * 100f;
		mainCam.transform.LookAt (lookatPosition);
	}
	public void LookRotation()
	{
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis ("Mouse Y") * YSensitivity;

		t_protector *= Quaternion.Euler (0f, yRot, 0f);
		t_main *= Quaternion.Euler (-xRot, 0f, 0f);

		turretProtector.localRotation = t_protector;
		turretMain.localRotation = t_main;
	}
	void ShootRay()
	{
		// shoot a Ray from the turret
		Vector3 t_direction = turretMain.up;
		Debug.DrawRay (turretMain.position, t_direction * 100f, Color.green, 0f, false);

		// make the camera look at where the turret is pointing at

	}
}
