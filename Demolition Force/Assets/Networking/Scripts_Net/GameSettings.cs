// chau tran
// purpose:			get input from the player to set up the game
// where to put:	LobbyPanel game object

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;

public class GameSettings : MonoBehaviour {

	public Dropdown TimeLimitDropdown;
	public Dropdown GameTypeDropdown;
	public Dropdown MapDropdown;
	public Dropdown MaxKillDropdown;
	public GameObject MaxKillTitle;

//	[SyncVar(hook = "UpdateMap")]public int MapInt;
//	[SyncVar(hook = "UpdateGameType")]public int GameTypeInt;
//	[SyncVar(hook = "UpdateTimeLimit")]public int TimeLimtInt;
//
//	public int MapInt;
//	public int GameTypeInt;
//	public int TimeLimtInt;

	void Reset()
	{
		TimeLimitDropdown = GameObject.Find ("Playtime_Dropdown").GetComponent<Dropdown>();
		GameTypeDropdown =  GameObject.Find ("GameType_Dropdown").GetComponent<Dropdown>();
		MapDropdown =  GameObject.Find ("Map_Dropdown").GetComponent<Dropdown>();
		MaxKillDropdown = GameObject.Find ("Max_Kill_Dropdown").GetComponent<Dropdown> ();
		MaxKillTitle = GameObject.Find ("MaxKillTitle");
	}
		
	public void ShowClientTexts()
	{
		TimeLimitDropdown.interactable = false;
		GameTypeDropdown.interactable = false;
		MapDropdown.interactable = false;
		MaxKillDropdown.interactable = false;
	}	
	public void ShowHostDropdowns()
	{
		TimeLimitDropdown.interactable = true;
		GameTypeDropdown.interactable = true;
		MapDropdown.interactable = true;
		MaxKillDropdown.interactable = true;
	}	

	/// <summary>
	/// call from the drop down menu in the server
	/// </summary>
	public void Playtime_Update ()
	{
//		CmdPlaytime_Update ();

	}
	public void GameType_Update()
	{
//		CmdGameType_Update ();
	}
	public void Map_Update()
	{
		switch (MapDropdown.value) {
		case 0:
			GetComponentInParent<LobbyManager>().playScene = "Map_multiplayer_canyon";
			break;
		case 1:
			GetComponentInParent<LobbyManager>().playScene = "MultyplayerRoad";
			break;
		case 2:
			GetComponentInParent<LobbyManager>().playScene = "MultyplayerSwamp2";
			break;
		case 3:
			GetComponentInParent<LobbyManager>().playScene = "MultyplayerTut";
			break;
		case 4:
			GetComponentInParent<LobbyManager>().playScene = "Mt.DOOM";
			break;
		}
	}
//	[Command]
//	public void CmdPlaytime_Update()
//	{
//		TimeLimtInt = TimeLimitDropdown.value;
//		RpcUpdateTimeLimit (TimeLimtInt);
//	}
//	[Command]
//	public void CmdGameType_Update()
//	{
//		GameTypeInt = GameTypeDropdown.value;
//		switch (GameTypeInt) {
//		case 0:
//			MaxKillDropdown.gameObject.SetActive(true);
//			MaxKillTitle.SetActive(true);
//			break;
//		case 1:
//			MaxKillDropdown.gameObject.SetActive(false);
//			MaxKillTitle.SetActive(false);
//			break;
//		}
//
//		RpcUpdateGameType (GameTypeInt);
//	}
//	[Command]
//	public void CmdMap_Update()
//	{
//		MapInt = MapDropdown.value;
//		switch (MapInt) {
//		case 0:
//			GetComponentInParent<LobbyManager>().playScene = "Map_multiplayer_canyon";
//			break;
//		case 1:
//			GetComponentInParent<LobbyManager>().playScene = "MultyplayerRoad";
//			break;
//		case 2:
//			GetComponentInParent<LobbyManager>().playScene = "MultyplayerSwamp2";
//			break;
//		case 3:
//			GetComponentInParent<LobbyManager>().playScene = "MultyplayerTut";
//			break;
//		case 4:
//			GetComponentInParent<LobbyManager>().playScene = "Mt.DOOM";
//			break;
//		}
//
//		RpcUpdateMap (MapInt);
	}

//	void UpdateMap(int newInt)
//	{
//		MapInt = newInt;
//		MapDropdown.value = MapInt;
//	}
//	void UpdateGameType(int newInt)
//	{
//		GameTypeInt = newInt;
//		GameTypeDropdown.value = GameTypeInt;
//	}
//	void UpdateTimeLimit(int newInt)
//	{
//		TimeLimtInt = newInt;
//		TimeLimitDropdown.value = TimeLimtInt;
//	}
	/// <summary>
	/// to all clients
	/// </summary>
//	[RPC]
//	void RpcUpdateMap(int newInt)
//	{
//		print("RPC update map");
//		MapDropdown.value = newInt;
//	}
//	[RPC]
//	void RpcUpdateTimeLimit(int newInt)
//	{
//		print("RPC update time limit");
//		TimeLimitDropdown.value = newInt;
//
//	}
//	[RPC]
//	void RpcUpdateGameType(int newInt)
//	{
//		print("RPC update game type");
//		GameTypeDropdown.value = newInt;
//	}

