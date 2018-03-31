//Sam
//AI SCRIPTING 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AiAxleInfo{
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
[System.Serializable]
public class SensorInfo{
	//Sensor Variables
	public float sensorLength = 5f; //length of front sensors
	public float frontSensorStartPoint = 2.5f; //sensor line ray starts in front of vehicle
	public float frontSensorSideDist = 3f; //sensor lines coming from side of vehicle
	public float frontSensorAngle = 30f; //sensor angle of angle side sensors
	public float sidewaySensorLength = 5f; //length of side sensors
	public float avoidSensitivity; //sensitivity of steering when facing incoming obstacle
	public float avoidSpeed = 10f;
	[HideInInspector]
	public float flag;	//Flag is to declare if we are in avoidance mode
}
[RequireComponent(typeof(AITurret))]
public class AITankControl : MonoBehaviour {

	//VEHICLE VARIABLES
	public List<AiAxleInfo> aIaxleInfo; //list of all wheel pairs
	public SensorInfo sensorInfo;
	public Transform pathGroup;
	Transform[] path;
	public int currentPathObj;
	public float distanceFromTarget = 20f;
	public float maxTorque = 5000f;
	public float topSpeed = 150f;
	public float currentSpeed;
	[Tooltip("The speed of deacceleration when torque isn't used.")]
	public float deAccelSpeed = 1;
	public float maxSteer = 15.0f;
	public float maxBrakeTorque = 100f;

	//Reverse Variables
	bool reversing = false;
	float reverseCounter = 0.0f;
	float waitToReverse = 2.0f;
	float reversFor = 3.5f;
	float resumeSteerCounter = 0;

	public Vector3 newCenterOfMass;

	AITurret aiTurret;
	Rigidbody rb;
	Transform sensorStartPoint; // chelsey : The point where the sensors start. to avoid hitting the Terrain collider

