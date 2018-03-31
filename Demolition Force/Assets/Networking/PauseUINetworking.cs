// russell and chelsey
// purpose:			showing pause screen
// where to put:	PauseUI game object in the prefab

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;

public class PauseUINetworking: MonoBehaviour {
	[Header ("Panels")]
    public GameObject PauseScreen;
    public GameObject MainMenuScreen;
    public GameObject OptionsMenu;
	public GameObject TintBackground;
	[Header ("Buttons and Sliders")]
	public Button YesButton;
	public Slider XSensitivity;
	public Slider YSensitivity;

//	[SerializeField]private LockCursor lockCursorScript;
    private bool IsPaused = false;
	private bool InOptions = false;
	private CanvasGroup pauseCanvasGroup;
	private LobbyManager lobbyManager;
	void Reset()
	{
		PauseScreen = transform.Find ("PauseScreen").gameObject;
		MainMenuScreen = transform.Find ("MainMenuScreen").gameObject;
		OptionsMenu = transform.Find ("OptionsScreen").gameObject;
		TintBackground = transform.Find ("TintBackground").gameObject;

		YesButton = MainMenuScreen.transform.Find ("YeBTN").gameObject.GetComponent<Button>();
		XSensitivity = OptionsMenu.transform.Find ("X_Sensitivity/Slider").gameObject.GetComponent<Slider> ();
		YSensitivity = OptionsMenu.transform.Find ("Y_Sensitivity/Slider").gameObject.GetComponent<Slider> ();
	}

    void Start()
    {
//		print ("PauseUINetworking- Start");
//		lockCursorScript = FindObjectOfType<LockCursor> ();
//		lockCursorScript.LockAndHide ();
		pauseCanvasGroup = GetComponent<CanvasGroup>();
		lobbyManager = FindObjectOfType<LobbyManager> ();
		YesButton.onClick.AddListener (lobbyManager.GoBackButton);
		NoScreenActive ();
    }
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (!IsPaused) {
				IsPaused = true;
				ShowPauseScreen ();
				pauseCanvasGroup.alpha = 1;
				pauseCanvasGroup.interactable = true;
				pauseCanvasGroup.blocksRaycasts = true;
				GameUINetworking.LocalPlayer.GetComponent<TankControllerNetworking> ().enabled = false;
			} else {
				if (!InOptions) {
					IsPaused = false;
					NoScreenActive ();
					pauseCanvasGroup.alpha = 0;
					pauseCanvasGroup.interactable = false;
					pauseCanvasGroup.blocksRaycasts = false;
					GameUINetworking.LocalPlayer.GetComponent<TankControllerNetworking> ().enabled = true;
				} else {
					ShowPauseScreen ();
					InOptions = false;
				}
			}
		}
	}
    
    public void ShowPauseScreen()
    {
        PauseScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
        OptionsMenu.SetActive(false);
		TintBackground.SetActive(true);
		// show cursor
//		lockCursorScript.UnlockAndShow ();
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
		InOptions = true;
    }

    public void NoScreenActive()
    {
        PauseScreen.SetActive(false);
        MainMenuScreen.SetActive(false);
        OptionsMenu.SetActive(false);
		TintBackground.SetActive(false);
		//hide cursor
//		lockCursorScript.LockAndHide ();
    }
	public void SetSensitivitySliders()
	{
		XSensitivity.onValueChanged.RemoveAllListeners ();
		YSensitivity.onValueChanged.RemoveAllListeners ();

		XSensitivity.onValueChanged.AddListener (Sens_X_Adjust);
		YSensitivity.onValueChanged.AddListener (Sens_Y_Adjust);
	}
	void Sens_X_Adjust(float value)
	{
		GameUINetworking.LocalPlayer.GetComponent<TankCameraControlNetworking>().sensitivityX = value;
	}
	void Sens_Y_Adjust(float value)
	{
		GameUINetworking.LocalPlayer.GetComponent<TankCameraControlNetworking>().sensitivityY = value;
	}
}
