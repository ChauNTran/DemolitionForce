using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour {

	
	public void ChauTranPortfolio()
	{
		Application.OpenURL("http://www.chautrangames.com");
	}
	public void RussellPortfolio()
	{
		Application.OpenURL("http://www.everydayimrusselin.com/");
	}
	public void StevePortfolio()
	{
		Application.OpenURL("http://stevenstores07.wixsite.com/3d-artist/");
	}
	public void DylanPortfolio()
	{
		Application.OpenURL("http://www.dylansemititsky.com/");
	}
	public void SamPortfolio()
	{
		Application.OpenURL("http://samsil187.wixsite.com/gamedev");
	}
	public void BacktoMainMenu()
	{
		SceneManager.LoadScene ("Main Menu");
	}
}
