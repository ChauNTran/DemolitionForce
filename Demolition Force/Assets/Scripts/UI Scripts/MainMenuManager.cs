using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuManager : MonoBehaviour {
	
	AsyncOperation gameAsync;
	public string gameSceneTitle;
	public GameObject MainMenuPanel;
	public GameObject TutorialPanel;
	public GameObject Notetext;
	bool playpressed = false;

	bool multiplaypressed = false;
	float timer = 0f;

	void Awake ()
	{
		Time.timeScale = 1f;
		Notetext.SetActive (false);
		ShowMainMenuPanel ();
	}
	void Start()
	{
		Cursor.visible = true;
	}
	void Update () {
		if (multiplaypressed) {
			Notetext.SetActive (true);
			timer += Time.deltaTime;
			if (timer >= 5f) {
				Notetext.SetActive (false);
				multiplaypressed = false;
				timer = 0f;
			}
		}
		if(playpressed)
		print (gameAsync.progress);
	}
	public void MultiplayerPressed()
	{
		multiplaypressed = true;
	}
	public void ShowTutorialPanel()
	{
		MainMenuPanel.SetActive (false);
		TutorialPanel.SetActive (true);
	}
	public void ShowMainMenuPanel()
	{
		MainMenuPanel.SetActive (true);
		TutorialPanel.SetActive (false);
	}
	public void PlayGame()
	{
		gameAsync = SceneManager.LoadSceneAsync (gameSceneTitle, LoadSceneMode.Single);
		playpressed = true;
	}
	public void ChangeLevel(string level){
		SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
	}

	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
