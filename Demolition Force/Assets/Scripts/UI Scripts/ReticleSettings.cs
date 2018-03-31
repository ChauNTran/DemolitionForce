// chau tran
// where to put:	in the gameplay canvas
// purpose:			swap the reticle to red reticle when see enemy

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReticleSettings : MonoBehaviour {

	private Animator reticleAnimator;

	void Start()
	{
		//find the animator componet in the "Reticle" game object
		reticleAnimator = transform.Find("Reticle").GetComponent<Animator> ();
	}
	
	void Update ()
	{
		if (TankTurretControl.objectBeingLookedAt != null && TankTurretControl.objectBeingLookedAt.tag == "Tank") 
		{
			reticleAnimator.SetBool ("focus", true);
		} 
		else
			reticleAnimator.SetBool ("focus", false);
	}
}
