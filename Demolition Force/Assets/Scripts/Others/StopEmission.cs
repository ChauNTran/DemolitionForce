using UnityEngine;
using System.Collections;

public class StopEmission : MonoBehaviour {

	public ParticleSystem emitPS;
	private ParticleSystem.EmissionModule emitEM;

	void Awake () {
		emitPS = GetComponent<ParticleSystem> ();
		emitEM = emitPS.emission;
	}

	public void stopEmission(){
		//emitEM.rate = new ParticleSystem.MinMaxCurve (0);
		emitEM.rateOverTime = 0f;
	}
}
