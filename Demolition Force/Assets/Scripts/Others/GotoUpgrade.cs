using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GotoUpgrade : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Bullet") {
			SceneManager.LoadScene ("Upgrade");
		}
	}
}
