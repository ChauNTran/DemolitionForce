// chau tran
// purpose:		Control Game UI in the Multiplayer game
// where to put:	Game UI game object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class GameUINetworking : NetworkBehaviour {

	private Animator reticleAnimator;
	public RectTransform p_HP_Background;
	public RectTransform p_HP_Filler;
	public Text KillCountText;
	public Text DeathCountText;
	public Text HP_Text;
	public GameObject shield;
	public GameObject damage;
	public LayerMask enemyLayer; // the one the eneny is on
	public LayerMask tankLayer; // the layer that stores the collider components
	public GameObject killPanelUI;
	public Text[] killTexts;	
	public float SightRange = 60f;
	float UIratio;
	int currentTankDetected;
	[SyncVar]int killCounter = 0;
	RectTransform radarTransform;

	public static GameObject LocalPlayer;
	Collider[] hitColliders;

	void Reset()
	{
		p_HP_Background = GameObject.Find("HP_Background").GetComponent<RectTransform>();
		p_HP_Filler = GameObject.Find("HP_Filler").GetComponent<RectTransform>();
		DeathCountText = GameObject.Find ("DeathCount_Text").GetComponent<Text>();
		KillCountText = GameObject.Find ("KillCount_Text").GetComponent<Text>();
		HP_Text = GameObject.Find ("HP_Text").GetComponent<Text> ();
		shield = GameObject.Find ("shield_icon");
		damage = GameObject.Find ("damage_icon");
	}
	void OnEnable ()
	{
		if(killTexts.Length == 0)
			killTexts = killPanelUI.GetComponentsInChildren<Text> ();
	}
	void Start ()
	{
		reticleAnimator = transform.Find("Reticle").GetComponent<Animator> ();
		radarTransform = transform.Find ("Radar_img").GetComponent<RectTransform> ();

		DamageCounterReset();
		ShieldCounterReset ();

		UIratio = (radarTransform.rect.width/SightRange)/2;

		if(hitColliders != null)
			currentTankDetected = hitColliders.Length;
		if(LocalPlayer!= null)
			hitColliders = Physics.OverlapSphere (LocalPlayer.transform.position, SightRange, 1 << 8);
	}

	public void ShieldCounterReset()
	{
		shield.GetComponentInChildren<Text> ().text = "";
		shield.GetComponentInChildren<Image> ().color = Color.grey;
	}
	public void DamageCounterReset()
	{
		damage.GetComponentInChildren<Text> ().text = "";
		damage.GetComponentInChildren<Image> ().color = Color.grey;
	}

	public void SetShieldCounterText(float timeFloat)
	{
		shield.GetComponentInChildren<Text> ().text = timeFloat.ToString();
		shield.GetComponentInChildren<Image> ().color = Color.cyan;
	}
	public void SetDamageCounterText(float timeFloat)
	{
		damage.GetComponentInChildren<Text> ().text = timeFloat.ToString();
		damage.GetComponentInChildren<Image> ().color = Color.cyan;
	}

	void Update ()
	{
		ReticleFocus ();
		if(LocalPlayer != null)
			CheckEnemy ();
	}
	public void UpdateHealthBarUI(float tankfullHP, float tankCurrentHP)
	{
		if(p_HP_Background != null || p_HP_Filler!= null || HP_Text != null)
		{
			//TankHealthNetworking localPlayerHealth= LocalPlayer.GetComponent<TankHealthNetworking> (); //***WASTNT WORKING
			TankHealthNetworking localPlayerHealth= FindLocalPlayer().GetComponent<TankHealthNetworking> ();

			float ratio;
			ratio =	p_HP_Background.rect.width / localPlayerHealth.tankFullHealth ;

			float HPwidth;
			HPwidth = localPlayerHealth.getTankHealth() * ratio;
			p_HP_Filler.sizeDelta = new Vector2 (HPwidth,50f);

			HP_Text.text = localPlayerHealth.getTankHealth () + "/" + localPlayerHealth.tankFullHealth;
		}
	}
	public void DeathCountRefresh(int deathcount)
	{
		DeathCountText.text = "Death: " + deathcount;
	}
	public void KillCountRefresh(int killCount)
	{
		KillCountText.text = "Kills: " + killCount;
	}
	void ReticleFocus()
	{
		if (TankTurretControl.objectBeingLookedAt != null && TankTurretControl.objectBeingLookedAt.tag == "Tank") {
			if (reticleAnimator.GetBool ("focus") == false)
				reticleAnimator.SetBool ("focus", true);
			else
				return;
		} else {
			if (reticleAnimator.GetBool ("focus") == true)
				reticleAnimator.SetBool ("focus", false);
			else
				return;
		}
	}
	void CheckEnemy()
	{
		hitColliders = Physics.OverlapSphere (LocalPlayer.transform.position, SightRange, enemyLayer);//     ENEMY TANKS NEED NEED TO BE SET TO THE "TANKS" LAYER   
		if (radarTransform.childCount != hitColliders.Length)	//if the number of tank detected is different than the number of dot in the radar
		{	
			if (radarTransform.childCount > 0)
				foreach (Transform dot in radarTransform) {
					Destroy (dot.gameObject);
				}
			// and make new dots associating with new tanks's position
			for (int i = 0; i < hitColliders.Length; i++) {
				Vector3 distance = hitColliders [i].gameObject.transform.position - LocalPlayer.transform.position;
				float angle = Vector3.Angle (distance, LocalPlayer.transform.forward);
				float sign = Mathf.Sign (Vector3.Dot (distance, LocalPlayer.transform.right));// this float shows whether it's on the left or right. If it's positive, it's on the right. Otherwise, it's on the left
				MakeDot (distance.magnitude, angle, sign);
			}
		}
		else
		{
			RectTransform[] dotArray = radarTransform.GetComponentsInChildren<RectTransform> ();
			for (int i = 0; i < radarTransform.childCount; i++)
			{
				Vector3 distance = hitColliders [i].gameObject.transform.position - LocalPlayer.transform.position;
				float angle = Vector3.Angle (distance, LocalPlayer.transform.forward);
				float sign = Mathf.Sign (Vector3.Dot (distance, LocalPlayer.transform.right));
				dotArray [i + 1].anchoredPosition = DotPosition (distance.magnitude, angle, sign);
			} 
		}
	}
	void MakeDot (float distance, float angle, float sign)
	{
		RectTransform dot;
		dot = Instantiate(Resources.Load("dot", typeof (RectTransform)), radarTransform.position, Quaternion.identity) as RectTransform;
		dot.transform.SetParent (radarTransform);
		float tankX = Mathf.Sin (angle * Mathf.Deg2Rad) * distance * sign * UIratio;
		float tankY = Mathf.Cos (angle * Mathf.Deg2Rad) * distance * UIratio;
		dot.anchoredPosition = new Vector2 (tankX, tankY);
	}
	Vector2 DotPosition(float distance, float angle, float sign)
	{
		Vector2 pos;
		float tankX = Mathf.Sin (angle * Mathf.Deg2Rad) * distance * sign * UIratio;
		float tankY = Mathf.Cos (angle * Mathf.Deg2Rad) * distance * UIratio;
		pos = new Vector2 (tankX, tankY);
		return pos;
	}
	//script for finding player
	public GameObject FindLocalPlayer()
	{
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player")) 
		{
			if (obj.GetComponent<NetworkIdentity> ().isLocalPlayer) 
			{
				return obj;
			}
		}
		return null;
	}
