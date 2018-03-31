// chau tran
// where to put:	put in the player
// purpose:			get input from user

// NOTE: CHECK THE INPUT MANAGER TO SET THE INPUT NAMES

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof (TankMotorNetworking))]
[RequireComponent(typeof (TankCameraControlNetworking))]
[RequireComponent(typeof (TankShootNetworking))]

public class TankControllerNetworking : NetworkBehaviour {

	TankMotorNetworking tankMotor;
    TankCameraControlNetworking cameraControl;
	TankShootNetworking tankShoot;
	VehicleRollControl vehicleRollControl;

	float timer = 0f;
	float timer2 = 0f;
	[SerializeField] float rightTrigger;
	[SerializeField] float leftTrigger;
	//
	float L;
	float R;
	float F;
	float B;

//	[SerializeField]float cameraY;
//	[SerializeField]float cameraX;

	void Start () 
	{
		tankMotor = GetComponent<TankMotorNetworking> ();
		cameraControl = GetComponent<TankCameraControlNetworking> ();
		tankShoot = GetComponent<TankShootNetworking> ();
		vehicleRollControl = GetComponent<VehicleRollControl> ();
	}
	void FixedUpdate () 
	{
		rightTrigger = Input.GetAxis ("Fire1Axis");
		leftTrigger = Input.GetAxis ("Fire2Axis");
		//variables sent to tankcameracontrol script
		if (!isLocalPlayer) { //return if not local player
			return;
		}

		timer += Time.deltaTime;
		timer2 += Time.deltaTime;
		if (tankShoot.holdAndShoot)
		{
//			if (Input.GetButton ("Fire1"))
			if(Input.GetButton ("Fire1") || rightTrigger> 0.5f)
			if (timer > tankShoot.GetFireRate_primary())
			{
				tankShoot.Shoot ();

				timer = 0f;
			}
		}
		else
//			if (Input.GetButtonDown ("Fire1"))
			if(Input.GetButton ("Fire1") || rightTrigger> 0.5f)
			if (timer > tankShoot.GetFireRate_primary())
			{
				tankShoot.Shoot ();
				timer = 0f;
			}

		if (tankShoot.holdAndShoot)
		{
//			if (Input.GetButton ("Fire2"))
			if(Input.GetButton ("Fire2") || leftTrigger > 0.5f)
			if (timer2 > tankShoot.GetFireRate_Secondary())
			{
				tankShoot.CmdShootSecondary ();
				timer2 = 0f;
			}
		}
		else
//			if (Input.GetButtonDown ("Fire2"))
			if(Input.GetButton ("Fire2") || leftTrigger> 0.5f)
			if (timer2 > tankShoot.GetFireRate_Secondary())
			{
				tankShoot.CmdShootSecondary ();
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

		float cameraY;
		float cameraX; 

        cameraY = Input.GetAxis("Mouse Y");
        cameraX = Input.GetAxis("Mouse X");
	
		cameraControl.ZoomInOut (Input.GetAxis ("Mouse ScrollWheel"));
		if (Input.GetButtonDown("Reset Camera"))
			cameraControl.ResetCam();


		cameraControl.UpdateCameraMovement(cameraX,cameraY);
		//variables sent to tank
		tankMotor.MoveChassis (h , v , deAccel, brake);

		if (Input.GetButtonDown("Fire4")) //FLIP CAR ANGLES
		{
			gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
		}

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
