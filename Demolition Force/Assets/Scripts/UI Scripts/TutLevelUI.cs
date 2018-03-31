using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutLevelUI : MonoBehaviour 
{
	
	public Text TutorialText;
	public float waitTime = 2;
	private int stage;

	public GameObject barrel;
	public GameObject normalBarrel;
	public GameObject AI;
	public Transform BarrelSpawn1;
	public Transform BarrelSpawn2;
	public Transform BarrelSpawn3;
	public Transform TankSpawn1;
	public Transform TankSpawn2;

	public GameObject CheckPoint1;
	public GameObject CheckPoint2;
//	public Transform TankSpawn3;
	private GameObject tutBarrel;
	private GameObject AI1;
	private GameObject AI2;
	private bool hasSpawened = false;
	private bool hasSpawenedAI = false;
	private bool WPressed = false;
	private bool APressed = false;
	private bool SPressed = false;
	private bool DPressed = false;

//	GameObject clone;

	public Animator textBoxAnimator;
	public Image textBox;
	void Start () 
	{
		textBox.enabled = false;
		Invoke ("StartTutorial", waitTime);
	}
	void StartTutorial()
	{
		stage = 1;
		textBox.enabled = true;
		textBoxAnimator.Play ("Open Text");
	}

	void Update ()
	{
		switch (stage) {
		case 1:	// introduction
			TutorialText.text = ("Welcome to Demolition Force Tutorial. Our first lesson is Navigating. Press 'N' to continue.");
			PressNToContinue ();
			break;
		case 2:	// navigating tutorial
			TutorialText.text = ("To move around, use W,A,S,D keys. To handbrake press Spacebar. Now move to that the check point.");
			CheckPoint1.SetActive (true);
			break;
		case 3:
			TutorialText.text = ("Great! Now let's try again and move to that check point.");
			CheckPoint2.SetActive (true);
			break;
		case 4:
			TutorialText.text = ("Very Nice! Our next lesson is Attacking. Press 'N' to countinue.");
			PressNToContinue ();
			break;
		case 5:
			TutorialText.text = ("You have two ways to attack. Left Mouse to use the turret or Right Mouse to use the machine gun. Now give them a try and shoot those barrels");
			if (hasSpawened != true)
				SpawnBarrel ();
			else if (tutBarrel == null)
				Continue ();
			break;
		case 6:
//			if (hasSpawenedAI != true)
				SpawnAI ();
			break;
		case 7:
			if (AI1== null && AI2 == null) 
				Continue ();
			
			break;
		case 8:
			TutorialText.text = ("Great job! You have complete the turtorial");
			Invoke ("NextScene", 6f);
			break;
		default:
			break;
		}
	}

	public void PressNToContinue()
	{
		if (Input.GetKeyDown (KeyCode.N)) {
			stage++;
			textBoxAnimator.Play ("Renew Text");
		}
	}
	public void Continue()
	{
		stage++;
		textBoxAnimator.Play ("Renew Text");
	}

//--------------------------------------------------------Methods
	private void NavigatingChecker()
	{
		if (Input.GetKeyDown (KeyCode.W)) 
		{
			WPressed = true;
		}
		if (Input.GetKeyDown (KeyCode.A)) 
		{
			APressed = true;
		}
		if (Input.GetKeyDown (KeyCode.S)) 
		{
			SPressed = true;
		}
		if (Input.GetKeyDown (KeyCode.D)) 
		{
			DPressed = true;
		}

	}
	private void MouseCheck()
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			stage++;
			textBoxAnimator.Play ("Renew Text");
		}
	}

	public void SpawnBarrel()
	{
		tutBarrel = Instantiate(barrel, BarrelSpawn1.position, Quaternion.identity);
		hasSpawened = true;
	}

	private void SpawnAI()
	{
		TutorialText.text = (" Ah heck looks like we have some guests. Take them out!");

		AI1 = Instantiate (AI, TankSpawn1.position, Quaternion.identity);
		AI2 = Instantiate (AI, TankSpawn2.position, Quaternion.identity);

		Instantiate (normalBarrel, BarrelSpawn2.position, Quaternion.identity);
		Instantiate (normalBarrel, BarrelSpawn3.position, Quaternion.identity);
//		hasSpawenedAI = true;
		stage++;
	}
	void NextScene()
	{
		SceneManager.LoadScene ("OpenWorldMap");
	}
}