//	public void ShowKillUI(string p_whokillplayer, string p_whoplayerkill)
//	{
//		print (p_whokillplayer);
//		print (p_whoplayerkill);
//		RpcShowKillUI (p_whokillplayer, p_whoplayerkill);
//	}
//	[ClientRpc]
//	public void RpcShowKillUI(string whokillplayer, string whoplayerkill)
//	{
//		print ("show kill");
//		killCounter += 1;
//		if (killCounter == 1) {
//			killTexts [0].text = whokillplayer + " Destroys " + whoplayerkill;
//		} else if (killCounter == 2) {
//			killTexts [1].text = whokillplayer + " Destroys " + whoplayerkill;
//		} else if (killCounter == 3) {
//			killTexts [2].text = whokillplayer + " Destroys " + whoplayerkill;
//		} else if (killCounter == 4) {
//			killTexts [3].text = whokillplayer + " Destroys " + whoplayerkill;
//		} else if (killCounter == 5) {
//			killTexts [4].text = whokillplayer + " Destroys " + whoplayerkill;
//		} else if (killCounter > 5) {
//			for (int i = 0; i < killTexts.Length ; i++)
//			{
//				killTexts [i].text = killTexts [i + 1].text;
//				if (i == killTexts.Length - 1)
//					killTexts[i].text = whokillplayer + " Destroys " + whoplayerkill;
//			}
//		}
//	} 	
}