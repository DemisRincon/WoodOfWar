using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioClip playerHitSound, seedSound, jumpSound, bottleDeathSound;
    static AudioSource audioSrc;

	// Use this for initialization
	void Start () {
        playerHitSound = Resources.Load<AudioClip>("newSoundEffects/playerHit");
        seedSound = Resources.Load<AudioClip>("playerHit");
        jumpSound = Resources.Load<AudioClip>("playerHit");
        bottleDeathSound = Resources.Load<AudioClip>("playerHit");

        audioSrc = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	} 

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "seed":
                audioSrc.PlayOneShot(seedSound);
                break;
            case "playerHit":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "bottleDeath":
                audioSrc.PlayOneShot(bottleDeathSound);
                break;
        }
    }
}
