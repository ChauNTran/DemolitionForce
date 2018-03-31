using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GubsUI : MonoBehaviour {

	Text GubsTotal;
	GameObject GubsValue;
	RectTransform StartPoint;

	bool moveText = false;
	float timer = 0f;

	void Awake ()
	{
		GubsTotal = transform.Find("Gubs_text/Gubs_number").GetComponent<Text>();
		GubsValue = transform.Find ("Gubs_text/Gubs_value").gameObject;
		StartPoint = transform.Find ("Gubs_text/Start_point").GetComponent<RectTransform>();
	}

	void Update()
	{
		RefreshText ();
		if (moveText) {
			timer += Time.deltaTime;
			float newYValue = GubsValue.GetComponent<RectTransform> ().anchoredPosition.y;
			newYValue += 1.5f;
			GubsValue.GetComponent<RectTransform> ().anchoredPosition = new Vector3(GubsValue.GetComponent<RectTransform> ().anchoredPosition.x,newYValue,0f);
			if (timer >= 2f) {
				moveText = false;
				GubsValue.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (1000f,1000f);
				timer = 0f;
			}
		}
	}

	void RefreshText()
	{
		GubsTotal.text = PlayerPrefsManager.GetGubs ().ToString();
	}

	public void ShowValue(int gubsValue)
	{
		GubsValue.GetComponent<Text>().text = gubsValue.ToString ();
		GubsValue.GetComponent<RectTransform> ().anchoredPosition = StartPoint.anchoredPosition;
		moveText = true;
	}
}
