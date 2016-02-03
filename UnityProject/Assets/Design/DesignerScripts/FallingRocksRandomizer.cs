using UnityEngine;
using System.Collections;

public class FallingRocksRandomizer : MonoBehaviour {

	public float fallingHeight = 20f;
	public float fallHeightRandomOffset = 15; 
	//public float xRange, zRange; 
	public float minTransformX, maxTransformX; 
	public float minTransformZ, maxTransformZ; 

	public int minSpawnBulk = 3;
	public int maxSpawnBulk = 10; 

	int currentlySpawned = 0; 

	public GameObject fallingRockRef; 

	private int[] upsiteDownA = new int[] {0, 180};

	public bool haveRandom = true; 
	public float minRandomTimer = 5f;
	public float maxRandomTimer = 10f; 
	float randomTimerAmount; 
	float currentRandomTimer; 
	bool randomTimerisOn = false;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			SpawnMultipleF (); 
		}
		if (haveRandom == true) {
			if (randomTimerisOn == false) {
				randomTimerAmount = Random.Range (minRandomTimer, maxRandomTimer); 
				randomTimerisOn = true; 
			} else {
				if (randomTimerAmount > currentRandomTimer) {
					currentRandomTimer += Time.deltaTime; 
				}
				if (currentRandomTimer >= randomTimerAmount) {
					randomTimerisOn = false; 
					currentRandomTimer = 0; 
					SpawnMultipleF ();

				}
			}

		}

	}

	void SpawnMultipleF(){
		int spawnAmount = Random.Range (minSpawnBulk, maxSpawnBulk); 

		while (spawnAmount > currentlySpawned) {
			SpawnRandomLocationF (); 
			currentlySpawned += 1; 
		}
		if (currentlySpawned >= spawnAmount)
			currentlySpawned = 0; 
	}

	void SpawnRandomLocationF(){
		Vector3 SpawnLocation = new Vector3 (Random.Range (minTransformX, maxTransformX), (fallingHeight + Random.Range (0,fallHeightRandomOffset)), Random.Range (minTransformZ, maxTransformZ)); 
		Instantiate(fallingRockRef, SpawnLocation, Quaternion.Euler(upsiteDownA[Random.Range( 0, upsiteDownA.Length)], Random.Range(0,360), 0));
	}

} 
