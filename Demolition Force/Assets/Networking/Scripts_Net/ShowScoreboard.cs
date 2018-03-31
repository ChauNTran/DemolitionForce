using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScoreboard : MonoBehaviour {

	public GameObject scoreBoard;


	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			scoreBoard.SetActive (!scoreBoard.activeSelf);
		}
		if (Input.GetKeyUp (KeyCode.Tab)) {
			scoreBoard.SetActive (!scoreBoard.activeSelf);
		}
	}
}
