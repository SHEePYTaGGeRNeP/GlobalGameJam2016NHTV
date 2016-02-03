using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusBarFunction: MonoBehaviour {

	Scrollbar scrollbarRef; 
	public float statusAmount; 

	// Use this for initialization
	void Start () {
		scrollbarRef = GetComponent<Scrollbar> (); 
	}
	
	// Update is called once per frame
	void Update () {
		scrollbarRef.size = statusAmount; 
	}
}
