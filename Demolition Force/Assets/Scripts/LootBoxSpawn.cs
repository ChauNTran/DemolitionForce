using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LootBoxSpawn : NetworkBehaviour {

	[SerializeField] float SpawnTimer = 15f;
	[SerializeField] GameObject LootBoxObject;
	GameObject clone;
	[ServerCallback]
	void Start ()
	{
		StartCoroutine(SpawnLootBox());

	}
//	public override void OnStartServer ()
//	{
//		base.OnStartServer ();
//		StartCoroutine(SpawnLootBox());
//	}
	IEnumerator SpawnLootBox()
	{
		if (clone == null) {
			//print ("NO loot box");
			yield return new WaitForSeconds (SpawnTimer);
			clone = Instantiate (LootBoxObject, transform.position, Quaternion.identity) as GameObject;
			NetworkServer.Spawn (clone);

			StartCoroutine(SpawnLootBox());
		} else {
			yield return new WaitForSeconds (1f);
			StartCoroutine(SpawnLootBox());
		}
	}
}
