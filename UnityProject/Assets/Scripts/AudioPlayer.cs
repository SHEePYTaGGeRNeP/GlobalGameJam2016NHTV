using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour {

	public AudioClip levelStartMusic, bossMusicLoop, playerFightMusicLoop;
	public List<AudioClip> listSFX = new List<AudioClip>();
	private AudioSource audio; 

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		StartCoroutine("PlayStartMusic");


	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)){
			PlayerFightMusicStart (); 
		}
	}

	IEnumerator PlayStartMusic() {
		audio.clip = levelStartMusic;
		audio.Play();
		yield return new WaitForSeconds(audio.clip.length);
		audio.clip = bossMusicLoop;
		audio.Play();
	}

	public void PlayerFightMusicStart(){
		audio.clip = playerFightMusicLoop;
		audio.Play();
	}


}
