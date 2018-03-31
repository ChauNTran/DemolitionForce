using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LootBoxNetworking : NetworkBehaviour {

	int hitPoint = 1;
	MeshRenderer M_renderer;

	void OnEnable()
	{
		M_renderer = GetComponent<MeshRenderer> ();
	}

	public void GetHit()
	{
		if (isServer) {
			hitPoint -= 1;
			if (hitPoint <= 0) {
				// spawn the power up
				float chance = Random.value;
				if (chance <= 0.2f) {
					GameObject clone = Instantiate (Resources.Load ("PowerUpNetworking/FullHealth"), transform.position, Quaternion.identity) as GameObject;
					NetworkServer.Spawn (clone);
				} else {
					GameObject[] allPowerUps;
					allPowerUps = Resources.LoadAll<GameObject> ("PowerUpNetworking");	// LoadAll game objects in the PowerUp folder
					GameObject clone = Instantiate (allPowerUps [Random.Range (0, allPowerUps.Length - 1)], transform.position, Quaternion.identity) as GameObject;
					NetworkServer.Spawn (clone);
				}
				// then destroy the box

				NetworkServer.Destroy (this.gameObject);
				Destroy (this.gameObject);
			} else
				StartCoroutine (FlashWhenHit ());
		}
		else
			StartCoroutine (FlashWhenHit ());
	}

	IEnumerator FlashWhenHit()
	{
		M_renderer.material.SetColor ("_EmissionColor", new Color(0.2f, 0.2f, 0.2f));
		yield return new WaitForSeconds (0.2f);
		M_renderer.material.SetColor ("_EmissionColor", new Color(0.01f, 0.01f, 0.01f));
	}
}
