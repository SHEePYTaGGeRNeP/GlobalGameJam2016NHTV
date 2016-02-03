using UnityEngine;
using System.Collections;

public class FallingRock : MonoBehaviour {

	public GameObject particleEffectRef;
	public Feedback feedbackRef; 

	float timer; 
	bool timerStart = false; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timerStart == true) {
			timer += Time.deltaTime;
			if (timer >= 0.5f)
				Destroy (gameObject); 

		}

		if (transform.position.y <= -200)
			Destroy (gameObject); 
		
	}

	void OnTriggerEnter(Collider  collider) {
		if (collider.gameObject.tag == "Ground") {
			Instantiate(particleEffectRef, transform.position, Quaternion.Euler(-90,0,0));
			feedbackRef.PlayImpactsF(); 
			timerStart = true; 
		}

	}
}
