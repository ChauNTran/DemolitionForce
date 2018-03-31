using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour {

	public float lifetime;
	public string soundFX;

	void Start () {
	//if (explosionSound != null)
		if (AudioManager.instance != null)
		{
			//AudioManager.instance.PlaySound (explosionSound, transform.position);
			if(soundFX != "")
			AudioManager.instance.PlaySound (soundFX, transform.position); //choose from random library of explosions
		}
		Destroy (this.gameObject,lifetime);
	//}
	}
}
