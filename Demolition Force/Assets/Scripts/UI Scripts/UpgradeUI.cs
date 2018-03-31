using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeUI : MonoBehaviour {

	public GameObject MainPanel;
	public GameObject TurretPanel;
	public GameObject ArmorPanel;
	public GameObject AmmoPanel;

	void Start () 
	{
		ShowMainPanel ();
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<LockCursor> ().UnlockAndShow ();
	}
	public void ShowTurretPanel()
	{
		MainPanel.SetActive (true);
		TurretPanel.SetActive (true);
		ArmorPanel.SetActive (false);
		AmmoPanel.SetActive (false);

	}
	public void ShowArmorPanel()
	{
		MainPanel.SetActive (true);
		TurretPanel.SetActive (false);
		ArmorPanel.SetActive (true);
		AmmoPanel.SetActive (false);

	}
	public void ShowAmmoPanel()
	{
		MainPanel.SetActive (true);
		TurretPanel.SetActive (false);
		ArmorPanel.SetActive (false);
		AmmoPanel.SetActive (true);

	}
	public void ShowMainPanel()
	{
		MainPanel.SetActive (true);
		TurretPanel.SetActive (false);
		ArmorPanel.SetActive (false);
		AmmoPanel.SetActive (false);

	}
	public void GotoOpenWorld()
	{
		SceneManager.LoadScene ("OpenWorldMap");
	}
}
