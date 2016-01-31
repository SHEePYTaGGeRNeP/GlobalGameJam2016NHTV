using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void resetLevel(){
		Time.timeScale = 1; 
		Application.LoadLevel("MainGame");

		
		
		
	}
	public void quitGame(){
		Application.Quit(); 
	}
	
}
