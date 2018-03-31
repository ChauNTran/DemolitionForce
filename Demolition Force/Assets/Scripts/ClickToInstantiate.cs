using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToInstantiate : MonoBehaviour {

	public GameObject gameObject;

	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			instantiateGameObject ();
		}
	}

	void instantiateGameObject(){
		Instantiate (gameObject, transform.position, transform.rotation);
	}
}
