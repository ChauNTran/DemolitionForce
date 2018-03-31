using UnityEngine;
using System.Collections;

public class GubsScript : MonoBehaviour {

	private int GubsValue;
	public float flashWaitTime;
	private Renderer theRenderer;
	bool flash = false;

	IEnumerator Highlight()
	{
		while (true) {
			yield return new WaitForSeconds (flashWaitTime);
			if (flash != true) {
				theRenderer.material.SetColor ("_EmissionColor", new Color (0.2f, 0.2f, 0.2f));
				flash = true;
			} else {
				theRenderer.material.SetColor ("_EmissionColor", new Color (0.01f, 0.01f, 0.01f));
				flash = false;
			}
		}
	}

	void Start()
	{
		SetGubsValue ();
		theRenderer = GetComponent<Renderer> ();
		StartCoroutine (Highlight ());
	}

	void SetGubsValue()
	{
		GubsValue = Random.Range (100,200);
	}

	int GetGubsValue()
	{
		return GubsValue;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerPrefsManager.AddGubs (GetGubsValue());
			GameObject.Find ("Game UI").GetComponent<GubsUI>().ShowValue(GubsValue);

			Destroy (this.gameObject);
		}
	}
}
