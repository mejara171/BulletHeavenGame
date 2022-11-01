using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    //SFX Handler
    public AudioSource throwKnifeAS;
    public AudioSource collectXPAS;
    public AudioSource playerHitAS;
    public AudioSource levelUp;

    public void PlayThrowKnifeSFX()
    {
        throwKnifeAS.Play();
    }

    public void PlayCollectXPSFX()
    {
        collectXPAS.Play();
    }

    public void PlayPlayerHitSFX()
    {
        if(playerHitAS !=null)
            playerHitAS.Play();
    }

    public void PlayLevelUP()
    {
        levelUp.Play();
    }

}
