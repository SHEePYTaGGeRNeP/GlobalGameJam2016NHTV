using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour {
	
	public float TimeToDestroy = 5f; 
	float currentTime = 0; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime; 
		
		if (currentTime >= TimeToDestroy)
			Destroy (this.gameObject); 
	}
}
