using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public GameObject playerInstance;
    public Transform playerTransform;
    [SerializeField] private Rigidbody2D playerRB;

    [Header("Player Variables")]
    [SerializeField] float rotateSpeed;
    [SerializeField] float knockbackForce;

    [Header("Weapons")]
    [SerializeField] PlayerWeapons weapons;

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

    public void KnockbackPlayer(float knockbackForce, Vector3 direction)
    {
        Vector3 velocity =  direction * knockbackForce;

        playerRB.AddForce(velocity);
    }

    public void RotateLeft()
    {
        playerTransform.Rotate(0, 0, rotateSpeed);
    }

    public void RotateRight()
    {
        playerTransform.Rotate(0, 0, rotateSpeed * -1f);
    }
}
