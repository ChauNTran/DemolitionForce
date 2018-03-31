using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<TutLevelUI> ().Continue ();
			Destroy (this.gameObject);
		}
	}
}
