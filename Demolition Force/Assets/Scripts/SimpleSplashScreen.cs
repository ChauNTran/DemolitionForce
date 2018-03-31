using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimpleSplashScreen : MonoBehaviour {

	RawImage logoImage;
	float alpha = 0f;
	void Start ()
	{
		logoImage = GetComponent<RawImage> ();
		logoImage.color = new Color (1,1,1,0);
		StartCoroutine (FadeIn_Logo ());
	}
	IEnumerator FadeIn_Logo()
	{
		logoImage.color = new Color (1,1,1,alpha);

		if (alpha < 1f) {
			alpha += 0.02f;
			yield return new WaitForEndOfFrame ();
			StartCoroutine (FadeIn_Logo ());
		} else {
			alpha = 1f;
			logoImage.color = new Color (1,1,1,alpha);
			yield return new WaitForSeconds (2f);
			SceneManager.LoadScene ("Main Menu");
		}
	}
}
