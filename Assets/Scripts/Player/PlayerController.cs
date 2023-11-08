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

    [Header("Weapons")]
    [SerializeField] PlayerWeapons weapons;

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
