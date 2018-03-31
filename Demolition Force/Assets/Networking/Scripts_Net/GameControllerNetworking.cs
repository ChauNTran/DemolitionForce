// chau tran
// purpose		control game play. win-lose condition
// where to put GameController object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public enum MultiplayerGameType 
{
	KingOfTheHill,
	Deathmatch,
	TeamDeathmatch,
	BattleArena,
	HavokHill,
	CaptureTheFlag
}
public enum TimePlayLimit
{
	FiveMinutes, TenMinutes, FifteenMinutes, TwentyMinutes, noLimit
}

public class GameControllerNetworking : NetworkBehaviour {
	
	static GameControllerNetworking instance;
	public static  int m_selectedMap = 0;
	public List<GameObject> maps;

	//contorller variables
	[Header ("CountDown Settings")]
	[SerializeField] int countDownTime;
	[SerializeField] AudioClip countDownSfx;
	[SerializeField] AudioClip goSfx;

	public Text messageText;
	public Text subMessageText;
	public Text countDownText;

	[Header ("UI HookUps")]
	public GameObject vehicleUI;
	public GameObject gameUI;
	public GameObject timerUI;


	[Header ("Gameplay Settings")]
	public MultiplayerGameType gameType;
	public TimePlayLimit timePlayLimit;
	public int killCountsToWin;
	public float RespawnTime;

	//public static List<NetworkIdentity> allPlayers = new List<NetworkIdentity>(); 	//list to hold stats of all players on server to access and pass
	//public List<Text> playerNameTexts;													//list of all players names
	//public List<Text> playerKillScoreTexts;												//list of all players kill count
	public static List<int> allPlayers = new List<int>();
	public GameObject[] players; //dynamic list of active player prefabs in the scene

	bool countTime = false; // serialized to test.
	bool team1Cap = false;
	bool team2Cap = false;
	float playTime;
	float timeLimit;
	bool draw = false;

	[SyncVar]public bool isGameOver;

	LobbyManager lobbyManager = LobbyManager.s_Singleton;
	PlayerMultiplayer playerWinner;
	//
	[SyncVar]int killCounter = 0;


	//[SerializeField] float team1Timer;
	//[SerializeField] float team2Timer;

