using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo{
	//used to create multiple sets of axle's 
	public string name;
	public WheelCollider leftWheel;
	public GameObject leftWheelMesh;
	public WheelCollider rightWheel;
	public GameObject rightWheelMesh;

	public bool motor; //check if this axle applies force
	public bool steering; //check if this axle applies turning
	public bool reverseSteering;
}


public class TankMotor: MonoBehaviour {

	//VEHICLE VARIABLES
	public List<AxleInfo> axleInfo; //list of all wheel pairs
	public float maxMotorTorque;
	public float lowSpeedSteerAngle; //steering handling at lowest speed
	public float highSpeedSteerAngle; //steer handling at highest speed

	[Tooltip("The speed of deacceleration when torque isn't used.")]
	public float deAccelSpeed = 1;

	public float currentSpeed;
	[Tooltip("Top speed of vehicle")]
	public float topSpeed = 150;
	public float topReverseSpeed = 30;

//	bool braked = false;
	public float maxBrakeTorque = 100f;

	Rigidbody rb;
	public Vector3 newCenterOfMass;

	//wheel collider values to set and change to create slip and friction
	private float sidewayFriction;
	private float forwardFriction;
	private float slipSidewayFriction;
	private float slipForwardFriction;

	//engine sound for gear levels
	[Tooltip("Gear levels to change engine sound")]
	public int[] gearRatio;
	public int gear;

	void Start () {
		//sets center of mass for vehicle to avoid flipping
		rb = GetComponent<Rigidbody> ();

		SetCenterOfMass();

		SetWheelColliderValues ();
	}
	
	void FixedUpdate () {
		WheelPosition ();
		EngineSound ();
		//LimitSteerSpeed ();

	}
	public void ApplySteeringToMesh(AxleInfo wheelPair){ //CHANGE WHEEL MESH TO MATCH WHEEL COLLIDER
		//SETS FORWARD ROTATION
		wheelPair.leftWheelMesh.transform.Rotate (Vector3.left, Time.deltaTime * wheelPair.leftWheel.rpm * -10, Space.Self);
		wheelPair.rightWheelMesh.transform.Rotate (Vector3.left, Time.deltaTime * wheelPair.rightWheel.rpm * -10, Space.Self);
		//wheelPair.leftWheelMesh.transform.Rotate (wheelPair.leftWheel.rpm / 60 * 360 * Time.deltaTime, 0, 0);
		//wheelPair.rightWheelMesh.transform.Rotate (wheelPair.rightWheel.rpm / 60 * 360 * Time.deltaTime, 0, 0);


		float wheelRotation = wheelPair.leftWheel.steerAngle;
		//SETS STEER ROTATION
		wheelRotation = wheelPair.leftWheel.steerAngle - wheelPair.leftWheelMesh.transform.localEulerAngles.z;
		wheelPair.leftWheelMesh.transform.localEulerAngles = new Vector3 (wheelPair.leftWheelMesh.transform.localEulerAngles.x, wheelRotation, wheelPair.leftWheelMesh.transform.localEulerAngles.z);

		wheelRotation = wheelPair.rightWheel.steerAngle - wheelPair.rightWheelMesh.transform.localEulerAngles.z;
		wheelPair.rightWheelMesh.transform.localEulerAngles = new Vector3 (wheelPair.rightWheelMesh.transform.localEulerAngles.x, wheelRotation, wheelPair.rightWheelMesh.transform.localEulerAngles.z);
	}
	//APPLIES WHEEL MESH TO MATCH COLLIDER SUSPENSION
	public void WheelPosition(){ 
		foreach (AxleInfo axleInfos in axleInfo) {
			RaycastHit hit;
			Vector3 leftWheelPos;
			Vector3 rightWheelPos;
			if (Physics.Raycast (axleInfos.leftWheel.transform.position, -axleInfos.leftWheel.transform.up, out hit, axleInfos.leftWheel.radius + axleInfos.leftWheel.suspensionDistance)) {
				leftWheelPos = hit.point + axleInfos.leftWheel.transform.up * axleInfos.leftWheel.radius;
			} else {
				leftWheelPos = axleInfos.leftWheel.transform.position - axleInfos.leftWheel.transform.up * axleInfos.leftWheel.suspensionDistance;
			}
			if (Physics.Raycast (axleInfos.rightWheel.transform.position, -axleInfos.rightWheel.transform.up, out hit, axleInfos.rightWheel.radius + axleInfos.rightWheel.suspensionDistance)) {
				rightWheelPos = hit.point + axleInfos.rightWheel.transform.up * axleInfos.rightWheel.radius;
			} else {
				rightWheelPos = axleInfos.rightWheel.transform.position - axleInfos.rightWheel.transform.up * axleInfos.rightWheel.suspensionDistance;
			}


			axleInfos.leftWheelMesh.transform.position = leftWheelPos;
			axleInfos.rightWheelMesh.transform.position = rightWheelPos;
		}
	}
	public void HandBrake(bool brake){
		foreach (AxleInfo axleInfos in axleInfo) {
			if (brake) {
				//print ("braking");
				axleInfos.leftWheel.brakeTorque = maxBrakeTorque;
				axleInfos.rightWheel.brakeTorque = maxBrakeTorque;
				axleInfos.leftWheel.motorTorque = 0;
				axleInfos.rightWheel.motorTorque = 0;
				if (rb.velocity.magnitude > 25f) 
				{
					SetSlip (slipForwardFriction, slipSidewayFriction);
				} else {
					SetSlip (1, 1);
				}
			}
		}
	}

