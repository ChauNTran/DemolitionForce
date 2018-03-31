using UnityEngine;
using System.Collections;

public class DisableObjectByDistance : MonoBehaviour {

	public float availableDistance;
	private float distance;
	public GameObject theGameObject;

	void Update () {
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		distance = Vector3.Distance (Player.transform.position, transform.position);

		if (distance < availableDistance) {
			if(theGameObject != null)
			theGameObject.SetActive (true);
		} else {
			if(theGameObject != null)
			theGameObject.SetActive (false);
		}
	}
}
