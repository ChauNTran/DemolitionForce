//Chau Tran
//purpose:			controll the UI for Power Ups
//where to put:		"Game UI" object

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour {

	Image shieldImage;
	Image damageImage;
	Text shieldText;
	Text damageText;
	bool shieldCountDown = false;
	bool damageCountDown = false;

	float shieldTimer = 0f;
	float damageTimer = 0f;
	GameObject player;

	void Awake()
	{
		shieldImage = transform.Find ("shield_icon").GetComponent<Image> ();
		damageImage = transform.Find ("damage_icon").GetComponent<Image> ();

		shieldText= transform.Find ("shield_icon/shield_text").GetComponent<Text> ();
		damageText= transform.Find ("damage_icon/damage_text").GetComponent<Text> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Start ()
	{
		shieldImage.color = new Color (0.1f, 0.1f, 0.1f, 0.8f);
		damageImage.color = new Color (0.1f, 0.1f, 0.1f, 0.8f);

		shieldText.text = "";
		damageText.text = "";
	}
	void Update ()
	{
		if (shieldCountDown)
		{
			shieldTimer -= Time.deltaTime;
			shieldText.text = Mathf.RoundToInt (shieldTimer).ToString ();
			if(shieldTimer <= 0)
			{
				shieldImage.color = new Color (0.1f, 0.1f, 0.1f, 0.8f);
				player.GetComponent<TankHealth>().ShieldDeactivate();
				shieldTimer = 0f;
				shieldText.text = "";
				shieldCountDown = false;
			}
		}

		if (damageCountDown)
		{
			damageTimer -= Time.deltaTime;
			damageText.text = Mathf.RoundToInt (damageTimer).ToString ();
			if(damageTimer <= 0)
			{
				damageImage.color = new Color (0.1f, 0.1f, 0.1f, 0.8f);
				player.GetComponent<PlayerTankShoot>().AddDamageRate(-10f);
				player.GetComponent<PlayerTankShoot>().AddDamageRate_Secondary(-4f);
				damageTimer = 0f;
				damageText.text = "";
				damageCountDown = false;
			}
		}
	}

	public void ShieldUIActivate(float timer)
	{
		shieldImage.color = new Color (0.6f, 0.6f, 0.6f, 0.8f);
		shieldTimer = timer;
		shieldCountDown = true;
		shieldText.text = shieldTimer.ToString ();
		print ("shield");
	}

	public void DamageUIActivate(float timer2)
	{
		damageImage.color = new Color (0.6f, 0.6f, 0.6f, 0.8f);
		damageTimer = timer2;
		damageCountDown = true;
		damageText.text = damageTimer.ToString ();
		print ("damage");
	}
}
