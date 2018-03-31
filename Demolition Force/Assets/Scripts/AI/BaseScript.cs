//Samuel D Silverman
//

using UnityEngine;
using System.Collections;

public class BaseScript : MonoBehaviour {

	//check for collision
	//spawn # of enemies
	//check for alle enemies destroyed
	//spawn boss
	//check for boss death
	public GameObject enemy;
	public GameObject boss;
	public Transform basePatrolPathGroup;

	public int numOfEnemiesSpawn;
	public int numOfWavesSpawn;
	public float spawnFrequency;
	public float waveFrequency;

	public Color baseColorWin;
	public Material baseColor;

	bool baseAlarm = false;

	Vector3[] spawnPos;
	int currentSpawnPos;
	float longestSpawnLength;
	public GameObject childCheckObj;
	GameObject[] baseEnemies;
	int waveCounter = 0;
	bool nextWaveGo;
	bool checkWaves;
	GameObject player;
	float[] distance;


	void Start () {

		AssignSpawnPoints ();
		distance = new float[basePatrolPathGroup.childCount];
	}
	
	void Update ()
	{

		//checks to see when wave is destroyed
		CheckWaveDestroyed ();

		//execute spawn enemies when wave is ready to go
		if (nextWaveGo) {
			StartCoroutine (SpawnEnemies ());
			nextWaveGo = false;
		}
		if (baseAlarm) { //if alarm is happening than update the spawn position
			player = GameObject.FindGameObjectWithTag ("Player");//find player to measure spawn distance
			UpdateSpawnPos();
		}

	}
	//Check to see if player has attacked base
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bullet") {
			if (baseAlarm == false) {

				StartCoroutine (SpawnEnemies ());
				baseAlarm = true;
				nextWaveGo = false;
			}
		} 
	}
	//checks to see when wave is done and reinstitaes new wave or boss.
	void CheckWaveDestroyed()
	{
		if (baseAlarm) {
			if (checkWaves) {
				baseEnemies = GameObject.FindGameObjectsWithTag ("BaseEnemy");

				for (int i = 0; i <= baseEnemies.Length; i++) {

					if (baseEnemies.Length == 0) {//once  all enemies are gone form the wave, start new wave
						checkWaves = false;
						//no more enemies
						//run wave again
						if (waveCounter == numOfWavesSpawn) {
							//boss time
							StartCoroutine(SpawnBoss());
						} else if (waveCounter < numOfWavesSpawn) {
							nextWaveGo = true;
							//spawn new wave
						} else if (waveCounter > numOfWavesSpawn) {
							baseAlarm = true;
							//execute win conditions
							baseColor.color = baseColorWin;
						}
							
					} 
				}
			}
		}
	}

	public IEnumerator SpawnEnemies()
	{
		int i = 0;

		yield return new WaitForSeconds (waveFrequency); //after wave is destroyed, wait X amount of seconds

		waveCounter++;

		while (i < numOfEnemiesSpawn) {
			GameObject spawnedEnemy = Instantiate (enemy, spawnPos[currentSpawnPos], Quaternion.identity) as GameObject;
			spawnedEnemy.GetComponent<AITankControl> ().pathGroup = basePatrolPathGroup;
			GameObject spawnedChildCheckObj = Instantiate (childCheckObj, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			spawnedChildCheckObj.gameObject.transform.parent = spawnedEnemy.transform;
			checkWaves = true;

			longestSpawnLength = 0;

			yield return new WaitForSeconds (spawnFrequency);
			i++;
		}

		yield return new WaitForSeconds (1f);
	}
	public IEnumerator SpawnBoss()
	{

		waveCounter++;

		yield return new WaitForSeconds (waveFrequency); //after wave is destroyed, wait X amount of seconds


		GameObject spawnedEnemy = Instantiate (boss, spawnPos[currentSpawnPos], Quaternion.identity) as GameObject;
		spawnedEnemy.GetComponent<AITankControl> ().pathGroup = basePatrolPathGroup;
		GameObject spawnedChildCheckObj = Instantiate (childCheckObj, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		spawnedChildCheckObj.gameObject.transform.parent = spawnedEnemy.transform;
		yield return new WaitForSeconds (waveFrequency);
		checkWaves = true;



	}

	void UpdateSpawnPos()
	{
		for (int i = 0; i < spawnPos.Length; i++) {
			distance[i] = Vector3.Distance (spawnPos [i], player.transform.position);

			if (distance[i] > longestSpawnLength) {
				longestSpawnLength = distance[i];
				currentSpawnPos = i;
				//print ("spawn Point " + spawnPos [i] + " is the furthest from player at " + longestSpawnLength);
			}

			//print (spawnPos [i] + "distance :" + distance);
		}
	}

	void AssignSpawnPoints()
	{
		spawnPos = new Vector3[basePatrolPathGroup.childCount];

		for(int i = 0; i < basePatrolPathGroup.childCount;i++){
			spawnPos[i] = basePatrolPathGroup.GetChild (i).transform.position;
		}
	}



}
