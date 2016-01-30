using UnityEngine;
using System.Collections;

public class FallingRock : MonoBehaviour {

	public GameObject particleEffectRef;
	public GameObject painSphereRef; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Ground") {
			//Instantiate(particleEffectRef, transform.position, Quaternion.identity);
			//Instantiate(painSphereRef, transform.position, Quaternion.identity);
			Destroy (gameObject); 
		}

	}
}
