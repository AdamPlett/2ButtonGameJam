using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public GameObject playerInstance;
    public Transform playerTransform;
    public Vector3 playerForward;
    public Rigidbody2D playerRB;

    [Header("Movement Variables")]
    [SerializeField] float rotateSpeed;
    [SerializeField] float knockbackForce;

    [Header("Booster Variables")]
    [SerializeField] float boosterForce;
    [SerializeField] float boosterFuel;
    [SerializeField] float boosterDepletion;  
    [SerializeField] float boosterRegeneration;
    [SerializeField] GameObject booster;
    [SerializeField] bool boosterActive;

    [Header("Weapons")]
    [SerializeField] PlayerWeapons weapons;

    private void Start()
    {
        ActivateBooster(false);
    }

    private void FixedUpdate()
    {
        if(boosterActive)
        {
            DepleteBooster();
        }
        else
        {
            ChargeBooster();
        }
    }

    private void ChargeBooster()
    {
        if(boosterFuel <= 100)
        {
            boosterFuel += (boosterRegeneration * Time.deltaTime);
        }
    }

    private void DepleteBooster()
    {
        if(boosterFuel > 0)
        {
            boosterFuel -= (boosterDepletion * Time.deltaTime);
        }
    }

    public void KnockbackPlayer(Vector3 velocity)
    {
        //Debug.Log("Knocking back player, with a force of " + velocity);

        playerRB.AddForce(velocity);
    }

    public void UsePowerup()
    {
        Debug.Log("ACTIVATE POWER-UP");
    }

    public void UseBoost()
    {
        if(boosterFuel > 0)
        {
            playerRB.AddForce(GameManager.gm.player.playerForward * boosterForce);
        }
    }

    public void ActivateBooster(bool activate)
    {
        booster.SetActive(activate);

        boosterActive = activate;
    }

    public void FireLeft()
    {
        foreach (var slot in weapons.leftWeapons)
        {
            slot.ShootWeapons();
        }
    }

    public void FireRight()
    {
        foreach (var slot in weapons.rightWeapons)
        {
            slot.ShootWeapons();
        }
    }

    public void RotateLeft()
    {
        playerTransform.Rotate(0, 0, rotateSpeed);

        playerForward = gameObject.transform.up;
    }

    public void RotateRight()
    {
        playerTransform.Rotate(0, 0, rotateSpeed * -1f);

        playerForward = gameObject.transform.up;
    }
}
