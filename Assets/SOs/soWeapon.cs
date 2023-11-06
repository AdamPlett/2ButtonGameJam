using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eShot { straightShot, tripleShot, scatter, sweeping, beam}

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapons")]
public class soWeapon : ScriptableObject
{
    [Header("Weapon Variables")]

    public float range;             // How far each individual bullet travels before it is destroyed
    public float fireRate;          // How fast each weapon shoots (The higher the fireRate, the less time between individual shots)
    public float clipCapacity;      // Amount of ammo each weapon can shoot before needing to reload
    public float reloadTime;        // Time player must wait after ammo reaches 0 to reload
  
    public eShot shootingPattern;   // Dictates what pattern bullets travel after being shot


    [Header("Bullet Variables")]

    public float bulletDamage;      // Base damage each individual bullet does to an enemy
    public float bulletSpeed;       // Speed that each individual bullet travels at

    public float bulletTravelTime;  // Time bullet is allowed to travel before it is destroyed (assuming nothing has been hit beforehand)

    public float recoilForce;       // Force in which the player is pushed when shooting
    public float projectileForce;   // Force in which enemies are pushed when shot

    [Space(5)]
    public bool piercing;           // If true, bullets will pass through enemies and continue their trajectory
    public bool ignoreArmor;        // If true, bullet damage will ignore armor and apply directly to enemy health

    [Space(5)]
    public GameObject bulletPrefab;
}
