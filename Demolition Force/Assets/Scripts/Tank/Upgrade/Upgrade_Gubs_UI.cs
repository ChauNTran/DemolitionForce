using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Upgrade_Gubs_UI : MonoBehaviour {

	Text GubNumber;

	void Awake()
	{
		GubNumber = transform.Find ("Gubs_text/Gubs_number").GetComponent<Text>();
	}
	void Start ()
	{
		RefreshGubText ();
	}

	public void RefreshGubText ()
	{
		GubNumber.text = PlayerPrefsManager.GetGubs ().ToString ();
	}
}
