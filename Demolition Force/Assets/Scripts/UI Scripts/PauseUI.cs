// russell and chelsey
// purpose:			showing pause screen
// where to put:	PauseUI game object in the prefab

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour {

    public GameObject PauseScreen;
    public GameObject MainMenuScreen;
    public GameObject OptionsMenu;
	public GameObject TintBackground;

	private LockCursor lockCursorScript;
    private bool IsPaused = false;

	void Awake()
	{

		// asuming that these 3 game objects are the children objects of PauseUI
		// if they has not been called
		if (PauseScreen == null)
			// call them
			PauseScreen = transform.Find ("PauseScreen").gameObject;
		if (MainMenuScreen == null)
			PauseScreen = transform.Find ("MainMenuScreen").gameObject;
		if (OptionsMenu == null)
			PauseScreen = transform.Find ("OptionsScreen").gameObject;
		if(TintBackground == null)
			TintBackground = transform.Find ("TintBackground").gameObject;
	}

    void Start()
    {
		lockCursorScript = GameObject.FindWithTag ("GameController").GetComponent<LockCursor> ();
		lockCursorScript.LockAndHide ();
		Time.timeScale = 1f;

		PauseScreen.SetActive(false);
        MainMenuScreen.SetActive(false);
        OptionsMenu.SetActive(false);
		TintBackground.SetActive(false);
    }

    
    public void ShowPauseScreen()
    {
        PauseScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
        OptionsMenu.SetActive(false);
		TintBackground.SetActive(true);
		// show cursor
		lockCursorScript.UnlockAndShow ();
    }

    public void ShowMenuScreen()
    {
        PauseScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void ShowOptionScreen()
    {
        PauseScreen.SetActive(false);
        MainMenuScreen.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void NoScreenActive()
    {
        PauseScreen.SetActive(false);
        MainMenuScreen.SetActive(false);
        OptionsMenu.SetActive(false);
		TintBackground.SetActive(false);
		//hide cursor
		lockCursorScript.LockAndHide ();
		Time.timeScale = 1;

    }

	public void GotoMainMenuScene()
	{
		SceneManager.LoadScene (0);
	}

	// Update is called once per frame
	void Update () 
	{
			if (Input.GetKeyDown (KeyCode.Escape) && IsPaused == false) 
			{
            Time.timeScale = 0;
            IsPaused = true;
            ShowPauseScreen ();
            }

        else if (Input.GetKeyDown(KeyCode.Escape) && IsPaused == true)
        {
            Time.timeScale = 1;
            IsPaused = false;
            NoScreenActive(); 
        }
	}

	





}
