// chau tran
// where to put:	put in the player
// purpose:			get input from user

// NOTE: CHECK THE INPUT MANAGER TO SET THE INPUT NAMES

using UnityEngine;
using System.Collections;

[RequireComponent(typeof (TankMotor))]
[RequireComponent(typeof (TankCameraControl))]
[RequireComponent(typeof (PlayerTankShoot))]
[RequireComponent(typeof (TankHealth))]
[RequireComponent(typeof (TankTurretControl))]

public class TankController : MonoBehaviour {

	TankMotor tankMotor;
	TankCameraControl cameraControl;
	PlayerTankShoot tankShoot;
	VehicleRollControl vehicleRollControl;


	float timer = 0f;
	float timer2 = 0f;

	void Start () 
	{
		tankMotor = GetComponent<TankMotor> ();
		cameraControl = GetComponent<TankCameraControl> ();
		tankShoot = GetComponent<PlayerTankShoot> ();
		vehicleRollControl = GetComponent<VehicleRollControl> ();

	}
	void FixedUpdate () 
	{

		timer += Time.deltaTime;
		timer2 += Time.deltaTime;
		if (tankShoot.holdAndShoot)
		{
			if (Input.GetButton ("Fire1"))
			if (timer > tankShoot.GetFireRate_primary())
			{
				tankShoot.Shoot ();

				timer = 0f;
			}
		}
		else
			if (Input.GetButtonDown ("Fire1"))
			if (timer > tankShoot.GetFireRate_primary())
			{
				tankShoot.Shoot ();
				timer = 0f;
			}

		if (tankShoot.holdAndShoot)
		{
			if (Input.GetButtonDown ("Fire2"))
				tankShoot.PlaySecondaryGunSound ();

			else if (Input.GetButtonUp ("Fire2"))
				tankShoot.StopSecondaryGunSound ();

			if (Input.GetButton ("Fire2"))
			if (timer2 > tankShoot.GetFireRate_Secondary())
			{
				tankShoot.ShootSecondary ();
				timer2 = 0f;
			}
		}
		else
			if (Input.GetButtonDown ("Fire2"))
			if (timer2 > tankShoot.GetFireRate_Secondary())
			{
				tankShoot.ShootSecondary ();
				timer2 = 0f;
			}
		//Tank Movement
		float h = Input.GetAxis ("Horizontal"); //turn
		float v = Input.GetAxis ("Vertical");	//forward
		bool deAccel = Input.GetButton ("Vertical"); //if vertical isnt pressed
		bool brake = Input.GetButton ("Jump"); //if space bar is pressed, brake

		//Tank Roll Control
		VehicleRollInput ();

		//Camera movement
		//currentX += Input.GetAxis ("Mouse X") * sensitivityX;
		//currentY += Input.GetAxis ("Mouse Y") * sensitivityY;
		float cameraY = Input.GetAxis("Mouse Y");
		float cameraX = Input.GetAxis("Mouse X");

		cameraControl.ZoomInOut (Input.GetAxis ("Mouse ScrollWheel"));
		if (Input.GetMouseButtonDown(2))
			cameraControl.ResetCam();

		//variables sent to tankcameracontrol script
		cameraControl.UpdateCameraMovement(cameraX,cameraY);

		//variables sent to tank
		tankMotor.MoveChassis (h , v , deAccel, brake);
		print ("update");

	}
	void VehicleRollInput() //DOES Roll Input
	{
		print ("called");
		bool left  = Input.GetButton("BumperLeft");
		bool right = Input.GetButton("BumperRight");
		print(Input.GetButton("BumperLeft"));
		print(Input.GetButton("BumperRight"));

		vehicleRollControl.RollVehicle (left,right);
	}
}
