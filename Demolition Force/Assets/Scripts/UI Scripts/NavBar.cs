using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Location
{
	public string name;
	public Transform locationTransform;
	public RectTransform rect;
}

public class NavBar : MonoBehaviour {

	public Transform turretPosition;
	public List<Location> locations;
	Camera mainCam;

	void Start ()
	{
		mainCam = Camera.main;
	}
	
	void Update ()
	{
		foreach (Location loc in locations)
		{
			ShowIcon (loc.locationTransform, loc.rect);
		}
	}

	void ShowIcon (Transform placeTranform, RectTransform icon)
	{
		Vector3 toPlayer = placeTranform.position - turretPosition.position;
		float angle = Vector3.Angle (toPlayer, turretPosition.GetChild(0).forward);

		if (angle < 40)
		{
			icon.gameObject.SetActive (true);
			Vector3 screenPosition = mainCam.WorldToScreenPoint (placeTranform.position);
			Vector2 ImagePosition = new Vector2 (screenPosition.x, icon.position.y);
			icon.position = ImagePosition;
			if (toPlayer.magnitude > 200f) {
				icon.localScale = new Vector3 (0.5f, 0.5f, 1f);
			} else if (toPlayer.magnitude <= 200f && toPlayer.magnitude > 50f) {
				float ratio = 0.5f / 150f;
				float offset = (toPlayer.magnitude - 50f) * ratio;
				icon.localScale = new Vector3 (1f - offset, 1f - offset, 1f);
			} else if (toPlayer.magnitude <= 50f) {
				icon.localScale = new Vector3 (1f, 1f, 1f);
			}
				
		}
		else
			icon.gameObject.SetActive (false);
	}
}
