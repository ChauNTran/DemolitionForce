using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Point_KingOfTheHill : NetworkBehaviour {

	[SerializeField] private float TakeOverTime = 5f;

	[SerializeField] public List<GameObject> playersAtThePoint;
	[SerializeField] public List<GameObject> playersTeam1AtThePoint;
	[SerializeField] public List<GameObject> playersTeam2AtThePoint;

	bool Team1AtPoint = false;
	bool Team2AtPoint = false;
	bool Team1TakePoint = false;
	bool Team2TakePoint = false;


	[SerializeField] public static bool pointIsTaken = false;
	[SerializeField] float timer = 0f;


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			playersAtThePoint.Add (other.gameObject);
//			if(other.gameObject.GetComponent<PlayerMultiplayer>().tankManager.team == Team.Team1)
//				playersTeam1AtThePoint.Add (other.gameObject);
//			else if(other.gameObject.GetComponent<PlayerMultiplayer>().tankManager.team == Team.Team2)
//				playersTeam2AtThePoint.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
//			if(other.gameObject.GetComponent<PlayerMultiplayer>().tankManager.team == Team.Team1)
//				playersTeam1AtThePoint.Remove (other.gameObject);
//			else if(other.gameObject.GetComponent<PlayerMultiplayer>().tankManager.team == Team.Team2)
//				playersTeam2AtThePoint.Remove (other.gameObject);
			playersAtThePoint.Remove (other.gameObject);
		}
	}
	void Update()
	{
		if (!isServer)
			return;
		
		CheckPlayerExistence ();
		if (playersAtThePoint.Count > 0)
		{
			if (playersTeam1AtThePoint.Count > 0 && playersTeam2AtThePoint.Count == 0 )	// if only team 1 members are at the point
			{
				if(pointIsTaken!= true || Team2TakePoint == true )	//	if the point has not yet taken by team 1
				{
				timer += Time.deltaTime;	// start the timer
				Team1AtPoint = true;
				Team2AtPoint = false;
				}
			} else if (playersTeam2AtThePoint.Count > 0 && playersTeam1AtThePoint.Count == 0) // if only team 2 members are at the point
			{
				if (pointIsTaken != true || Team1TakePoint == true)		//	if the point has not yet taken by team 2
				{ 	
					timer += Time.deltaTime;	// start the timer
					Team2AtPoint = true;
					Team1AtPoint = false;
				}
			} else   // when no one is at the point
			{
				timer = 0f;
				Team1AtPoint = false;
				Team2AtPoint = false;
			}

			if (timer > TakeOverTime)
			{
				timer = 0f;
				pointIsTaken = true;
				if (Team1AtPoint)
				{
					print ("Team 1 has taken the point");
					Team1TakePoint = true;
					Team2TakePoint = false;
					// send message to the Game Controller
					GameControllerNetworking.Instance.Team1CapThePoint(true);
				} else if (Team2AtPoint)
				{
					print ("Team 2 has taken the point");
					Team1TakePoint = false;
					Team2TakePoint = true;
					// send message to the Game Controller
					GameControllerNetworking.Instance.Team1CapThePoint(false);
				}
			}

		} else
			return;

	}
		
	void CheckPlayerExistence()
	{
		foreach (GameObject player in playersAtThePoint)
		{
			if (player.activeInHierarchy != true) {
//				if(player.GetComponent<PlayerMultiplayer>().tankManager.team == Team.Team1)
//					playersTeam1AtThePoint.Remove (player);
//				else if(player.GetComponent<PlayerMultiplayer>().tankManager.team == Team.Team2)
//					playersTeam2AtThePoint.Remove (player);
				playersAtThePoint.Remove (player);
			}
		}
	}
}
