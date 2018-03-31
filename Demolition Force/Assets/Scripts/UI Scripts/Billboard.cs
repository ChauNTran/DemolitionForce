using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	public Camera gameCamera;
	private float initialRotation;

	void Start()
	{
		gameCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();

		initialRotation = transform.rotation.z;
	}

	void Update()
	{
		transform.rotation = gameCamera.transform.rotation;
		transform.Rotate(Vector3.up * initialRotation * 180); // Fix Rotation (Flipped sprites)
	}
}
