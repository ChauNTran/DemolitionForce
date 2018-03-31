using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class PlayerMotionBlur : MonoBehaviour {

	VignetteAndChromaticAberration motionBlurScript;
	TankMotorNetworking tankMotor;

	void Start () {
		motionBlurScript = gameObject.GetComponent<VignetteAndChromaticAberration> ();
		tankMotor = gameObject.transform.root.GetComponent<TankMotorNetworking> ();
	}


	void Update () {

		float multiplier = 0.65f / tankMotor.topSpeed;

		motionBlurScript.blur = multiplier * tankMotor.currentSpeed;


	}
}
