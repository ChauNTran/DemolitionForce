using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBarScript : MonoBehaviour {

	AsyncOperation ao;
	public GameObject loadingScreenBG;
	public Image progBar;
	public Text loadingText;

	public bool loadWithFakeProgress;
	public float fakeIncrement = 0f;
	public float fakeTiming = 0f;
	[HideInInspector ]public bool loaded;

	void Start () {
	}
	
	void Update () {
	
	}

	public void LoadLevel(string level){

		loadingScreenBG.SetActive (true);
		progBar.gameObject.SetActive (true);
		loadingText.gameObject.SetActive (true);
		loaded = false;
		loadingText.text = "Loading...";
		Time.timeScale = 1f;
		if (!loadWithFakeProgress) {
			StartCoroutine (LoadLevelWithProgress (level));
		} else {
			StartCoroutine (LoadLevelWithFakeProgress(level));
		}
	}

	IEnumerator LoadLevelWithProgress(string level){

		loadingScreenBG.GetComponent<Image> ().canvasRenderer.SetAlpha(0.0f);
		loadingScreenBG.GetComponent<Image> ().CrossFadeAlpha(1.0f,1.0f, false);


		yield return new WaitForSeconds (1f);

		ao = SceneManager.LoadSceneAsync(level);
		ao.allowSceneActivation = false;

		while (!ao.isDone) {

			//update progress
			progBar.fillAmount = ao.progress;
			loadingText.text = "Progress: " + (int)progBar.fillAmount;
			print (ao.progress);
			if (ao.progress == 0.9f) {
				loaded = true;
				progBar.fillAmount = 1;
				loadingText.text = "Complete";
				ao.allowSceneActivation = true;
			}

			//Debug.Log (ao.progress);
			yield return null;

		}

	}
	IEnumerator LoadLevelWithFakeProgress(string level){

		loadingScreenBG.GetComponent<Image> ().canvasRenderer.SetAlpha(0.0f);
		loadingScreenBG.GetComponent<Image> ().CrossFadeAlpha(1.0f,1.0f, false);

		yield return new WaitForSeconds (1f);

		ao = SceneManager.LoadSceneAsync(level);
		ao.allowSceneActivation = false;

		while(progBar.fillAmount != 1){
			progBar.fillAmount += fakeIncrement;
			yield return new WaitForSeconds (fakeTiming);
			loadingText.text = (int)(progBar.fillAmount * 100) + " %";
		}

		if (progBar.fillAmount == 1f) {
			loadingText.text = "100 %";
			loaded = true;
			//click to continue
			ao.allowSceneActivation = true;
		}

		yield return null;

	}
}
