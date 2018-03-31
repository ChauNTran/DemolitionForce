using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour {

	public GameObject firerateText;
	public GameObject armorText;
	public GameObject damageText;

	RectTransform FRFiller;
	RectTransform DMFiller;
	RectTransform ARFiller;

	void Awake ()
	{
		if (damageText == null)
		{
			firerateText = GameObject.Find ("Firerate_text");
			armorText = GameObject.Find ("Armor_text");
			damageText = GameObject.Find ("Ammo_text");
		}

		FRFiller = firerateText.transform.Find ("Background/Filler").GetComponent<RectTransform> ();
		DMFiller = damageText.transform.Find ("Background/Filler").GetComponent<RectTransform> ();
		ARFiller = armorText.transform.Find ("Background/Filler").GetComponent<RectTransform> ();
	}

	void Start()
	{
		RefreshStatus ();
	}

	public void RefreshStatus()
	{
		switch(PlayerPrefsManager.GetTurretLevel())
		{
			case 1:
				FRFiller.sizeDelta = new Vector2 (120, 30f);
				break;
			case 2:
				FRFiller.sizeDelta = new Vector2 (160f, 30f);
				break;
			case 3:
				FRFiller.sizeDelta = new Vector2 (200f, 30f);
				break;
			default:
				FRFiller.sizeDelta = new Vector2 (80, 30f);
				break;
		}

		switch(PlayerPrefsManager.GetArmorLevel())
		{
		case 1:
			ARFiller.sizeDelta = new Vector2 (120, 30f);
			break;
		case 2:
			ARFiller.sizeDelta = new Vector2 (160f, 30f);
			break;
		case 3:
			ARFiller.sizeDelta = new Vector2 (200f, 30f);
			break;
		default:
			ARFiller.sizeDelta = new Vector2 (80, 30f);
			break;
		}

		switch(PlayerPrefsManager.GetDamageLevel())
		{
		case 1:
			DMFiller.sizeDelta = new Vector2 (120, 30f);
			break;
		case 2:
			DMFiller.sizeDelta = new Vector2 (160f, 30f);
			break;
		case 3:
			DMFiller.sizeDelta = new Vector2 (200f, 30f);
			break;
		default:
			DMFiller.sizeDelta = new Vector2 (80, 30f);
			break;
		}
	}
}
