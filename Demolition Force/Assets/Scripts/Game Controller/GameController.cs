

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {


	void Start(){
		FindObjectOfType<TankHealth>().deathEvent += OnPlayerDeath;
//		MusicManager.instance.SwitchMusic ();
		//FindObjectOfType<TankHealth>().deathEvent();
	}

	public void OnPlayerDeath(){
		//FindObjectOfType<TankHealth>().deathEvent -= OnPlayerDeath;
		GameObject.Find("GameOverUI").gameObject.transform.GetChild(0).gameObject.SetActive(true);
		GameObject.FindGameObjectWithTag("PauseUI").GetComponent<PauseUI> ().enabled = false;
		GetComponent<LockCursor> ().UnlockAndShow ();

		//LoadThisScene ();
	}

	public void LoadThisScene()
	{
		int sceneIndex = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (sceneIndex);
	}
}
