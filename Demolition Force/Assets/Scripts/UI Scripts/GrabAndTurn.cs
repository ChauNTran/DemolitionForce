// Chau Tran
// where to put:	in the Game Controller of the Upgrade scene.
// purpose:			enable the player to grab the tank and spin it around

using UnityEngine;
using System.Collections;

[RequireComponent(typeof (BoxCollider))]

public class GrabAndTurn : MonoBehaviour {

	public Transform tankObject;
	Vector3 mousePos;
	Quaternion originalRotation;

	void OnMouseDown()
	{
		mousePos = Input.mousePosition;
		originalRotation = tankObject.transform.rotation;
	}
	void OnMouseDrag()
	{
		Vector3 offset = mousePos  - Input.mousePosition ;
		Quaternion newRotation;
		newRotation = Quaternion.Euler (originalRotation.eulerAngles.x, originalRotation.eulerAngles.y + offset.x, originalRotation.eulerAngles.z);
		tankObject.rotation = newRotation;
	}
}
