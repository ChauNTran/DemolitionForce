using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyCheckManager : MonoBehaviour {

	GameObject[] enemies;
	bool switched;
	int enemyCount;
	LoadingBarScript loadBarScript;

	void Start () {
		//loadBarScript = (LoadingBarScript).gameObject.GetComponent (typeof(LoadingBarScript));
		loadBarScript = GameObject.Find("LoadingSceneScript").GetComponent<LoadingBarScript>();
	}
	
	void Update () {
		CheckEnemies();
	}

	void CheckEnemies(){

		enemies = GameObject.FindGameObjectsWithTag ("Tank");

		for (int i = 0; i <= enemies.Length; i++) {
			if (enemies.Length == 0 && switched){
				print("all eliminated");
				if(switched){
				switched = false;
				StartCoroutine(GoToNewLevel());
				}
			}
			if(enemies.Length > 1){
				switched = true;
			}
		}
	}

	IEnumerator GoToNewLevel(){
		yield return new WaitForSeconds(1f);
		loadBarScript.LoadLevel ("OpenWorldMap");

	}
}
