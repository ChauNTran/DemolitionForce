//chau tran
// where to put:	"Electric Poles with Cables" prefab
// purpose:		make cables for electric poles prefabs

// NOTE: DUPLICATE THE "electric_pole" child object. NOT the "Electric Poles with Cables"

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeElectricCable : MonoBehaviour {

	LineRenderer leftLineRenderer;
	LineRenderer rightLineRenderer;

	public List<Transform> leftPieces;
	public List<Transform> rightPieces;

	public List <Transform> Poles;

	void Start () 
	{
		GetPieces ();
	}
	void GetPieces()
	{
		// get the poles and line renderer
		foreach (Transform child in transform)
		{
			if (child.name == "Left Line")
				leftLineRenderer = child.GetComponent<LineRenderer> ();
			else if(child.name == "Right Line")
				rightLineRenderer = child.GetComponent<LineRenderer> ();
			else
				Poles.Add (child);
		}
		// get left and right pieces
		for (int i = 0; i < Poles.Count; i++)
		{
			foreach (Transform grandChild in Poles[i])
				if (grandChild.tag == "LeftPiece")
					leftPieces.Add (grandChild);
				else if(grandChild.tag == "RightPiece")
					rightPieces.Add (grandChild);
		}
		MakeCable ();
	}

	void MakeCable()
	{
		Vector3[] leftVectors = new Vector3[leftPieces.Count];
		Vector3[] rightVectors = new Vector3[rightPieces.Count];

		for (int i = 0; i < leftPieces.Count; i++) 
		{
			leftVectors [i] = new Vector3 (leftPieces [i].position.x, leftPieces [i].position.y, leftPieces [i].position.z);
		}

		for (int i = 0; i < leftPieces.Count; i++) 
		{
			rightVectors[i] = new Vector3(rightPieces[i].position.x,rightPieces[i].position.y,rightPieces[i].position.z);
		}

		leftLineRenderer.positionCount = leftPieces.Count;
		rightLineRenderer.positionCount = rightPieces.Count;

		leftLineRenderer.SetPositions (leftVectors);
		rightLineRenderer.SetPositions (rightVectors);
	}
}
