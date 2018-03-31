// chau tran
// where to put:		in the tank
//	purpose:			to contain the tank health veriable

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class TankHealthNetworking : NetworkBehaviour {

	public float tankFullHealth = 100f;

	[SyncVar(hook ="UpdateHealthBar")]public float t_Health;// tank health ; make it private so it cannot be access from outside

	private bool shieldTank = false;
	PlayerMultiplayer playerMultiplayerScript;
	public GameObject deathParticles;
	public GameUINetworking gameUIScipt;

	[SyncVar]public bool isDead = false;
	[HideInInspector]public bool deadOnce; //prevent over kill
	bool rpcDieOnce = false; //prevent rpc from calling twice
	[HideInInspector]public RectTransform p_HP_Background;
	[HideInInspector]public RectTransform p_HP_Filler;
	[HideInInspector]public PlayerMultiplayer whoKilledMe;
	float shield_timer;

	void Start()
	{
		deadOnce = false;
		if(isLocalPlayer)
		{
		playerMultiplayerScript = GetComponent<PlayerMultiplayer> ();
		gameUIScipt = GameObject.FindGameObjectWithTag ("Game UI").GetComponent<GameUINetworking> ();
		UpdateHealthBar (t_Health);
		//isDead = false;
		}
		isDead = false;
		CmdFullHeath ();
	}
	public float getTankHealth()
	{
		return t_Health;
	}
	public void SetTankFullHealth(float newFullHealth)
	{
		tankFullHealth = newFullHealth;
	}

	public void UpdateHealthBar(float currentHP)
	{
		if (gameUIScipt != null) 
		{
			t_Health = currentHP;
			gameUIScipt.UpdateHealthBarUI (tankFullHealth, currentHP);
		}
		UpdateNonLocalHealthBarUI (currentHP);
	}
	public void ShieldActivate(float timer)
	{
		shieldTank = true;

		RpcShieldActivate (timer);
	}

	IEnumerator Shield_Countdown()
	{
		gameUIScipt.SetShieldCounterText (shield_timer);
		yield return new WaitForSeconds (1f);
		shield_timer -= 1f;
	
		// show the UI maybe
		if (shield_timer <=0f) {
			shield_timer = 0f;
			gameUIScipt.ShieldCounterReset ();
			CmdShieldDeactivate ();
		}
		else
			StartCoroutine (Shield_Countdown());
	}
	[Command]
	public void CmdShieldDeactivate()
	{
//		this.transform.FindChild("Shield").gameObject.SetActive(false);
		shieldTank = false;
		RpcShieldDeactivate();
	}
	public void TakeDamage(float damage)
	{
		if (!isServer) {
			return;
		}
		if (shieldTank != true) 
		{
			t_Health -= damage;
			if (t_Health <= 0 && !isDead) 
			{
				isDead = true;
				GameControllerNetworking.Instance.UpdateScoreBoard (); //update score board
				RpcPlayerDies (3,whoKilledMe.playerName + " Destroyed you");//call death function
			}
		}
	}
	/*
	[Command]
	public void CmdTankDamage(float damageRate)	// called to be damaged
	{
		if (shieldTank != true)	// take no damage when shield is activated
		{
			if (t_Health > 0) 
			{
				t_Health -= damageRate;
				if (t_Health <= 0) 
				{
					RpcPlayerDies ();	// play the effect; respawn and stuff
					playerMultiplayerScript.AddDeathCount();// add death count to this player
					GetComponent<PlayerMultiplayer>().DisablePlayer();//Must disable on server too
					// add the kill count to the player who shoot
				}
			}
			else
			{
				RpcPlayerDies ();
			}
		}
	}*/
	// set tank health to full health
	[Command]
	public void CmdFullHeath()
	{
		t_Health = tankFullHealth;
	}
	// call this to activate the shield
	[ClientRpc]
	public void RpcShieldActivate(float timer)
	{
		this.transform.Find("Shield").gameObject.SetActive(true);
		shield_timer = timer;
		if (isLocalPlayer)
			StartCoroutine (Shield_Countdown());
	}
	// call this to de-activate the shield
	[ClientRpc]
	public void  RpcShieldDeactivate()
	{
		this.transform.Find("Shield").gameObject.SetActive(false);

	}

	public void AddHealthCapacity(float moreHP)
	{
		moreHP += tankFullHealth;
		tankFullHealth = moreHP;
	}

	[ClientRpc]
	public void RpcPlayerDies(int respawnTime, string whoKilledMe)
	{
		//to prevent this from being called several times
		if (rpcDieOnce == false) {
			rpcDieOnce = true;
			//instanitate death particles
			//	Instantiate(Resources.Load("Explosions/explosion_barrel_02"),this.transform.position,Quaternion.identity);// change this explosion later
			Instantiate (deathParticles, this.transform.position, Quaternion.identity);
			//deactivate player name UI if non local player
			if (gameObject.GetComponent<PlayerMultiplayer> ().playerNameUI != null) {
				print ("DISABLE PLAYER NAME UI");
				gameObject.GetComponent<PlayerMultiplayer> ().playerNameUI.gameObject.SetActive (false);
			}
			//Disable player but not camera until respawn happens
			if (GameControllerNetworking.Instance.isGameOver)
				return;
			RespawnDisablePlayer ();

			if (this.gameObject.GetComponent<NetworkIdentity> ().isLocalPlayer) {
				CmdAddDeathCount ();// add death count to player if local
				GetComponent<AudioSource> ().Stop ();
				//zero out velocity
				this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
				this.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
				this.GetComponent<Rigidbody> ().useGravity = false;
				this.transform.rotation = Quaternion.identity;
				StartCoroutine (PlayerRespawnCountDown (respawnTime, whoKilledMe)); // start respawn count down

			}
		}

	}
	//called from rpc player dies
	[Command]
	void CmdAddDeathCount()
	{
		GetComponent<PlayerMultiplayer>().AddDeathCount();
		GameControllerNetworking.Instance.UpdateScoreBoard (); //update score board
	}

	//Update non local player healthbar that floats above tanks head
	void UpdateNonLocalHealthBarUI(float currentHealth)
	{
		if (!isLocalPlayer) { //only as non local player do we update this bar for the local player to see
			GameObject playerTagObj = gameObject.transform.Find("PlayerNameTag").gameObject;
			p_HP_Background = playerTagObj.transform.GetChild (0).gameObject.GetComponent<RectTransform>();
			p_HP_Filler = playerTagObj.transform.GetChild (1).gameObject.GetComponent<RectTransform>();

			float ratio;
			ratio =	p_HP_Background.rect.width / tankFullHealth ;

			float HPwidth;
			HPwidth = currentHealth * ratio;
			p_HP_Filler.sizeDelta = new Vector2 (HPwidth,50f);

		}
	}
	IEnumerator PlayerRespawnCountDown(int respawnTime, string whoKilledMe)
	{
		//CALL RESPAWN METHOD WITH RESPAWN TIME
//		if (GameControllerNetworking.Instance.isGameOver) {
//			yield return null;
//		}
		yield return GameControllerNetworking.Instance.RespawnCountDownIE(respawnTime,whoKilledMe);
		GetComponent<PlayerMultiplayer> ().changeClassUIScript.gameObject.GetComponent<CanvasGroup> ().alpha = 1;
		GetComponent<PlayerMultiplayer> ().changeClassUIScript.gameObject.GetComponent<CanvasGroup> ().interactable = true;
		GetComponent<PlayerMultiplayer> ().changeClassUIScript.gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		GetComponent<PlayerMultiplayer> ().gameUIScipt.gameObject.GetComponent<CanvasGroup> ().alpha = 0; //set gui to 0 alpha
		GetComponent<PlayerMultiplayer>().DisablePlayer(); //

		yield return null;
	}

	//DISABLES PLAYER PARTIALLY TO ALLOW FOR CAMERA 
	void RespawnDisablePlayer()
	{
		GetComponent<TankControllerNetworking> ().enabled = false;
		GetComponent<PlayerMultiplayer>().ToggleColliders (false);
		GetComponent<PlayerMultiplayer>().ToggleRenderer (false);
		CmdRespawnDisablePlayer ();

	}
	[Command]
	void CmdRespawnDisablePlayer()
	{
		GetComponent<TankControllerNetworking> ().enabled = false;
		GetComponent<PlayerMultiplayer>().ToggleColliders (false);
		GetComponent<PlayerMultiplayer>().ToggleRenderer (false);
	}
}
