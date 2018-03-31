// chau tran
// where to put: 	put in the barrel prefab
// purpose : 		make the barrel explosion

using UnityEngine;
using System.Collections;

public class BarrelExplodes : MonoBehaviour {

	public float barrelDamage = 110f;

	public void BarrelExplosion()
	{
		Instantiate(Resources.Load("Explosions/explosion_barrel", typeof (GameObject)), this.transform.position, Quaternion.identity);
	}
}
