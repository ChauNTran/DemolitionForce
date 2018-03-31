//	chau tran
// where to put:	in the Game UI canvas
// purpose:			show enemy tanks in the radar minimap

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadarNetworking : MonoBehaviour {

	public float SightRange = 60f;
	RectTransform radarTransform;
	Transform player;

	float UIratio;	// the realRange:UIrange ratio

	void Awake()
	{
		radarTransform = transform.Find ("Radar_img").GetComponent<RectTransform> ();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform> ();
	}
	void Start()
	{
		UIratio = (radarTransform.rect.width/SightRange)/2;
	}

	void Update()
	{
		CheckEnemy ();
	}

	void CheckEnemy()
	{
		Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, SightRange,1<<8);// 	ENEMY TANKS NEED NEED TO BE SET TO THE "TANKS" LAYER	

		// Destroy the old dots
		if(radarTransform.childCount > 0)
			foreach (Transform dot in radarTransform) {
				Destroy (dot.gameObject);
			}
		// and make new dots associating with new tanks's position
		for (int i = 0; i < hitColliders.Length; i++)
		{
			Vector3 distance =hitColliders [i].gameObject.transform.position - player.transform.position;
			float angle = Vector3.Angle (distance, player.transform.forward);
			float sign = Mathf.Sign (Vector3.Dot (distance,player.transform.right));// this float shows whether it's on the left or right. If it's positive, it's on the right. Otherwise, it's on the left
			MakeDot (distance.magnitude, angle, sign);
		}
	}

	void MakeDot (float distance, float angle, float sign)
	{
		RectTransform dot;

		dot = Instantiate(Resources.Load("dot", typeof (RectTransform)), radarTransform.position, Quaternion.identity) as RectTransform;
		dot.transform.SetParent (radarTransform);
		float tankX = Mathf.Sin (angle * Mathf.Deg2Rad) * distance * sign * UIratio;
		float tankY = Mathf.Cos (angle * Mathf.Deg2Rad) * distance * UIratio;
		dot.anchoredPosition = new Vector2 (tankX, tankY);
	}
}
