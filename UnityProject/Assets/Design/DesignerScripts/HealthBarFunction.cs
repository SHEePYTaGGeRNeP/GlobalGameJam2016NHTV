using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarFunction : MonoBehaviour {

	Scrollbar scrollbarRef; 
	public float healthFloat; 

	// Use this for initialization
	void Start () {
		scrollbarRef = GetComponent<Scrollbar> (); 
	}
	
	// Update is called once per frame
	void Update () {
		scrollbarRef.size = healthFloat; 
	}
}
