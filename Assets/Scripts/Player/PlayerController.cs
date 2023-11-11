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
    public PlayerHealth playerHealth;

    [Header("Movement Variables")]
    [SerializeField] float rotateSpeed;
    [SerializeField] float knockbackForce;

    [Header("Booster Variables")]
    [SerializeField] float boosterForce;
    public float boosterFuel=100;
    public float maxFuel=100;
    [SerializeField] float boosterDepletion;  
    [SerializeField] float boosterRegeneration;
    [SerializeField] GameObject booster;
    [SerializeField] bool boosterActive;

    [Header("Weapons")]
    public PlayerWeapons weapons;

    private void Start()
    {
        ActivateBooster(false);
        playerForward = gameObject.transform.up;
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
        else
        {
            boosterFuel = maxFuel;
        }
    }

    private void DepleteBooster()
    {
        if(boosterFuel > 0)
        {
            boosterFuel -= (boosterDepletion * Time.deltaTime);
        }
        else
        {
            boosterFuel = 0;
        }
    }
    public void AddBoost(float addedBoost)
    {
        if (boosterFuel + addedBoost < 100)
        {
            boosterFuel += addedBoost;
        }
        else
        {
            boosterFuel = 100;
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
        if(boosterFuel >= 0)
        {
            //booster.SetActive(activate);
            if (boosterFuel == 0) booster.SetActive(false);
            else booster.SetActive(activate);

            boosterActive = activate;
        }
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
