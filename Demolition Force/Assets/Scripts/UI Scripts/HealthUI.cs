// chau tran

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	Text p_HealthText;
	RectTransform p_HP_Filler;	// p_ stands for player
	RectTransform p_HP_Background;

	Text e_HealthText;
	RectTransform e_HP_Filler;	//e_ stands for enemy
	RectTransform e_HP_Background;
	RectTransform e_HP_Transform;

	float e_health;
	float e_max_health;
	bool countDown = false;
	float timer = 0f;
	TankHealth PlayerHealth;

	void Awake()
	{
		p_HealthText = transform.Find ("HP_text").GetComponent<Text>();
		p_HP_Filler = transform.Find ("HP_text/HP_Filler").GetComponent<RectTransform>();
		p_HP_Background = transform.Find ("HP_text/HP_Background").GetComponent<RectTransform>();
		e_HealthText = transform.Find("EnemyHP_text").GetComponent<Text>();
		e_HP_Filler =  transform.Find("EnemyHP_text/HP_Filler").GetComponent<RectTransform>();
		e_HP_Background = transform.Find("EnemyHP_text/HP_Background").GetComponent<RectTransform>();
		e_HP_Transform = transform.Find("EnemyHP_text/enemyhpPosition").GetComponent<RectTransform>();

		PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<TankHealth>();

	}

	void Start ()
	{
		HideEnemyHP ();
	}
	void Update ()
	{
		p_HealthText.text = PlayerHealth.getTankHealth().ToString() + "/" + PlayerHealth.tankFullHealth.ToString();

		HealthBarUpdate ();

		if (countDown) {
			timer += Time.deltaTime;
			if (timer >= 10f)
			{
				HideEnemyHP ();
			}
		}
	}

	void HealthBarUpdate()
	{
		float ratio;
		ratio =	p_HP_Background.rect.width / PlayerHealth.tankFullHealth ;

		float HPwidth;
		HPwidth = PlayerHealth.getTankHealth () * ratio;
		p_HP_Filler.sizeDelta = new Vector2 (HPwidth,50f);
	}

	public void EnemyHealthBarUpdate(float e_hp, float e_max_hp)
	{
		e_health = e_hp;
		e_max_health = e_max_hp;

		float e_ratio;
		e_ratio =	e_HP_Background.rect.width / e_max_hp ;

		countDown = true;
		timer = 0f;
		ShowEnemyHP ();

		float e_HPwidth;
		e_HPwidth = e_hp * e_ratio;
		e_HP_Filler.sizeDelta = new Vector2 (e_HPwidth,35f);
	}

	public void HideEnemyHP()
	{
		//print ("hide");
		e_HealthText.text = "";
		timer = 0f;
		countDown = false;
		e_HP_Filler.anchoredPosition = new Vector2 (1000f,1000f);
		e_HP_Background.anchoredPosition = new Vector2 (1000f,1000f);
	}

	public void ShowEnemyHP()
	{
		e_HealthText.text = e_health.ToString() + "/" + e_max_health.ToString();
		e_HP_Filler.anchoredPosition = e_HP_Transform.anchoredPosition;
		e_HP_Background.anchoredPosition = e_HP_Transform.anchoredPosition;
	}
}