	public static GameControllerNetworking Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<GameControllerNetworking> ();
				if (instance == null)
				{
					instance = new GameObject().AddComponent<GameControllerNetworking>();
				}
			}
			return instance;
		}
	}

	[ServerCallback]
	void Start()
	{
		print ("Start");

		TimeLimitCondition ();
		StartCoroutine("GameLoopRoutine"); //begin game loop
	}

	//IENUMERATOR GAME LOOP TO LOOP THROUGH DIFFERENT PARTS OF GAME PLAY
	IEnumerator GameLoopRoutine()
	{
		print ("start GameLoopRoutine");
		if (lobbyManager != null) { //wait until all player join
			while (allPlayers.Count < lobbyManager._playerNumber) {
				yield return null;
			}
			//yield return StartCoroutine ("SetupMaps"); //------------MAP STUFF ----------
		players = new GameObject[allPlayers.Count]; //set players list to size of # of players
			//yield return new WaitForSeconds (2f);
			yield return StartCoroutine ("StartGameRoutine");
			yield return StartCoroutine ("MidGameRoutine");
			yield return StartCoroutine ("EndGameRoutine");
		} 
//		else 
//		{
//			Debug.LogWarning ("====== GAMEMANAGER WARNING ===== Launch game from lobby");
//		}

	}
	IEnumerator StartGameRoutine()
	{
		//disable players ui
		//disable players class ui
		DeactivatePlayerUI();
		//set game timer if there is one to true
//		if(timePlayLimit != TimePlayLimit.noLimit){
//
//		}

		//do a count down before match begins
		for (int i = countDownTime; i > -1; i--) 
		{
			yield return new WaitForSeconds (1f);
			if (i != 0) {
				RpcPlaySFX (0);
				UpdateMessages ("Free for all death match", "Match begins in...", i.ToString ());
			} else {
				RpcPlaySFX(1);
				UpdateMessages ("Free for all death match", "Match begins in...", "GO!");
			}
		}

		//then release
		UpdateMessages ("", "", ""); //TURN message ui down
		FindPlayerByID (); //find player id so we can update score board
		SetScoreBoard (); //update score board for beginning game
		ActivateUI (); // activate class change ui
		GetComponent<GameTimer_Network>().startGameTime = true; //start timer

		yield return null;

	}
	IEnumerator MidGameRoutine()
	{
		while (isGameOver == false ) //while gameover is false loop
		{
			CheckScores ();//checks score of players and if score is == to score to win then end game

			if (gameObject.GetComponent<GameTimer_Network> ().timesUp) //if clock hits 0:00
			{
						isGameOver = true;
			}


			yield return null;

		}
	}
	IEnumerator EndGameRoutine()
	{
		print ("EndGameRoutine");
		CheckScoresTimesUp ();

		//deactivate everyones ui
		DeactivatePlayerUI();
		DeactivatePlayers ();

		if (draw) {
			UpdateMessages ("DRAW", "", "");
		}
		else if (!draw) {
			UpdateMessages (playerWinner.playerName + " WINS", "", "");
		}
		yield return new WaitForSeconds (5f);

		LobbyManager.s_Singleton._playerNumber = 0;
		allPlayers.Clear ();
		StopAllCoroutines ();
		FindObjectOfType<NetworkLobbyManager> ().SendReturnToLobby ();
		yield return null;
	}
	//---------------------	MAP STUFF -------------
	IEnumerator SetupMaps()
	{
		yield return null;
		RpcSetupMap (GameControllerNetworking.m_selectedMap);
	}
	[ClientRpc]
	void RpcSetupMap(int mapIdx)
	{
		SelectMap (mapIdx);
	}
	public void SelectMap(int selectedIndex)
	{
		if (maps.Count <= 0) {
			Debug.LogWarning ("MAPSELECTOR ERROR! NO MAPS SPECIFIED");
			return;
		}
		selectedIndex = Mathf.Clamp (selectedIndex, 0, maps.Count - 1);

		foreach (GameObject m in maps) {
			m.SetActive (maps [selectedIndex] == m);
		}
		Debug.Log ("Setting Map to " + maps [selectedIndex].name);
	}

	[Server]
	public void FindPlayerByID()//used to grab players by their assigned ID...ID is assigned in hook script.
	{
		print ("finding player");
		print (allPlayers.Count);
		//players = new GameObject[allPlayers.Count];
		for (int i = 0; i < allPlayers.Count; i++) 
		{
			//print ( i + "ID NUMBER " + allPlayers [i]);
			//print(allPlayers[i]);
			players[i] = CatchPlayerIDObject (allPlayers [i]);
			print ("GameObject" + players [i].gameObject.name);
		
		}

	}
	[Server]
	public GameObject CatchPlayerIDObject(int playerID)
	{
		GameObject[] _players = GameObject.FindGameObjectsWithTag ("Player");//get all player in array
		//print(playerID);
		//print("caught players " + _players[0] + _players[1]);
		for (int i = 0; i < allPlayers.Count; i++) 
		{

			if (_players [i].GetComponent<PlayerMultiplayer> ().playerId == playerID) 
			{
				return _players [i];
			}
		}
		return null;


	}




	[ServerCallback]
	void Update()
	{
		if (countTime)
			CountingTime ();

		switch (gameType) {
		case MultiplayerGameType.KingOfTheHill:
			//KingOfTheHillController ();
			break;
		case MultiplayerGameType.Deathmatch:
			DeathmatchController ();
			break;
		case MultiplayerGameType.TeamDeathmatch:
			TeamDeathmatchController ();
			break;
		case MultiplayerGameType.BattleArena:
			BattleArenaController ();
			break;
		case MultiplayerGameType.HavokHill:
			HavokHillController ();
			break;
		case MultiplayerGameType.CaptureTheFlag:
			CaptureTheFlagController ();
			break;
		}
	}

	/*void KingOfTheHillController()
	{
		if (team1Cap) {
			team1Timer += Time.deltaTime;
		} else if (team2Cap) {
			team2Timer += Time.deltaTime;
		}
	}*/
	void DeathmatchController()
	{

	}
	void TeamDeathmatchController()
	{

	}
	void BattleArenaController()
	{

	}
	void HavokHillController()
	{

	}
	void CaptureTheFlagController()
	{

	}
	// other functions for king of the hill
	public void Team1CapThePoint(bool value)
	{
		team1Cap = value;
		team2Cap = !value;
	}
	public void Team2CapThePoint(bool value)
	{
		team2Cap = value;
		team1Cap = !value;
	}
	void CountingTime()
	{
		playTime += Time.deltaTime;
		if (playTime >= timeLimit)
		{
			print ("GAME OVER!");
		}
	}
	void TimeLimitCondition()
	{
		switch (timePlayLimit) {
		case TimePlayLimit.noLimit:
			countTime = false;
			break;
		case TimePlayLimit.TenMinutes:
			countTime = true;
			timeLimit = 10f * 60f;
			break;
		case TimePlayLimit.FifteenMinutes:
			timeLimit = 15f * 60f;
			break;
		case TimePlayLimit.TwentyMinutes:
			timeLimit = 20f * 60f;
			break;
		}
	}

	//SENDS SERVER RELATED PLAYER STAT DATA TO CLIENTS THROUGH RPC
	public void UpdateScoreBoard()
	{

		if (isServer) 
		{
			string[] names = new string[allPlayers.Count];
			int[] killScores = new int[allPlayers.Count];
			int[] deathCount = new int[allPlayers.Count];
			int playerCount = allPlayers.Count;

			for (int i = 0; i < allPlayers.Count; i++)
			{
				names [i] = players [i].GetComponent<PlayerMultiplayer> ().playerName;
				killScores [i] = players [i].GetComponent<PlayerMultiplayer> ().killCount;
				deathCount [i] = players [i].GetComponent<PlayerMultiplayer> ().deathCount;
			}

			RpcSetScoreBoard (names, killScores, deathCount, playerCount);
		}
	}
	[ClientRpc]
	public void RpcUpdateScoreBoard(string[] playerNames,int[] killScores,int[] deathCount, int playerCount)
	{
		print(killScores);
		for (int i = 0; i < playerCount; i++) 
		{
			gameObject.GetComponent<ScoreController> ().SetScore (playerNames [i], "kills", killScores[i]);
			gameObject.GetComponent<ScoreController> ().SetScore (playerNames [i], "deaths", deathCount[i]);
		}
	}
	public void SetScoreBoard()//updates score board at the beginning of the game..clean slate
	{
		if (isServer) 
		{
			string[] names = new string[allPlayers.Count];
			int[] killScores = new int[allPlayers.Count];
			int[] deathCount = new int[allPlayers.Count];
			int playerCount = allPlayers.Count;

			for (int i = 0; i < allPlayers.Count; i++)
			{
				names [i] = players [i].GetComponent<PlayerMultiplayer> ().playerName;
				killScores [i] = players [i].GetComponent<PlayerMultiplayer> ().killCount;
				deathCount [i] = players [i].GetComponent<PlayerMultiplayer> ().deathCount;
			}

			RpcSetScoreBoard (names, killScores, deathCount, playerCount);
		}
	}
	[ClientRpc]
	public void RpcSetScoreBoard(string[] playerNames,int[] killScores,int[] deathCount, int playerCount)
	{
		for (int i = 0; i < playerCount; i++) 
		{
			gameObject.GetComponent<ScoreController> ().SetScore (playerNames [i], "kills", killScores[i]);
			gameObject.GetComponent<ScoreController> ().SetScore (playerNames [i], "deaths", deathCount[i]);
		}
	}
	void CheckScores()
	{
		playerWinner = GetWinnerDeathMatch ();
		if (playerWinner != null)
		{
			isGameOver = true;
		}
	}
	void CheckScoresTimesUp()
	{
		playerWinner = GetWinnerTimesUp ();
		if (playerWinner != null)
		{
			isGameOver = true;
		}
	}
	public PlayerMultiplayer GetWinnerDeathMatch() //checks to see when total kills is reached
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		for (int i = 0; i < lobbyManager._playerNumber; i++) {
			if (players [i].GetComponent<PlayerMultiplayer> ().killCount >= killCountsToWin) 
			{
				return players[i].GetComponent<PlayerMultiplayer>();
			}
		}
		return null;
	}
	public PlayerMultiplayer GetWinnerTimesUp() // checks to see when time is up and see who has most kills
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player"); // grab all active players in match
		int highestScore = 0;
		PlayerMultiplayer highestPlayer = null;
		PlayerMultiplayer tiedPlayer = null;
		bool flag = false; //used to check whether we tie or not

		for (int i = 0; i < lobbyManager._playerNumber; i++) //loop through all player in match
		{
			if (players [i].GetComponent<PlayerMultiplayer> ().killCount > highestScore) { // if this player is higher than set hgiest score
				highestPlayer = players [i].GetComponent <PlayerMultiplayer> (); // set this player as highest player 
				highestScore = players [i].GetComponent<PlayerMultiplayer> ().killCount; //set this players score as highest score
				flag = false;
			} 
			else if (players [i].GetComponent<PlayerMultiplayer> ().killCount == highestScore) //if there is a draw
			{
				flag = true;
				tiedPlayer = players [i].GetComponent<PlayerMultiplayer> ();
			}
		}
		//once loop is finished if, there is a tie and ther is a tied player, check tie breaker.. this check deaths count to see who has least deaths if they tied for most kills
		if (flag && (tiedPlayer != null)) 
		{
			if (tiedPlayer.deathCount > highestPlayer.deathCount) {
				flag = false;//no draw, highest player is still highest
			} 
			else if (tiedPlayer.deathCount < highestPlayer.deathCount) 
			{
				flag = false; // no draw
				highestPlayer = tiedPlayer; //set tied second player to the new highest player
			}
			else if (tiedPlayer.deathCount == highestPlayer.deathCount) 
			{
				flag = true; //draw is true
			}
		}

		//once loop is finished
		if (flag) //after the loop check to see returns
		{
			draw = true;
		} 
		else if (!flag) 
		{
			return highestPlayer.GetComponent<PlayerMultiplayer>();
		}
		return null;
	}

	public void DeactivatePlayerUI()
	{
		RpcDeactivatePlayerUI ();
	}
	[ClientRpc]
	void RpcDeactivatePlayerUI()
	{
		gameUI.GetComponent<CanvasGroup> ().alpha = 0;
		vehicleUI.GetComponent<CanvasGroup> ().alpha = 0f;
		vehicleUI.GetComponent<CanvasGroup> ().interactable = false;
		vehicleUI.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		timerUI.GetComponent<CanvasGroup> ().alpha = 0f;
	}
	public void ActivateUI()
	{
		RpcActivateUI ();
	}
	[ClientRpc]
	void RpcActivateUI()
	{
		vehicleUI.GetComponent<CanvasGroup> ().alpha = 1f;
		vehicleUI.GetComponent<CanvasGroup> ().interactable = true;
		vehicleUI.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		timerUI.GetComponent<CanvasGroup> ().alpha = 1f;
	}
	[ClientRpc] //Plays GameController SFX
	void RpcPlaySFX(int i) //cant transfer sound clips over rpcs so use an int
	{
		if(i == 0)
		AudioManager.instance.PlayerSound2D (countDownSfx);
		else if(i == 1)
		AudioManager.instance.PlayerSound2D (goSfx);

	}
	[ClientRpc]
	void RpcUpdateMessages(string msg, string subMsg, string cdMsg)
	{
		if (messageText != null && subMessageText != null && countDownText != null) 
		{
			messageText.gameObject.SetActive (true);
			messageText.text = msg;

			subMessageText.gameObject.SetActive (true);
			subMessageText.text = subMsg;

			countDownText.gameObject.SetActive (true);
			countDownText.text = cdMsg;
		}
	}

	public void UpdateMessages(string msg, string subMsg, string cdMsg)
	{
		RpcUpdateMessages (msg, subMsg, cdMsg);
	}
	//When a message is geared toward one player only
	void LocalUpdateMessages(string msg, string subMsg, string cdMsg)
	{
		if (messageText != null && subMessageText != null && countDownText != null) 
		{
			messageText.gameObject.SetActive (true);
			messageText.text = msg;

			subMessageText.gameObject.SetActive (true);
			subMessageText.text = subMsg;

			countDownText.gameObject.SetActive (true);
			countDownText.text = cdMsg;
		}
	}
	public void DeactivatePlayers()
	{
		RpcDeactivatePlayers ();
	}
	[ClientRpc]
	void RpcDeactivatePlayers()
	{
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) 
		{
			if (obj.GetComponent<NetworkIdentity> ().isLocalPlayer) 
			{
				obj.GetComponent<PlayerMultiplayer> ().DisablePlayer();
			}
		}

	}
	//MATCH BEGINS FUNCTION


	//UPDATE PLAYER KILLS
	[ClientRpc]
	public void RpcShowKillUI(string whokillplayer, string whoplayerkill)
	{
		GameUINetworking gameUIScript = gameUI.GetComponent<GameUINetworking> ();

		killCounter += 1;
		if (killCounter == 1) {
			
			gameUIScript.killTexts [0].text = whokillplayer + " Destroys " + whoplayerkill;
		} else if (killCounter == 2) {
			gameUIScript.killTexts [1].text = whokillplayer + " Destroys " + whoplayerkill;
		} else if (killCounter == 3) {
			gameUIScript.killTexts [2].text = whokillplayer + " Destroys " + whoplayerkill;
		} else if (killCounter == 4) {
			gameUIScript.killTexts [3].text = whokillplayer + " Destroys " + whoplayerkill;
		} else if (killCounter == 5) {
			gameUIScript.killTexts [4].text = whokillplayer + " Destroys " + whoplayerkill;
		} else if (killCounter > 5) {
			for (int i = 0; i < gameUIScript.killTexts.Length ; i++)
			{
				if (i == gameUIScript.killTexts.Length - 1)
					gameUIScript.killTexts[i].text = whokillplayer + " Destroys " + whoplayerkill;
				else
					gameUIScript.killTexts [i].text = gameUIScript.killTexts [i + 1].text;
			}
		}

	} 
	//CALLED FROM PLAYER ON DEATH IN HEALTH SCRIPT
	public IEnumerator RespawnCountDownIE(int respawnTime,string whoKilledMeName)
	{	
		
			for (int i = respawnTime; i > -1; i--) { //loop through respawn time
				yield return new WaitForSeconds (1f);
				if (i != 0) {
					//AudioManager.instance.PlayerSound2D (countDownSfx);
					LocalUpdateMessages ( whoKilledMeName, "Respawn in...", i.ToString ());
				} else {
					AudioManager.instance.PlayerSound2D (goSfx);
					LocalUpdateMessages ("", "", "");
				}
			}
	}

}
