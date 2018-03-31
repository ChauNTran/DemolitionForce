// chau tran
// where to put: 	non-player object that can get hit
// purpose : 		set toughness / explosion effect/ flashing when being hit 

using UnityEngine;
using System.Collections;

public class DestroyableSettings : MonoBehaviour {

	public float toughness; // how many hits by normal bullet
	private float objectHP; // object health points
	[HideInInspector]
	public bool hit = false;
	public Rigidbody[] Pieces; // The pieces will from the object when being destroyed;
	private Renderer theRenderer;
	public GameObject explosion;

	float timer = 0f;

	void Start()
	{
		theRenderer = GetComponent<Renderer> ();
		ResetNormalColor ();
		objectHP = toughness;
	}

	void Update()
	{
		if (hit) {
			FlashWhenHit ();
			timer += Time.deltaTime;
			if (timer >= 0.12f) {
				ResetNormalColor ();
				timer = 0f;
				hit = false;
			}
		}
	}
	void ExplodeAndDestroy()
	{
		if(explosion!= null)
		Explosion ();
		// if the user set up the explosion pieces
		if (Pieces.Length > 0)
		{
			for (int i = 0; i <= Pieces.Length - 1; i++) {
				Rigidbody clone;
				clone = Instantiate (Pieces [i], this.transform.position, Quaternion.identity) as Rigidbody;
				clone.velocity = new Vector3 (Random.Range (-5f, 5f), Random.Range (0f, 5f), Random.Range (-5f, 5f));
			}
			LoadRandom.LoadRandomPowerUp (this.transform);
			Destroy (this.gameObject);
		}
	}
	public void DoDamage(float damageRate)
	{
		objectHP -= damageRate;

		if (objectHP <= 0f)
			ExplodeAndDestroy ();
	}

	public void Explosion(){
		Instantiate (explosion, transform.position, transform.rotation);
	}

	void FlashWhenHit()
	{
		theRenderer.material.SetColor ("_EmissionColor", new Color(0.2f, 0.2f, 0.2f));
	}
	void ResetNormalColor()
	{
		theRenderer.material.SetColor ("_EmissionColor", new Color(0.01f, 0.01f, 0.01f));
	}
}
