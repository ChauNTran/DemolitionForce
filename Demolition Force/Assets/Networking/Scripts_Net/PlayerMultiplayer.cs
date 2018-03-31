// chau tran
// purpose:		toggle on local controller scripts when play. toggle the scripts off when player is disable
// where to put: the multiplayer player tank

//Sam Update 3/28/17 2:22
//Add command function to change class. Not fully functional yet, just doing some scirpt work


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ToggleEvents: UnityEvent <bool>{}

public class PlayerMultiplayer : NetworkBehaviour {

	[SyncVar] public int playerId;
	[SyncVar] public string playerName;
	[SyncVar]public Transform spawnPoint;
	[SyncVar(hook="UpdateUIKillCount")] public int killCount;
	[SyncVar(hook ="UpdateUIDeathCount")] public int deathCount;
	[SyncVar] public bool prefabSwitched;
	public Vector3 originalSpawnPos;//SpawnPosition
	public GameObject[] vehicleClass;//Variables for class changing
	public GameUINetworking gameUIScipt;
	public PauseUINetworking pauseUIScipt;
	public GameObject playerNameUI;
	//Access to GUI to update with stats

	GameObject newPlayer;
	[HideInInspector]public VehicleClassChangeUI_Network changeClassUIScript;
	[SerializeField] ToggleEvents onToggleShared;
	[SerializeField] ToggleEvents onToggleLocal;
	[SerializeField] ToggleEvents onToggleRemote;
	[SerializeField] GameObject mainCamera;


	//test can be deleted
	bool updateRosterFlag;
	void Start()
	{

		if (isLocalPlayer) {//if is local player
			mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").gameObject;
			GameUINetworking.LocalPlayer = this.gameObject;
			foreach(MeshCollider meshCol in GetComponentsInChildren<MeshCollider>())
			{
				if (meshCol.gameObject != this.gameObject)
				{
					meshCol.gameObject.layer = 2;
				}
			}
			transform.Find ("Shield").gameObject.layer = 2;
			if (GameObject.FindGameObjectWithTag ("Game UI") != null) {
				gameUIScipt = GameObject.FindGameObjectWithTag ("Game UI").GetComponent<GameUINetworking> (); //get player game ui and set it on
			} 
			if (GameObject.FindGameObjectWithTag ("Class UI") != null) {
				changeClassUIScript = GameObject.FindGameObjectWithTag ("Class UI").GetComponent<VehicleClassChangeUI_Network> ();
			} 
			if (GameObject.FindGameObjectWithTag ("PauseUI") != null) {
				pauseUIScipt = GameObject.FindGameObjectWithTag ("PauseUI").GetComponent<PauseUINetworking> ();
			}
			if (!prefabSwitched) { //if this is the beginning of the game then set all this up
				pauseUIScipt.SetSensitivitySliders ();

				GetComponent<AudioSource>().Stop(); //turn off engine sound
				gameObject.transform.Find ("Camera").gameObject.GetComponent<AudioListener> ().enabled = true;
				//playerId = Random.Range (100000, 999999); //Create Player ID
				originalSpawnPos = transform.position; //Capture Player spawn Position
				CmdUpdateOriginalSpawnPos (); //send captured spawn position to server
				CmdUpdatePlayerId (playerId); //send set player id to server player
				DisablePlayer (); //so the player wont be seen at the beggining of the game on the UI 
			} 
			else //if this is not the beginning of the game then go ahead and do this
			{

				gameUIScipt.GetComponent<CanvasGroup> ().alpha = 1;//set player UI on
				gameObject.transform.Find ("Camera").gameObject.GetComponent<AudioListener> ().enabled = true;
				EnablePlayer ();
				CmdEnablePlayerOnReSpawn ();
				GetComponent<TankHealthNetworking>().UpdateHealthBar(GetComponent<TankHealthNetworking>().tankFullHealth);
			}
		}
		else //if is not local player then do this to non local players
		{
			if (!prefabSwitched) { //if this is the beginning of the game
				DisablePlayer ();
				gameObject.transform.Find ("Camera").gameObject.GetComponent<AudioListener> ().enabled = false;
				playerNameUI = gameObject.transform.Find ("PlayerNameTag").gameObject; //set ui off before play
				playerNameUI.SetActive (false);
				GetComponent<AudioSource>().Stop();//turn off engine sound

			} else { // if this is not the beginning of the game
				playerNameUI = gameObject.transform.Find ("PlayerNameTag").gameObject;
				gameObject.transform.Find ("Camera").gameObject.GetComponent<AudioListener> ().enabled = false;

				if (playerNameUI == null)
					return;
			
				playerNameUI.SetActive (true);
				playerNameUI.GetComponent<Text> ().text = "[" + playerName + "]";
			}
		}
	
	}
	[ServerCallback]
	void Update(){
		if (prefabSwitched) {
			if (!updateRosterFlag) {
				updateRosterFlag = true;
				StartCoroutine (UpdatePlayerRosterOnServer ()); //we had to put this in the update a second or two after for it to work
			}
		}
	}
	IEnumerator UpdatePlayerRosterOnServer()
	{
		if (isServer) {
			//yield return new WaitForSeconds (1f);
			GameControllerNetworking.Instance.FindPlayerByID ();//update player roster
			yield return null;
		}
	}
	public override void OnStartClient()//TEST TO UPDATE INFO TO GAME MANANGER ***
	{
		base.OnStartClient ();
//		if (!isServer) 
//		{
//			//PlayerMultiplayer pMultiplayer = GetComponent<PlayerMultiplayer> ();
//			if (playerId != null) 
//			{
//				GameControllerNetworking.allPlayers.Add (playerId);
//			}
//		}
	}

