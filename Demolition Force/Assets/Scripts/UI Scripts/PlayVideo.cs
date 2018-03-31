// Chau Tran

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayVideo : MonoBehaviour {

	public MovieTexture movie;
	public float waitTime = 3f;
	private float timer = 0;
	public string mainMenu;

	void Start () 
	{
		GetComponent<RawImage>().texture = movie as MovieTexture;
		movie.Play();	
		Time.timeScale = 1f;
		Cursor.visible = false;
	}
	void Update()
	{
		timer += Time.deltaTime;
		
		if (timer >= waitTime)
			SceneManager.LoadScene(mainMenu);
	}
	
}
