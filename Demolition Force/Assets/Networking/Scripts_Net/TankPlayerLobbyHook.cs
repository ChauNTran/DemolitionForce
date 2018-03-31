// chau tran
// purpose:			lobby hook script. sync player information from the lobby to the game
// where to put:	lobby manager game object

//Updates:
//Sam April 2nd '17:: Adding aspect to add player object playermananger script -
// - to gameMananger script so the server has a list of all players

using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class TankPlayerLobbyHook : LobbyHook {

	public override void OnLobbyServerSceneLoadedForPlayer (NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
	{
		LobbyPlayer lPlayer = lobbyPlayer.GetComponent<LobbyPlayer> ();
		PlayerMultiplayer gPlayer = gamePlayer.GetComponent<PlayerMultiplayer> ();
		// set name
		gPlayer.playerName = lPlayer.playerName;

		int playerId = Random.Range (100000, 999999); //Create Player ID

		gamePlayer.GetComponent<PlayerMultiplayer> ().playerId = playerId; //ADD PLAYER TO GAME TO COUNT PLAYERS IN MATCH
		if (playerId != null) 
		{
			GameControllerNetworking.allPlayers.Add (playerId);
		}
		GameSettings gameSettings;
		gameSettings = FindObjectOfType<GameSettings> ();
		print (gameSettings);
		if (lPlayer.isServer && lPlayer.isLocalPlayer) {
			GameControllerNetworking.m_selectedMap = lPlayer.playerMapSelection;
			Debug.Log ("NETWORK LOBBY HOOK SELECTING MAP " + lPlayer.playerMapSelection + "for player " + lPlayer.playerName);
		}

		if (gameSettings != null) { //BECAUSE ITS NOT BALE TO REFERENCE IT BUG ***
			switch (gameSettings.TimeLimitDropdown.value) {
			case 0:
				GameControllerNetworking.Instance.timePlayLimit = TimePlayLimit.TenMinutes;
				break;
			case 1:
				GameControllerNetworking.Instance.timePlayLimit = TimePlayLimit.FifteenMinutes;
				break;
			case 2:
				GameControllerNetworking.Instance.timePlayLimit = TimePlayLimit.TwentyMinutes;
				break;
			case 3:
				GameControllerNetworking.Instance.timePlayLimit = TimePlayLimit.noLimit;
				break;
			}
		} else {
			GameControllerNetworking.Instance.timePlayLimit = TimePlayLimit.TenMinutes;
		}

		if (gameSettings != null) {
			switch (gameSettings.GameTypeDropdown.value) {
			case 0:
				GameControllerNetworking.Instance.gameType = MultiplayerGameType.Deathmatch;
				switch (gameSettings.MaxKillDropdown.value) {
				case 0:
					GameControllerNetworking.Instance.killCountsToWin = 5;
					break;
				case 1:
					GameControllerNetworking.Instance.killCountsToWin = 10;
					break;
				case 2:
					GameControllerNetworking.Instance.killCountsToWin = 15;
					break;
				case 3:
					GameControllerNetworking.Instance.killCountsToWin = 20;
					break;
				}
				break;
			case 1:
				GameControllerNetworking.Instance.gameType = MultiplayerGameType.CaptureTheFlag;
				break;
			}
		} else {
			GameControllerNetworking.Instance.killCountsToWin = 7;
		}

	}
}
