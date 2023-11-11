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
    public Collider2D colliderToDeactivate;
    //on pickup SFX
    public AudioSource collectSFX;
    //on pickup VFX
    public ParticleSystem collectVFX;

    public bool isInstantUse = true;
    protected bool movingUp=true;

    public virtual void Awake()
    {
        movingUp = true;
        colliderToDeactivate = GetComponent<Collider2D>();
    }
    public virtual void Update()
    {
        if (movingUp)
        {
            transform.Translate(new Vector3(0, .375f, 0) * Time.deltaTime);
            Invoke(nameof(SetMovingDown), .5f);
        }
        else
        {
            transform.Translate(new Vector3(0, -.375f, 0) * Time.deltaTime);
            Invoke(nameof(SetMovingUp), .5f);
            Debug.Log(movingUp);
        }
    }
    //activates the powerup and start a coroutine lasting duration 
    public virtual void Collect()
    {
        Debug.Log("PowerUp collected!");
    }
    //after the powerups duration is over, destroy the gameobject
    public virtual void DestoryPowerUp()
    {
        Debug.Log("PowerUp Destroyed!");
        Destroy(gameObject);
    }
    public virtual void Use()
    {
        Debug.Log("PowerUp Used");
    }
    protected virtual void SetMovingUp()
    {
        movingUp = true;
    }
    protected virtual void SetMovingDown()
    {
        movingUp = false;
    }
    protected virtual void DisablePowerupInteraction()
    {
        colliderToDeactivate.enabled = false;
        visualsToDeactivate.SetActive(false);
    }
}
