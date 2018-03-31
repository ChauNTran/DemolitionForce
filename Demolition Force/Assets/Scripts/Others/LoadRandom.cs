// chau tran
// where to put:	no where. Static class
// purpose:			load random objects in the Resources folder

using UnityEngine;
using System.Collections;

public class LoadRandom : MonoBehaviour {


	// call to spawn random power up
	public static void LoadRandomPowerUp (Transform targetTransform)
	{
		GameObject[] allPowerUps;
		allPowerUps = Resources.LoadAll<GameObject> ("PowerUp");	// LoadAll game objects in the PowerUp folder

		Instantiate (allPowerUps[Random.Range(0,allPowerUps.Length-1)], targetTransform.position, Quaternion.identity);
	}

	public static void LoadHealthHalfChance(Transform targetTransform)	// increase chance of dropping health to 55%
	{
		float chance = Random.value;
		if (chance <= 0.4f)
		{
			Instantiate (Resources.Load ("PowerUp/FullHealth"), targetTransform.position, Quaternion.identity);
		}
		else
		{
			GameObject[] allPowerUps;
			allPowerUps = Resources.LoadAll<GameObject> ("PowerUp");	// LoadAll game objects in the PowerUp folder

			Instantiate (allPowerUps[Random.Range(0,allPowerUps.Length-1)], targetTransform.position, Quaternion.identity);
		}
	}
}
