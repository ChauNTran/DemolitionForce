using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoresList : MonoBehaviour {

	public GameObject playerScoreEntryPrefab;

	ScoreController scoreController;

	// Use this for initialization
	void Start () {
		scoreController = GameObject.FindObjectOfType<ScoreController>();

	}
	
	// Update is called once per frame
	void Update () {

		while (this.transform.childCount > 0) {
			Transform c = this.transform.GetChild (0);
			c.SetParent (null);  // Become Batman
			Destroy (c.gameObject);
		}

		string[] names = scoreController.GetPlayerNames ("kills");

		foreach (string name in names) {
			GameObject go = (GameObject)Instantiate (playerScoreEntryPrefab);
			go.transform.SetParent (this.transform);
			go.transform.Find ("PlayerName").GetComponent<Text> ().text = name;
			go.transform.Find ("Kills").GetComponent<Text> ().text = scoreController.GetScore (name, "kills").ToString();
			go.transform.Find ("Deaths").GetComponent<Text> ().text = scoreController.GetScore (name, "deaths").ToString();
		}
	}
}
