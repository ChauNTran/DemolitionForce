//theis is used to create a black fade in panel for scenes when they start

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	public GameObject fadePanel;
	public float fadeDuration;

	void Start () {
		fadePanel.SetActive(true);
		fadePanel.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
		StartCoroutine(FadeIn());
	}

	IEnumerator FadeIn(){
		fadePanel.GetComponent<Image>().CrossFadeAlpha(0.0f,fadeDuration,false);

		yield return new WaitForSeconds(fadeDuration);

		fadePanel.SetActive(false);


	}
}
