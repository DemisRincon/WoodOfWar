using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioClip playerHitSound, seedSound, jumpSound, bottleDeathSound;
    static AudioSource audioSrc;

	// Use this for initialization
	void Start () {
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
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Doran/playerHit"));
                break;
            case "playerHit":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Doran/playerHit"));
                break;
            case "cristalSword":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Doran/sword sound"));
                break;
            case "hSlayerSword":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Doran/MetalHit"));
                break;
            case "jump":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Doran/jumpland"));
                break;
            case "leftStep":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Doran/Paso-izquierdo-doran"));
                break;
            case "rightStep":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Doran/Paso-derecho-doran"));
                break;
            case "bottleDeath":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Bottle Break"));
                break;
            case "canDeath":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Aluminum Can Crush"));
                break;
            case "bagDeath":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/breakingBag"));
                break;
            case "fireball":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/breakingBag"));
                break;
            case "canDamaged":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Monstruos/ogre4_can"));
                break;
            case "glassDamaged":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Monstruos/mnstr1_glass"));
                break;
            case "plasticDamaged":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Monstruos/mnstr9_plastic"));
                break;
            case "humanDamaged":
                audioSrc.PlayOneShot(Resources.Load<AudioClip>("SoundEffects/Monstruos/ogre1_human"));
                break;
        }
    }
}