	public void SetCenterOfMass(){
		//SET CENTER OF MASS FOR VEHICLE: DONT FLIP

		rb.centerOfMass = newCenterOfMass;
	}

	public void MoveChassis(float steering, float motor, bool deAccel, bool brake) //VEHICLE MOVEMENT
	{
		motor *= maxMotorTorque;
		steering *= LimitSteerSpeed(); //Multiply Horizontal(steering) by steer angle, steerangle changes depending on speed

		//SteerHandling
			//float speedFactor = rb.velocity.magnitude / lowestSteerAtSpeed;
			//float currentSteerAngle = Mathf.Lerp (lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor);
			//currentSteerAngle *= steering;

		foreach (AxleInfo axleInfos in axleInfo) {
			//STEERING
			if (axleInfos.steering == true) 
			{
				axleInfos.leftWheel.steerAngle = steering;		// the steer needs to be smoother. Reccommend using Lerp
				axleInfos.rightWheel.steerAngle = steering;		//	the steer need to be smoother. Reccommend using Lerp
//				if (Mathf.Abs (steering) >= 0.1f) {
//					axleInfos.leftWheel.steerAngle = Mathf.Lerp (axleInfos.leftWheel.steerAngle, steering, 3f * Time.deltaTime);
//					axleInfos.rightWheel.steerAngle = Mathf.Lerp (axleInfos.rightWheel.steerAngle, steering, 3f * Time.deltaTime);
//				} 
//				else 
//				{
//					axleInfos.leftWheel.steerAngle = steering;		// the steer needs to be smoother. Reccommend using Lerp
//					axleInfos.rightWheel.steerAngle = steering;		//	the steer need to be smoother. Reccommend using Lerp
//				}
			}
			//MOTOR
			if (axleInfos.motor == true) 
			{
				currentSpeed = Mathf.Round(rb.velocity.magnitude * 2.237f);
				if (brake) {
					HandBrake (brake);
				} else {
					SetSlip (forwardFriction, sidewayFriction);
					axleInfos.leftWheel.brakeTorque = 0;
					axleInfos.rightWheel.brakeTorque = 0;
				}

				if (currentSpeed < topSpeed && currentSpeed > -topReverseSpeed && !brake) {
					//if speed is lower than top speed
					axleInfos.leftWheel.motorTorque = motor;
					//print (axleInfos.leftWheel.motorTorque);
					axleInfos.rightWheel.motorTorque = motor;
					axleInfos.leftWheel.brakeTorque = 0;
					axleInfos.rightWheel.brakeTorque = 0;
				} else {
					//if speed is faster than top speed then equal out
					//print("top speed hit");
					axleInfos.leftWheel.motorTorque = 0f;
					axleInfos.rightWheel.motorTorque = 0f;
				}
			}
			//REVERSE STEERING
			if (axleInfos.reverseSteering == true) 
			{
				axleInfos.leftWheel.steerAngle = -steering;
				axleInfos.rightWheel.steerAngle = -steering;
			}
			//DEACCELERATION
			if (deAccel == false) { //if no gas is given, slow car, de-accel
				rb.drag = deAccelSpeed * 0.1f;
				//axleInfos.leftWheel.brakeTorque = deAccelSpeed;
				//axleInfos.rightWheel.brakeTorque = deAccelSpeed;
			} else {									//if gas is changed turn brake off
				rb.drag = 0;
				axleInfos.leftWheel.brakeTorque = 0f;
				axleInfos.rightWheel.brakeTorque = 0f;
			}
			ApplySteeringToMesh (axleInfos);
			//currentSpeed = Mathf.Round(2 * Mathf.PI * axleInfos.leftWheel.radius * axleInfos.leftWheel.rpm * 60 / 1000);
			//print (rb.velocity);
		}
	}
	public void SpeedUp(float newTorque)
	{
		this.maxMotorTorque = newTorque;
	}
	//LIMITS THE STEERING OF THE VEHICLE AT HIGH SPEEDS
	public float LimitSteerSpeed(){


		float speedFactor = rb.velocity.magnitude / topSpeed * 2;//gets a percentage from 0 to top speed
		float currentSteerAngle = Mathf.Lerp (lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor); //interpolates between the two steer angles
		return currentSteerAngle; //returns limited new steer angle


	}
	//CHANGES WHEEL VALUES WHEN BREAK TO CREATE SLIP AND SKID
	public void SetWheelColliderValues()
	{
		foreach (AxleInfo axleInfos in axleInfo) {
			if (axleInfos.name == "Rear" || axleInfos.name == "Mid") 
			{
				forwardFriction = axleInfos.leftWheel.forwardFriction.stiffness;
				forwardFriction = axleInfos.rightWheel.forwardFriction.stiffness;

				sidewayFriction = axleInfos.leftWheel.sidewaysFriction.stiffness;
				sidewayFriction = axleInfos.rightWheel.sidewaysFriction.stiffness;

				slipForwardFriction = 0.75f;
				slipSidewayFriction = 2.2f;
			}
		}
	}
	public void SetSlip(float currentForwardFriction, float currentSidewaysFriction)
	{
		foreach (AxleInfo axleInfos in axleInfo) {
			if (axleInfos.name == "Rear" || axleInfos.name == "Mid") {
				//DONT ASK ME ABOUT THIS BUSHLEAF OF CODE
				WheelFrictionCurve forwardFrictionCurveLeft = axleInfos.leftWheel.forwardFriction;
				WheelFrictionCurve sidewaysFrictionCurveLeft = axleInfos.leftWheel.sidewaysFriction;
				WheelFrictionCurve forwardFrictionCurveRight = axleInfos.rightWheel.forwardFriction;
				WheelFrictionCurve sidewaysFrictionCurveRight = axleInfos.rightWheel.sidewaysFriction;


				forwardFrictionCurveLeft.stiffness = currentForwardFriction;
				sidewaysFrictionCurveLeft.stiffness = currentSidewaysFriction;
				forwardFrictionCurveRight.stiffness = currentForwardFriction;
				sidewaysFrictionCurveRight.stiffness = currentSidewaysFriction;

				axleInfos.leftWheel.forwardFriction = forwardFrictionCurveLeft;
				axleInfos.rightWheel.forwardFriction = forwardFrictionCurveRight;

				axleInfos.leftWheel.sidewaysFriction = sidewaysFrictionCurveLeft;
				axleInfos.rightWheel.sidewaysFriction = sidewaysFrictionCurveRight;

			}
		}
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.transform != transform && other.contacts.Length != 0) {
			for (int i = 0; i < other.contacts.Length; i++) {
				//instantiate spark at other.contacts[i].point
				//AudioManager.instance.PlaySound("Vehicle Collisions",other.contacts[i].point);
				//other.gameObject.GetComponent<Rigidbody> ().AddForce (other.impulse * -1f);

			}
		}
	}
	void EngineSound(){
		//GetComponent<AudioSource> ().pitch = currentSpeed / topSpeed+1;

		for (int i = 0; i < gearRatio.Length; i++) {
			if (gearRatio [i] > currentSpeed) {
				//gear = i;
				break;
			}
			float gearMinValue = 0.0f;
			float gearMaxValue = 0.0f;
			if (i == 0) {
				gearMinValue = 0;
				gearMaxValue = gearRatio [i];
			} else {
				gearMinValue = gearRatio [i-1];
				gearMaxValue = gearRatio [i];

			}
				//gearMaxValue = gearRatio [i];

				float enginePitch = ((currentSpeed - gearMinValue) / (gearMaxValue - gearMinValue)) + 0.25f;
				GetComponent<AudioSource> ().pitch = enginePitch;
				GetComponent<AudioSource>().volume = AudioManager.instance.sfxVolumePercent * AudioManager.instance.masterVolumePercent;
		}

	}
}
