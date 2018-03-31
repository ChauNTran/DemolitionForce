//	chau tran
// where to put:	tank AI
// purpose:			rotate the turret to the player

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(TankShoot))]
public class AITurret : MonoBehaviour {

	public Transform turretProtector;
	public Transform turretMain;
	public float turretRotSpeed;
	public float sightRange = 50f;	// how far can the AI tank sees
	EnemyTankShoot enemyTankShoot;
	Transform player;
	Vector3 positionOffset;
	Vector3 playerPositionOffset;
	Vector3 distance;
	public Vector3 playerLastSeen;
	float timer = 0f;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		enemyTankShoot = GetComponent<EnemyTankShoot> ();
		playerLastSeen = new Vector3 (0, 0, 0);
	}

	void Update ()
	{
		if(player != null){//if player object isn't present
			// the tank object has pivot at (0,0,0). So we should offset it to (0,1,0) so that it's above the ground
			positionOffset = new Vector3 (transform.position.x, transform.position.y + 1.5f, transform.position.z);
			playerPositionOffset = new Vector3 (player.position.x, player.position.y + 1.5f,player.position.z);

			distance = playerPositionOffset - positionOffset;
			if (distance.magnitude <= sightRange)
			{
				RaycastHit hit;
				Debug.DrawRay (positionOffset,distance,Color.green);
				if (Physics.Raycast (positionOffset, distance.normalized, out hit, distance.magnitude))
				if (hit.collider.tag == "Player") {
					PointTurretAtPlayer ();
					ShootPlayer ();
					playerLastSeen = player.position;
				}
				else
					playerLastSeen = new Vector3 (0, 0, 0);
			}
		}else{
			return;
		}
	}

	void ShootPlayer()
	{
		timer += Time.deltaTime;

		if (enemyTankShoot.holdAndShoot)
		{
			if (timer > enemyTankShoot.GetFireRate())
			{
				enemyTankShoot.Shoot ();
				timer = 0f;
			}
		}
		else
			if (timer > enemyTankShoot.GetFireRate())
			{
				enemyTankShoot.Shoot ();
				timer = 0f;
			}
	}

	void PointTurretAtPlayer()
	{
		//gets direction rotation from player to transform position
		Vector3 relativePos = player.position - transform.position;
		Quaternion newRotation = Quaternion.LookRotation (relativePos);
		//updates rotation of turret base connected to cameras y axis angle
		Quaternion rotation = Quaternion.Slerp (turretProtector.transform.rotation, Quaternion.Euler(0,newRotation.eulerAngles.y,0), Time.deltaTime * turretRotSpeed);

		turretProtector.transform.rotation = Quaternion.Euler (turretProtector.transform.parent.eulerAngles.x, rotation.eulerAngles.y , turretProtector.transform.parent.eulerAngles.z);
		//resets local rotation x & z to 0 so the turret stays planted on tank
		turretProtector.localRotation = Quaternion.Euler(new Vector3(0,turretProtector.transform.localEulerAngles.y,0));

		turretMain.transform.LookAt (playerPositionOffset);
		//clamps x rotation from looking down
		if (turretMain.transform.localRotation.x < -10f) {
			turretMain.localRotation = Quaternion.Euler(new Vector3(-10f,turretMain.localRotation.y,turretMain.localRotation.z));
		}

	}

}
