using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Feedback : MonoBehaviour {
	public AudioClip[] bossMoansA;
	public AudioClip[] bossDeathMoansA;
	public AudioClip[] impactsA; 

	public AudioClip[] playerMoansA;
	public AudioClip[] hitSplashesA;
	public AudioClip[] attacksA;
	public AudioClip[] footstepsA;

	public AudioClip raiseShield;
	public AudioClip blockAttack;

	private AudioSource audio; 

	public GameObject meshToBlink; 
	public Material blinkMaterial;
    Material defaultMaterial; 

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();

        if(meshToBlink != null)
            defaultMaterial = meshToBlink.GetComponent<Renderer>().material; 
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayBossMoansF() {
		audio.clip = bossMoansA[Random.Range( 0, bossMoansA.Length)];
		audio.Play();
	}
	public void PlayBossDeathMoansF() {
		audio.clip = bossDeathMoansA[Random.Range( 0, bossDeathMoansA.Length)];
		audio.Play();
	}
	public void PlayImpactsF() {
		audio.clip = impactsA[Random.Range( 0, impactsA.Length)];
		audio.Play();
	}


	public void PlayPlayerMoansF() {
		audio.clip = playerMoansA[Random.Range( 0, playerMoansA.Length)];
		audio.Play();
	}
	public void PlayHitSplashesF() {
		audio.clip = hitSplashesA[Random.Range( 0, hitSplashesA.Length)];
		audio.Play();
	}
	public void PlayPlayerAttackF() {
		audio.clip = attacksA[Random.Range( 0, attacksA.Length)];
		audio.Play();
	}
	public void PlayPlayerFootstepsF() {
		audio.clip = footstepsA[Random.Range( 0, footstepsA.Length)];
		audio.Play();
	}

	public void PlayRaiseShieldF() {
		audio.clip = raiseShield;
		audio.Play();
	}
	public void PlayBlockAttackF() {
		audio.clip = blockAttack;
		audio.Play();
	}


	public void StartInvincibilityMaterialF(float invincibilityTimer){
        meshToBlink.GetComponent<Renderer>().material = blinkMaterial;
        Invoke("SetDefaultMaterialF", invincibilityTimer);

    }


	void SetDefaultMaterialF() {
        meshToBlink.GetComponent<Renderer>().material = defaultMaterial;
    }













}