	void Start ()
	{
		aiTurret = GetComponent<AITurret>();
		sensorStartPoint = transform.Find("SensorStartPoint");

		FindPathGroup ();
		SetCenterOfMass ();
	}
	void Update () {
		if (sensorInfo.flag == 0) {	// if there's no obstacle detected
			if (aiTurret.playerLastSeen != Vector3.zero) {	// and player is seen
				SteerTowardPlayer ();
			}else 
				{
				if (resumeSteerCounter > 1f) {
					GetSteer (); //change steering towards target
				}
			}
		}
		ReverseCheck(); // is called when reverse is true
		Sensors (); //sense for obstacles
		WheelPosition ();//update wheels mesh
		AIMove (); //move vehicle forward 
		
		resumeSteerCounter += Time.deltaTime;
	}
	public void SetCenterOfMass(){
		//SET CENTER OF MASS FOR VEHICLE: DONT FLIP
		rb = GetComponent<Rigidbody> ();

		rb.centerOfMass = newCenterOfMass;
	}
	//DETECTS COLLSION IN FRONT OF THE CAR
	void Sensors(){
		sensorInfo.flag = 0;
		sensorInfo.avoidSensitivity = 0;
//		float direction = 0;

		Vector3 pos;
		RaycastHit hit;
		var rightAngle = Quaternion.AngleAxis (sensorInfo.frontSensorAngle, sensorStartPoint.up) * sensorStartPoint.forward;
		var leftAngle = Quaternion.AngleAxis (-sensorInfo.frontSensorAngle, sensorStartPoint.up) * sensorStartPoint.forward;

		pos = sensorStartPoint.position;
		pos += sensorStartPoint.forward * sensorInfo.frontSensorStartPoint;

		//BRAKING SENSOR *******************
		//		if (Physics.Raycast (pos, sensorStartPoint.forward, out hit, sensorInfo.sensorLength)) {
		//			if (hit.transform.tag != "Terrain") {
		//				sensorInfo.flag++;
		//				//set motor wheel to deaccel speed
		//				Debug.DrawLine (pos, hit.point, Color.white, 1f, false);
		//				foreach (AiAxleInfo axleInfos in aIaxleInfo) {
		//						//print ("braking");
		//						axleInfos.leftWheel.brakeTorque = maxBrakeTorque;
		//						axleInfos.rightWheel.brakeTorque = maxBrakeTorque;
		//						axleInfos.leftWheel.motorTorque = 0;
		//						axleInfos.rightWheel.motorTorque = 0;
		//				}
		//			}
		//		} else {
		//			foreach (AiAxleInfo axleInfos in aIaxleInfo) {
		//				//print ("braking");
		//				axleInfos.leftWheel.brakeTorque = 0;
		//				axleInfos.rightWheel.brakeTorque = 0;
		//
		//			}
		//		}


		//FRONT MID RIGHT SENSOR
		pos += sensorStartPoint.right * sensorInfo.frontSensorSideDist;
		if (Physics.Raycast (pos, sensorStartPoint.forward, out hit, sensorInfo.sensorLength)) {
			if (hit.transform.tag != "Terrain") { //what to avoid in game
				sensorInfo.flag++;
				sensorInfo.avoidSensitivity -= 0.9f;
				Debug.DrawLine (pos, hit.point, Color.red, 0f, false);

			}
		} //FRONT RIGHT ANGLE SENSOR
		else if (Physics.Raycast (pos, rightAngle,out hit, sensorInfo.sensorLength)) {
			if (hit.transform.tag != "Terrain") {
				sensorInfo.flag++;
				sensorInfo.avoidSensitivity -= 0.5f;//if only angle sensor is detected then make obtuse steer angle
				Debug.DrawLine (pos, hit.point, Color.green, 0f, false);
				resumeSteerCounter = 0;
			}
		}//RIGHT SIDEWAYS SENSOR
		else if (Physics.Raycast (sensorStartPoint.position, sensorStartPoint.right,out hit, sensorInfo.sidewaySensorLength)) {
			if (hit.transform.tag != "Terrain") {
				sensorInfo.flag++;
				sensorInfo.avoidSensitivity -= 0f;
				Debug.DrawLine (sensorStartPoint.position, hit.point, Color.blue, 0f, false);
			}
		}

		//FRONT MID LEFT SENSOR
		pos = sensorStartPoint.position;
		pos += sensorStartPoint.forward*sensorInfo.frontSensorStartPoint;
		pos -= sensorStartPoint.right * sensorInfo.frontSensorSideDist;
		if (Physics.Raycast (pos, sensorStartPoint.forward, out hit, sensorInfo.sensorLength)) 
		{
			if (hit.transform.tag != "Terrain") { //what to avoid in game
				sensorInfo.flag++;
				sensorInfo.avoidSensitivity += 0.9f;
				Debug.DrawLine (pos, hit.point, Color.yellow, 0f, false);
			}
		}//FRONT  LEFT ANGLE SENSOR
		else if (Physics.Raycast (pos, leftAngle,out hit, sensorInfo.sensorLength)) 
		{
			if (hit.transform.tag != "Terrain") {
				sensorInfo.flag++;
				sensorInfo.avoidSensitivity += 0.5f;
				Debug.DrawLine (pos, hit.point, Color.cyan, 0f, false);
				resumeSteerCounter = 0;

			}
		}//LEFT SIDEWAYS SENSOR
		else if (Physics.Raycast (sensorStartPoint.position, -sensorStartPoint.right,out hit, sensorInfo.sidewaySensorLength)) {
			if (hit.transform.tag != "Terrain") {
				sensorInfo.flag++;
				sensorInfo.avoidSensitivity += 0f;
				Debug.DrawLine (sensorStartPoint.position, hit.point, Color.magenta, 0f, false);
			}
		}



		pos = sensorStartPoint.position;
		pos += sensorStartPoint.forward * sensorInfo.frontSensorStartPoint;

		//FRONT MID SENSOR
		if ( sensorInfo.avoidSensitivity == 0) { //if vehicle AI doesnt know wheether to go left or right head on colllsion
			if (Physics.Raycast (pos, sensorStartPoint.forward, out hit, sensorInfo.sensorLength)) {
				if (hit.transform.tag != "Terrain") {
					if (hit.normal.x < 0) {
						sensorInfo.avoidSensitivity = -1f;
						Debug.DrawLine (pos, hit.point, Color.white, 1f, false);
					} else {
						sensorInfo.avoidSensitivity = 1f;
					}
				}
			}
		}

		//ReverseCheck ();


		if (sensorInfo.flag != 0) {//if a sensor is being triggered then activate avoid steering
			if (!reversing)
				AvoidSteer (sensorInfo.avoidSensitivity);
			else {
				AvoidSteer (sensorInfo.avoidSensitivity * -1f);
			}
		}
	}
	void ReverseCheck(){
		//ReverseFunction
		if (rb.velocity.magnitude < 1 && !reversing) {
			reverseCounter += Time.deltaTime;
			if (reverseCounter >= waitToReverse) {
				reverseCounter = 0;
				reversing = true;
			}
		}
		else if (!reversing) {
			reverseCounter = 0;
		}

		if (reversing) {
//			print("reversing");
			sensorInfo.avoidSensitivity *= -1;
			reverseCounter += Time.deltaTime;
			if (reverseCounter >= reversFor) {
				reverseCounter = 0;
				reversing = false;
			}
		}
	}

