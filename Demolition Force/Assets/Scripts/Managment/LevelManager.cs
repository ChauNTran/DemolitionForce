using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void ChangeLevel(string level){
		SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
	}
}
