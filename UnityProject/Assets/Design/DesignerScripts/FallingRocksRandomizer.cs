using UnityEngine;
using System.Collections;

public class FallingRocksRandomizer : MonoBehaviour {

	public float fallingHight = 20f; 
	public float xRange, zRange; 
	float transformX, transformZ; 

	public GameObject fallingRockRef; 


	// Use this for initialization
	void Start () {
		transformX = transform.position.x + xRange;
		transformZ = transform.position.z + zRange;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			SpawnRandomLocationF (); 
		}
	}



	void SpawnRandomLocationF(){
		Vector3 SpawnLocation = new Vector3 (Random.Range (transformX, -transformX), fallingHight, Random.Range (transformZ, -transformZ)); 
		Instantiate(fallingRockRef, SpawnLocation, Quaternion.identity);
	}

}