	public void DisablePlayer()
	{
		if (isLocalPlayer)
			mainCamera.GetComponent<Camera>().enabled = true;
		
		onToggleShared.Invoke (false);
		if (isLocalPlayer)
			onToggleLocal.Invoke (false);
		else
			onToggleRemote.Invoke (false);
	}
	void EnablePlayer()
	{
		onToggleShared.Invoke (true);
		if (isLocalPlayer)
			onToggleLocal.Invoke (true);
		else
			onToggleRemote.Invoke (true);
	}
	public void ToggleRenderer(bool state)
	{
		foreach (Renderer rend in GetComponentsInChildren<Renderer>()) {
			rend.enabled = state;
		}
	}
	public void ToggleColliders(bool state)
	{
		foreach (Collider c in GetComponentsInChildren<Collider>()) 
		{
			c.enabled = state;
		}
	}
//	void UpdateName(string pName) NOT REALLY NEEDED
//	{
//		//if(pName != null)
//		//playerName = pName;
//	}
	void UpdateUIKillCount(int kill)
	{
		
		if (gameUIScipt != null) 
		{
			killCount = kill; //update kill count
			gameUIScipt.KillCountRefresh(killCount);
		}
	}
	void UpdateUIDeathCount(int deaths)
	{
		if (gameUIScipt != null) 
		{
			deathCount = deaths; //update death count
			gameUIScipt.DeathCountRefresh(deathCount);
		}
	}

	[Command]
	void CmdUpdateOriginalSpawnPos()
	{
		originalSpawnPos = transform.position;// 
	}
	[Command]
	void CmdUpdatePlayerId(int playerIdNumber)
	{
		playerId = playerIdNumber;
	}
	public void Die()
	{
		DisablePlayer ();
		Invoke ("Respawn", 2f);
	}
	// called from tank health script
	public void AddDeathCount()
	{
		if (!isServer) {
			return;
		}
		deathCount += 1;
	}
	// called from tank health script
	public void AddKillCount()
	{
		if (!isServer) {
			return;
		}
		killCount += 1;
	}
	void ShowKillOnKillPanel()
	{

	}
	void Respawn()
	{
		if (isLocalPlayer)
		{
			Transform spawn = NetworkManager.singleton.GetStartPosition ();
			transform.position = spawn.position;
			transform.rotation = spawn.rotation;
		}

		EnablePlayer ();
	}

	//COMMAND FUNCTION TO CALL PLAYER PREFAB TO CHANGE CLASSES- NOTE, THIS WILL BE CALLED FROM CHANGE CLASS UI PANEL
	[Command]
	public void CmdChangeVehicleClass(int classNum,NetworkInstanceId netId)
	{
		GameObject oldPlayer = NetworkServer.FindLocalObject (netId);								//Grabs this local player on server

		var conn = oldPlayer.GetComponent<NetworkIdentity> ().connectionToClient;					//get local player client connection
		//NetworkInstanceId netID = oldPlayer.GetComponent<NetworkIdentity>().netId;
		PlayerMultiplayer playerStats = oldPlayer.GetComponent<PlayerMultiplayer>();				//get local player prefab player setup information script

		//RPC DEACTIVATE PLAYER*** already happens on death

		//CHANGE CLASS ACCORDING TO INT PASSED FROM UI CHANGE CLASS PANEL BUTTON
		//Spawn new player prefab
		if (classNum == 1) {
			newPlayer = Instantiate<GameObject> (vehicleClass [0], playerStats.originalSpawnPos, playerStats.transform.rotation);
		} else if (classNum == 2) {
			newPlayer = Instantiate<GameObject> (vehicleClass [1], playerStats.originalSpawnPos, playerStats.transform.rotation);
		} else if (classNum == 3) {
			newPlayer = Instantiate<GameObject> (vehicleClass [2], playerStats.originalSpawnPos, playerStats.transform.rotation);
		} else if (classNum == 4) {
			newPlayer = Instantiate<GameObject> (vehicleClass [3], playerStats.originalSpawnPos, playerStats.transform.rotation);
		} 
		else {

		}
		//once both prefabs are present in the game, pass stats
		//CROSS OVER STATS
		newPlayer.GetComponent<PlayerMultiplayer>().prefabSwitched = true;
		newPlayer.GetComponent<PlayerMultiplayer> ().playerName = playerStats.playerName;
		newPlayer.GetComponent<PlayerMultiplayer> ().killCount = playerStats.killCount;
		newPlayer.GetComponent<PlayerMultiplayer> ().deathCount = playerStats.deathCount;
		newPlayer.GetComponent<PlayerMultiplayer> ().originalSpawnPos = playerStats.originalSpawnPos;
		newPlayer.GetComponent<PlayerMultiplayer> ().playerId = playerStats.playerId;

		NetworkServer.Spawn (newPlayer);

		//RPC DESTORY OLD PLAYER PREFAB

		NetworkServer.ReplacePlayerForConnection (conn, newPlayer, 0);

		//NetworkServer.Destroy (oldPlayer);
		RpcDestroyOldPlayerPrefab();

	}
		
	[ClientRpc]
	void RpcDestroyOldPlayerPrefab()
	{
		NetworkServer.Destroy (GetComponent<NetworkIdentity> ().gameObject);
	}
	[Command]
	public void CmdEnablePlayerOnReSpawn()
	{
		EnablePlayer ();
	}
}