	void AIMove(){// SETS SPEED, LIMITS SPEED.
		foreach (AiAxleInfo axleInfos in aIaxleInfo) {

			if (axleInfos.motor == true) {
				//set current speed
				currentSpeed = Mathf.Round(rb.velocity.magnitude * 2.237f);
				currentSpeed = Mathf.Round (currentSpeed);

				if (currentSpeed <= topSpeed) {
					if (!reversing) {
						axleInfos.leftWheel.motorTorque = maxTorque;
						axleInfos.rightWheel.motorTorque = maxTorque;
						axleInfos.leftWheel.brakeTorque = 0;
						axleInfos.rightWheel.brakeTorque = 0;
					} else {
						axleInfos.leftWheel.motorTorque = -maxTorque;
						axleInfos.rightWheel.motorTorque = -maxTorque;
					}
				} else {
					//balance out top speed
					axleInfos.leftWheel.motorTorque = 0f;
					axleInfos.rightWheel.motorTorque = 0f;
				}
			}
			ApplySteeringToMesh (axleInfos);

		}
	}
	//Called when car detects obstacle in front of it. Changes steering
	void AvoidSteer(float sensitivity){
		foreach (AiAxleInfo axleInfos in aIaxleInfo) {
			if (axleInfos.steering == true) { // if these wheels are steering then change their angle to maxSteer angle X sensitivity.
				axleInfos.rightWheel.steerAngle = Mathf.Lerp(axleInfos.rightWheel.steerAngle,sensorInfo.avoidSpeed *sensitivity, Time.deltaTime * 10);
				axleInfos.leftWheel.steerAngle = Mathf.Lerp(axleInfos.leftWheel.steerAngle,sensorInfo.avoidSpeed *sensitivity, Time.deltaTime * 10);
				//axleInfos.rightWheel.steerAngle = sensorInfo.avoidSpeed * sensitivity;
				//axleInfos.leftWheel.steerAngle = sensorInfo.avoidSpeed * sensitivity;
			}
		}
	}
	//ALLIGNS STEER ANGLE TO CURRENT TRANSFORM WAYPOINT POSITION
	void GetSteer()
	{

		//Vector3 steerVector = transform.InverseTransformPoint(Vector3(path[currentPathObj].position.x,transform.position.y,path[currentPathObj].position.z));
		Vector3 tempVector = new Vector3(path[currentPathObj].position.x,transform.position.y,path[currentPathObj].position.z);
		Vector3 steerVector = transform.InverseTransformPoint (tempVector);
		float newSteer = maxSteer * (steerVector.x / steerVector.magnitude);	

		foreach (AiAxleInfo axleInfos in aIaxleInfo) {
			if (axleInfos.steering == true) {
				axleInfos.leftWheel.steerAngle = newSteer;
				axleInfos.rightWheel.steerAngle = newSteer;
			}
		}
		if (steerVector.magnitude <= distanceFromTarget) {
			currentPathObj++;
		}if (currentPathObj >= path.Length) {
			currentPathObj = 0;
		}
	}

