using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour {

	public AudioClip levelStartMusic, bossMusicLoop, playerFightMusicLoop, victoryMusic, gameOverMusic;
	public List<AudioClip> listSFX = new List<AudioClip>();
	private AudioSource audio; 

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		StartCoroutine("PlayStartMusic");
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown(KeyCode.P)){
		//	PlayerFightMusicStartF (); 
		//}
	}

	IEnumerator PlayStartMusic() {
		audio.clip = levelStartMusic;
		audio.Play();
		yield return new WaitForSeconds(audio.clip.length);
		audio.clip = bossMusicLoop;
		audio.Play();
	}

	public void PlayerFightMusicStartF(){
		audio.clip = playerFightMusicLoop;
		audio.Play();
	}

	public void VictoryMusicF(){
		audio.clip = victoryMusic;
		audio.Play();
	}
	public void GameOverMusicF(){
		audio.clip = gameOverMusic;
		audio.Play();
	}

}
