// chau tran
// where to put:		in the tank
//	purpose:			to contain the tank health veriable

using UnityEngine;
using System.Collections;

public class TankHealth : MonoBehaviour {

	public float tankFullHealth = 100f;
	[SerializeField]private float t_Health;// tank health ; make it private so it cannot be access from outside
	private bool shieldTank = false;
	//called to let gamecontroller know to execute game over.
	public delegate void DeathDelegate ();
	public event DeathDelegate deathEvent;

	void Start()
	{
		t_Health = tankFullHealth;
	}
	public float getTankHealth()
	{
		return t_Health;
	}
	public void SetTankFullHealth(float newFullHealth)
	{
		tankFullHealth = newFullHealth;
	}

	public void TankDamage(float damageRate)
	{
		if (shieldTank != true)	// take no damage when shield is activated
		{
			if (t_Health > 0) {
				t_Health -= damageRate;
				if (t_Health <= 0) {
					if (this.gameObject.tag != "Player")
						TankDies ();
					else
						PlayerDies ();
				}
			} else {
				if (this.gameObject.tag != "Player")
					TankDies ();
				else
					PlayerDies ();
			}
		}
	}
	// set tank health to full health
	public void FullHeath()
	{
		t_Health = tankFullHealth;
	}

	public void ShieldActivate()
	{
		this.transform.Find("Shield").gameObject.SetActive(true);
	}

	public void ShieldDeactivate()
	{
		this.transform.Find("Shield").gameObject.SetActive(false);

	}

	public void AddHealthCapacity(float moreHP)
	{
		moreHP += tankFullHealth;
		tankFullHealth = moreHP;
	}


	void PlayerDies()
	{
		if (deathEvent != null) {
			deathEvent (); //tell game controller player has died
			this.gameObject.SetActive(false);
		}

	}
	void TankDies()
	{
		t_Health = 0f;
		Instantiate(Resources.Load("Explosions/explosion_tank_Truck", typeof (GameObject)), this.transform.position, Quaternion.identity);	// replace to another explosion

		// hide the tank HP UI
		GameObject.Find ("Game UI").GetComponent<HealthUI> ().HideEnemyHP();
		// the tank has 20% chance of dropping power up and 80% chance of dropping gubs

		if (GameObject.FindGameObjectWithTag ("Player").GetComponent<TankHealth> ().getTankHealth () < 50f)
			LoadRandom.LoadHealthHalfChance (this.transform);
			
		else
		{
			float value = Random.value;
			if (value < 0.8f)
				Instantiate (Resources.Load("gubs"),this.transform.position, Quaternion.identity);
			else
				LoadRandom.LoadRandomPowerUp(this.transform);

			this.gameObject.SetActive(false);
		}
		Destroy (this.gameObject);
	}
}