	void SteerTowardPlayer()
	{
		Vector3 newTempVector = new Vector3(aiTurret.playerLastSeen.x,transform.position.y,aiTurret.playerLastSeen.z);
		Vector3 newSteerVector = transform.InverseTransformPoint (newTempVector);
		float newSteertoPlayer = maxSteer * (newSteerVector.x / newSteerVector.magnitude);	

		foreach (AiAxleInfo axleInfos in aIaxleInfo) {
			if (axleInfos.steering == true) {
				axleInfos.leftWheel.steerAngle = newSteertoPlayer;
				axleInfos.rightWheel.steerAngle = newSteertoPlayer;
			}
		}
	}
	//	Chelsey - Find the closest Path Group if one isnt assigned
	void FindPathGroup()
	{
		GameObject[] allPathGroups = GameObject.FindGameObjectsWithTag ("Path Group");
	
		float closestDistance = 0f;
		for (int i = 0; i < allPathGroups.Length; i++) 
		{
			if (i > 0)
			{
				float thisDistance;
				thisDistance = (allPathGroups [i].transform.position - this.transform.position).magnitude;
				if (thisDistance < closestDistance) 
				{
					closestDistance = thisDistance;
					pathGroup = allPathGroups [i].transform;
				}
			}
			else
			{
				closestDistance = (allPathGroups [i].transform.position - this.transform.position).magnitude;
				pathGroup = allPathGroups [i].transform;
			}
		}
		GetPath ();
	}

	//GRABS PATH OF PATH GROUP OBJECT
	void GetPath(){

		path = pathGroup.GetComponentsInChildren<Transform> ();
		List<Transform> path_objs = new List<Transform> ();

		foreach (Transform path_obj in path) {
			if (path_obj != this.pathGroup) {
				path_objs.Add (path_obj);
			}
		}

	}
	//APPLIES WHEEL MESH TO ROTATE WITH COLLIDER
	public void ApplySteeringToMesh(AiAxleInfo wheelPair){ //CHANGE WHEEL MESH TO MATCH WHEEL COLLIDER
		//SETS FORWARD ROTATION
		wheelPair.leftWheelMesh.transform.Rotate (Vector3.left, Time.deltaTime * wheelPair.leftWheel.rpm * -10, Space.Self);
		wheelPair.rightWheelMesh.transform.Rotate (Vector3.left, Time.deltaTime * wheelPair.rightWheel.rpm * -10, Space.Self);

		float wheelRotation = wheelPair.leftWheel.steerAngle;
		//SETS STEER ROTATION
		wheelRotation = wheelPair.leftWheel.steerAngle - wheelPair.leftWheelMesh.transform.localEulerAngles.z;
		wheelPair.leftWheelMesh.transform.localEulerAngles = new Vector3 (wheelPair.leftWheelMesh.transform.localEulerAngles.x, wheelRotation, wheelPair.leftWheelMesh.transform.localEulerAngles.z);

		wheelRotation = wheelPair.rightWheel.steerAngle - wheelPair.rightWheelMesh.transform.localEulerAngles.z;
		wheelPair.rightWheelMesh.transform.localEulerAngles = new Vector3 (wheelPair.rightWheelMesh.transform.localEulerAngles.x, wheelRotation, wheelPair.rightWheelMesh.transform.localEulerAngles.z);
	}
	//APPLIES WHEEL MESH TO MATCH COLLIDER SUSPENSION
	public void WheelPosition(){ 
		foreach (AiAxleInfo axleInfos in aIaxleInfo) {
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
	//When avoid steer is over and getsteer is accessed needs buffer time

}
