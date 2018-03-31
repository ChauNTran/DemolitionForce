// chau tran
// where to put:	Game Controller
// purpose:			spawn enemies

using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	public GameObject EnemyTank;
	public Transform[] spawnPosistions;

	void Start () 
	{
		SpawnTank ();
	}
	
	public void SpawnTank()
	{
		for (int i = 0; i < spawnPosistions.Length; i++) 
		{
			Instantiate(EnemyTank, spawnPosistions[i].position, Quaternion.identity);
		}
	}
}
