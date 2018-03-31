// chau tran
// where to put: 	the explosion particle system
// purpose : 		make particle system disappear it's done playing

using UnityEngine;
using System.Collections;

public class ExplosionDies : MonoBehaviour {

	ParticleSystem ps;

	void Start()
	{
		ps = GetComponentInChildren<ParticleSystem> ();
	}

	void Update ()
	{
		if (!ps.IsAlive (true))
			Destroy (this.gameObject);
	}
}
