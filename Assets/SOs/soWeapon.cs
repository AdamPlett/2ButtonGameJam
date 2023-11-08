using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eShot { straightShot, spread, sweeping, beam}

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapons")]
public class soWeapon : ScriptableObject
{
    [Header("Weapon Variables")]

    [Tooltip("How far each individual bullet travels before it is destroyed")]
    public float range;

    [Tooltip("Time player must wait between each shot")]
    public float fireRate;          

    [Tooltip("Time player must wait after ammo reaches 0 to reload")]
    public float reloadTime;      

    [Tooltip("Amount of ammo each weapon can shoot before needing to reload")]
    public int clipCapacity;     

    [Tooltip("Number of bullets shot each time the fire button is pressed")]
    public int bulletsPerShot;

    [Tooltip("Dictates what pattern bullets travel after being shot")]
    public eShot shootingPattern;


    [Header("Bullet Variables")]

    [Tooltip("Base damage each individual bullet does to an enemy")]
    public float bulletDamage;

    [Tooltip("Speed that each individual bullet travels at")]
    public float bulletSpeed;     

    [Tooltip("Time bullet is allowed to travel before it is destroyed (assuming nothing has been hit beforehand)")]
    public float bulletTravelTime;

    [Tooltip("Force in which the player is pushed when shooting")]
    public float recoilForce;

    [Tooltip("Force in which enemies are pushed when shot")]
    public float projectileForce; 

    [Space(5)]
    [Tooltip("If true, bullets will pass through enemies and continue their trajectory")]
    public bool piercing; 

    [Tooltip("If true, bullet damage will ignore armor and apply directly to enemy health")]
    public bool ignoreArmor; 

    [Space(5)]
    public GameObject bulletPrefab;
}
