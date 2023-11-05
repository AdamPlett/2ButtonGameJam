using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [Header("Powerup Settings")]
    //time the powerup lasts
    public float duration=10;

    [Header("Setup")]
    /*to hide the visual and collider for powerup while the duration 
    is still active before destorying the powerup*/
    public GameObject visualsToDeactivate;
    public Collider colliderToDeactivate;
    //on pickup SFX
    public AudioSource collectSFX;
    //on pickup VFX
    public ParticleSystem collectVFX;

    //activates the powerup and start a coroutine lasting duration 
    public virtual void Collect()
    {
        Debug.Log("PowerUp collected!");
    }
    //after the powerups duration is over, destroy the gameobject
    public virtual void Destory()
    {
        Debug.Log("PowerUp Destroyed!");
    }
}
