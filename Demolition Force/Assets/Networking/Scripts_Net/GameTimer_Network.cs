using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameTimer_Network : NetworkBehaviour {

	[SyncVar] float gameTime; //in seconds the amount of time the game is running on...this number is set from game controller due to ENUM TimePlayLimit
	[SyncVar(hook = "UpdateTimeUI")] public float timer; //variable to current time


	[SyncVar]public bool masterTimer = false;

	GameTimer_Network serverTime;
	[HideInInspector]
	[SyncVar]public bool startGameTime = false; //flagged when game controller is ready to set timer
	public bool timesUp = false; //flag when timer has reached 0

	public Text timerUI;

	public override void OnStartServer ()//set time from controller
	{
		base.OnStartServer ();
		if (GameControllerNetworking.Instance.timePlayLimit == TimePlayLimit.FiveMinutes) {
			gameTime = 60f * 5f;
		} else if (GameControllerNetworking.Instance.timePlayLimit == TimePlayLimit.TenMinutes) {
			gameTime = 60f * 10f;
		} else if (GameControllerNetworking.Instance.timePlayLimit == TimePlayLimit.FifteenMinutes) {
			gameTime = 60f * 15f;
		} else if (GameControllerNetworking.Instance.timePlayLimit == TimePlayLimit.TwentyMinutes) {
			gameTime = 60f * 20f;
		} else if (GameControllerNetworking.Instance.timePlayLimit == TimePlayLimit.noLimit) {
			//disable this script and UI ***
			gameTime = 90f;
		} else {
			//disable this script and UI ***
		}

	}


	void Start () {
		
		if (isServer) { //for host to do: use the timer and control timer
			
			timer = gameTime + 1f;

			if (isLocalPlayer) {
				serverTime = this;
				masterTimer = true;
			}
		} else if (isLocalPlayer) {
			GameTimer_Network[] timers = FindObjectsOfType<GameTimer_Network> ();
			for (int i = 0; i < timers.Length; i++) {
				if (timers [i].masterTimer) {
					serverTime = timers [i];
				}
			}
		}
	}

	void Update () {
		if (masterTimer && startGameTime && !timesUp) {//only master timer controls time
			if (timer <= 1) 
			{
				timer = 0; 
				//GAME OVER DUE TO TIME
				timesUp = true;

			} 
			else 
			{
				timer -= Time.deltaTime;
			}
		//	print (timer);

		}
	



		if (isLocalPlayer) {//everyone else updates their own time accrodingly
			if (serverTime) {
				gameTime = serverTime.gameTime;
				timer = serverTime.timer;
			} else {
				GameTimer_Network[] timers = FindObjectsOfType<GameTimer_Network>();
				for(int i = 0; i<timers.Length;i++){
					if(timers[i].masterTimer){
						serverTime = timers[i];
					}
				}
			}
		}
	}

	public void UpdateTimeUI(float time)
	{
		if (timerUI != null) {
			int convertedTime = (int)time;
			timerUI.text = convertedTime.ToString ();
			timerUI.text = string.Format ("{0:0}:{1:00}", Mathf.Floor (convertedTime / 60), convertedTime % 60);
		}
	}
}
