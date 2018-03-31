//Music List

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip menuTheme;
	public AudioClip openWorldTheme;
	public AudioClip tutorialTheme;
	public AudioClip optionsTheme;
	public AudioClip upgradeTheme;
	public AudioClip battleTheme;
	public AudioClip settingsTheme;
	public AudioClip lobbyTheme;

	public static MusicManager instance;

	string sceneName;

	void Awake(){
		instance = this;
		OnLevelWasLoaded (0);

	}
		
	void OnLevelWasLoaded(int sceneIndex)// will be outdated soon
	{
		string newSceneName = SceneManager.GetActiveScene ().name;
		if (newSceneName != sceneName) {
			sceneName = newSceneName;
			Invoke ("PlayMusic", .2f);
		}
	}

	void PlayMusic(){
		AudioClip clipToPlay = null;

		if (sceneName == "Main Menu") {
			clipToPlay = menuTheme;
		} else if (sceneName == "OpenWorldMap") {
			clipToPlay = openWorldTheme;
		} else if (sceneName == "Upgrade") {
			clipToPlay = upgradeTheme;
		} else if (sceneName == "Settings") {
			clipToPlay = settingsTheme;
		} else if (sceneName == "Demo for International Gaming Day") {
			clipToPlay = tutorialTheme;
		} else if (sceneName == "Lobby") {
			clipToPlay = lobbyTheme;
		}

		if (clipToPlay != null) {
			AudioManager.instance.PlayMusic (clipToPlay, 2);
			Invoke ("PlayMusic", clipToPlay.length);
		} else {
			AudioManager.instance.PlayMusic (null, 2);
		}

	}
}