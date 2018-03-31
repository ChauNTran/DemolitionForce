// chau tran
// purpose:			to set up the scene in the multiplayer games (creating the objectives). Use for the scene that is built for King of the Hild or Capture the flag
// where to put:	Game Controller game object
// NOTE:			designers have to manually set up differently for different scenes.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerLevelSetup : NetworkBehaviour {

	public GameObject PointPosition_KingOfTheHill;
	public GameObject PointPosition_CaptureFlag_Team1;
	public GameObject PointPosition_CaptureFlag_Team2;

	[ServerCallback]
	void Start ()
	{
		print ("Start");
		switch (GameControllerNetworking.Instance.gameType)
		{
			case MultiplayerGameType.KingOfTheHill:
				RpcSetupKOTHPoint ();
				break;
			case MultiplayerGameType.CaptureTheFlag:
				RpcSetupCTFPoint ();
				break;
		}
	}
	[ClientRpc]
	void RpcSetupKOTHPoint()
	{
		PointPosition_KingOfTheHill.SetActive (true);
		PointPosition_CaptureFlag_Team1.SetActive (false);
		PointPosition_CaptureFlag_Team2.SetActive (false);
	}
	[ClientRpc]
	void RpcSetupCTFPoint()
	{
		PointPosition_KingOfTheHill.SetActive (false);
		PointPosition_CaptureFlag_Team1.SetActive (true);
		PointPosition_CaptureFlag_Team2.SetActive (true);
	}
}
