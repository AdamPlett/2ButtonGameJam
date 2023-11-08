using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eShot { straightShot, spread, cone, sweeping, beam}

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapons")]
public class soWeapon : ScriptableObject
{
    [Space(10)] 
    [Header("General Weapon Variables")]

    [Tooltip("Time player must wait between each shot")]
    public float timeBetweenShots;

    [Tooltip("For weapons that shoot multiple bullets at a time, the time between each firing of bullets \n\n If shoots a sigular bullet, or no bullets like a beam weapon, then default to 0")]
    public float timeBetweenBullets;

    [Space(5)]

    [Tooltip("Dictates what pattern bullets travel after being shot")]
    public eShot shootingPattern;

    [Tooltip("Used to adjust firing patterns that use multiple 'paths' to shoot, such as spread & sweep \n\n The higher the linesOfFire, the 'denser' the bullet spray will be \n\n If a straight shot or beam weapon, default to 0")]
    public int linesOfFire;



    [Space(10)] 
    [Header("Ammo & Reloading")]

    [Tooltip("If false, then you can ignore the remaining 3 stats and default them to 0")]
    public bool requiresAmmo;

    [Tooltip("Time player must wait after ammo reaches 0 to reload")]
    public float timeToReload;

    [Tooltip("Amount of ammo each weapon can shoot before needing to reload")]
    public int totalAmmo;

    [Tooltip("Amount of ammo shot each time the fire button is pressed")]
    public int ammoPerShot;



    [Space(10)] 
    [Header("Spread-Specific Variables \n If not a spread weapon, default to 0")] [Space(5)]
    [Space(5)]

    [Tooltip("The angle between bullets")]
    public int angleBetweenBullets;



    [Space(10)]
    [Header("Cone-Specific Variables \n If not a cone weapon, default to 0")]
    [Space(5)]

    [Tooltip("Determines how wide the cone is drawn for bullets to be shot in; The higher the angle, the more spread out bullets will be \n\n Note that cone angle represents only one half of the cone, so the total angle will be 2x 'coneAngle'")]
    public int coneAngle;



    [Space(10)]
    [Header("Sweep-Specific Variables \n If not a sweeping weapon, default to 0")]
    [Space(5)]

    [Tooltip("How wide the sweeping motion goes \n\n Note that cone angle represents only one half of the cone, so the total angle will be 2x 'sweepAngle'")]
    public float sweepAngle;



    [Space(10)] 
    [Header("Beam-Specific Variables \n If not a beam weapon, default to 0")]
    [Space(5)]

    [Tooltip("How far out the beam travels from the ship")]
    public float beamRange;

    [Tooltip("How long the beam stays active after the fire key has been pressed")]
    public float beamDuration;


    [Space(10)] 
    [Header("Bullet Variables")]

    [Tooltip("Base damage each individual bullet does to an enemy")]
    public float bulletDamage;

    [Tooltip("Speed that each individual bullet travels at")]
    public float bulletSpeed;     

    [Tooltip("Time bullet is allowed to travel before it is destroyed (assuming nothing has been hit beforehand) \n\n If a beam weapon, default to 0")]
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

    [Tooltip("If true, bullets will spawn an explosion with high damage and knockback on collision")]
    public bool explodeOnHit;

    [Tooltip("If true, bullets will have a chance to slow / stun enemies on collision")]
    public bool stunOnHit;

    [Space(5)]
    public GameObject bulletPrefab;
}
