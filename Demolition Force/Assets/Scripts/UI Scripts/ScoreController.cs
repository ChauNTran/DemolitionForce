using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreController : MonoBehaviour {


	Dictionary<string, Dictionary<string, int> > playerScores;

	int changeCounter;

	void Start(){

		/*SetScore ("player1", "kills", 12); 
		SetScore ("player1", "deaths",123); 
		SetScore ("player2", "kills",423);
		SetScore ("player2", "deaths", 12); 
		SetScore ("player3", "kills", 345); 
		SetScore ("player3", "deaths", 21); 
		SetScore ("player4", "kills", 1); 
		SetScore ("player4", "deaths",654); 
		
		Debug.Log (GetScore ("player1", "kills"));*/
	}


	void Init(){
		if(playerScores != null)
			return;

		playerScores = new Dictionary<string, Dictionary<string, int>> ();
	}


	public int GetScore(string username, string scoreType){
		Init ();

		if (playerScores.ContainsKey (username) == false) {
			return 0;
		}

		if (playerScores [username].ContainsKey (scoreType) == false) {
			return 0;
		}

		return playerScores [username] [scoreType];
	}


	public void SetScore(string username, string scoreType, int value){
		Init ();

		if (playerScores.ContainsKey (username) == false) {
			playerScores[username] = new Dictionary<string, int>();
		}

		playerScores [username] [scoreType] = value;
	
	}


	public void ChangeScore(string username, string scoreType, int amount){
		Init ();
	
		int currentScore = GetScore (username, scoreType);
		SetScore (username, scoreType, currentScore + amount);
	}

	public string[] GetPlayerNames(){
		Init ();
		return playerScores.Keys.ToArray();
	}

	public string[] GetPlayerNames(string sortingScoreType){
		Init ();

		return playerScores.Keys.OrderByDescending (n => GetScore (n, sortingScoreType)).ToArray ();
	}

	public void DEBUG_ADD_KILL_TO_PLAYER1(){
		ChangeScore ("player1", "kills", 10);
	}

	public int GetChangeCounter(){
		return changeCounter;
	}
}