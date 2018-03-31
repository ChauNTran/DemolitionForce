// chau tran
// purpose: kill player when touched
// where to put: lava

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InstantKill : NetworkBehaviour {

	[ServerCallback]
	void OnTriggerEnter(Collider other)
	{
		//this may get called a couple to several times 
		if ( other.gameObject.layer == 16 ||  other.gameObject.layer == 8 ||other.gameObject.layer == 0 ||other.gameObject.layer == 2)
		{
			GameObject playerObj = other.gameObject.transform.root.gameObject;
			TankHealthNetworking tankHealth = playerObj.gameObject.GetComponent<TankHealthNetworking> ();
			if(tankHealth != null){ //if this is a player in the lobby
			//tankHealth.TakeDamage (tankHealth.t_Health);
			//tankHealth.isDead = true;
			print("death lava");
			//playerObj.GetComponent<PlayerMultiplayer>().AddDeathCount();
			tankHealth.RpcPlayerDies (9,"Lava Lava!");// Disable player
			GameControllerNetworking.Instance.UpdateScoreBoard (); //update score board
			}
		}
	}
}
