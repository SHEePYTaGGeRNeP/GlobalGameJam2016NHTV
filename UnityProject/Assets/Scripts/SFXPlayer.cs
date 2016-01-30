using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFXPlayer : MonoBehaviour {
	public List<AudioClip> bossMoansL = new List<AudioClip>();
	public List<AudioClip> impactsL = new List<AudioClip>();


	public List<AudioClip> playerMoansL = new List<AudioClip>();
	public List<AudioClip> hitSplashesL = new List<AudioClip>();
	public List<AudioClip> attacksL = new List<AudioClip>();
	public List<AudioClip> footstepsL = new List<AudioClip>();

	public AudioClip raiseShield;
	public AudioClip blockAttack;

	private AudioSource audio; 

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BossMoansF() {
		audio.clip = bossMoansL[Random.Range( 0, bossMoansL.Count)];
		audio.Play();
	}
	public void ImpactsF() {
		audio.clip = impactsL[Random.Range( 0, impactsL.Count)];
		audio.Play();
	}


	public void PlayerMoansF() {
		audio.clip = playerMoansL[Random.Range( 0, playerMoansL.Count)];
		audio.Play();
	}
	public void HitSplashesF() {
		audio.clip = hitSplashesL[Random.Range( 0, hitSplashesL.Count)];
		audio.Play();
	}
	public void PlayerAttackF() {
		audio.clip = attacksL[Random.Range( 0, attacksL.Count)];
		audio.Play();
	}
	public void PlayerFootstepsF() {
		audio.clip = footstepsL[Random.Range( 0, footstepsL.Count)];
		audio.Play();
	}

	public void RaiseShieldF() {
		audio.clip = raiseShield;
		audio.Play();
	}
	public void BlockAttackF() {
		audio.clip = blockAttack;
		audio.Play();
	}















}
