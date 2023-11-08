using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    // ACCESS "soWeapon" TO MODIFY GUN STATS
    public soWeapon weaponType;

    [SerializeField] int ammoCount;
    [SerializeField] float reloadTimer;

    [SerializeField] bool canFire;
    [SerializeField] float fireTimer;

    private void Start()
    {
        ammoCount = weaponType.clipCapacity;
        reloadTimer = 0f;
        canFire = true;
        fireTimer = 0f;
    }

    void Update()
    {
        if (fireTimer > 0)
        {
            canFire = false;
            fireTimer -= Time.deltaTime;
        }
        else
        {
            canFire = true;
            fireTimer = 0f;
        }
    }

    #region General Shooting

    public void Shoot(Vector3 bulletForward, Transform weaponBarrel)
    {
        Vector3 bulletVelocity = GetBulletVelocity(bulletForward);

        CheckCanFire();

        if(canFire)
        {
            DepleteAmmo(weaponType.bulletsPerShot);

            // CHECKS THE FIRE PATTERN OF THE WEAPON AND CALLS CORRESPONDING SHOOT FUNCTION
            switch (weaponType.shootingPattern)
            {
                case eShot.straightShot:
                    ShootStraightShot(bulletVelocity, weaponBarrel);
                    break;

                case eShot.spread:
                    ShootSpreadShot(bulletVelocity, weaponBarrel);
                    break;

                case eShot.cone:
                    ShootConeShot(bulletVelocity, weaponBarrel);
                    break;

                case eShot.sweeping:
                    //ShootSweepingShot(bulletVelocity);
                    break;

                case eShot.beam:
                    //ShootBeam(bulletVelocity);
                    break;
            }
        }
        else
        {
            StartCoroutine(Reload());
        }

        GameManager.gm.player.KnockbackPlayer(bulletForward * weaponType.recoilForce * -1f);
    }

    // MULTIPLIES BULLET SPEED BY THE WEAPON BARREL'S "FORWARD" TO GET BULLET VELOCITY
    private Vector3 GetBulletVelocity(Vector3 direction)
    {
        return 100f * weaponType.bulletSpeed * direction;
    }

    private void CheckCanFire()
    {
        if(ammoCount > 0)
        {
            if (fireTimer > 0)
            {
                canFire = false;
                fireTimer -= Time.deltaTime;
            }
            else
            {
                canFire = true;
                fireTimer = 0f;
            }
        }
        else
        {
            canFire = false;
        }
    }

    #endregion

    #region Ammo & Reloading

    private void DepleteAmmo(int bullets)
    {
        ammoCount -= bullets;

        fireTimer = weaponType.fireRate;
    }

    IEnumerator Reload()
    {
        reloadTimer = weaponType.reloadTime;

        while(reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;
            
            yield return null;
        }

        ammoCount = weaponType.clipCapacity;
    }

    #endregion


    // SPECIFIC FUNCTIONS FOR FIRING PATTERNS

    #region Straight-Shot

    // WEAPONS THAT FIRE BULLETS IN A STRIGHT LINE (EX: STANDARD RIFLE)
    private void ShootStraightShot(Vector3 velocity, Transform spawn)
    {
        StartCoroutine(StraightShot(velocity, spawn));
    }

    IEnumerator StraightShot(Vector3 bulletVelocity, Transform bulletSpawn)
    {       
        for(int i = 0; i < weaponType.bulletsPerShot; i++)
        {
            GameObject bulletInstance = Instantiate(weaponType.bulletPrefab, bulletSpawn);
            Rigidbody2D bulletRB = bulletInstance.GetComponent<Rigidbody2D>();

            //sets the bullets damage
            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            bullet.SetDamage(weaponType.bulletDamage);

            bulletInstance.transform.parent = null;
            bulletRB.AddForce(bulletVelocity);
            Destroy(bulletInstance, weaponType.bulletTravelTime);

            yield return new WaitForSeconds(0.1f);
        }
    }

    #endregion

    #region Spread-Shot

    // WEAPONS THAT SHOOT SEVERAL LINES OF BULLETS IN A CONE-LIKE FASHION (EX: SHOTGUN)
    private void ShootSpreadShot(Vector3 velocity, Transform spawn)
    {
        StartCoroutine(SpreadShot(velocity, spawn));
    }

    IEnumerator SpreadShot(Vector3 bulletVelocity, Transform bulletSpawn)
    {             
        Vector3 angle1, angle2 = Vector3.zero;

        for (int i = 0; i < weaponType.bulletsPerShot; i++)
        {
            for (int j = 0; j < weaponType.linesOfFire; j++)
            {
                angle1 = Quaternion.AngleAxis(weaponType.fireAngle * j, Vector3.forward) * bulletVelocity;
                angle2 = Quaternion.AngleAxis(-1f * weaponType.fireAngle * j, Vector3.forward) * bulletVelocity;

                // Remove the * j from above for wild weapon type

                GameObject bulletInstance1 = Instantiate(weaponType.bulletPrefab, bulletSpawn);
                Rigidbody2D bulletRB1 = bulletInstance1.GetComponent<Rigidbody2D>();

                //sets the bullets damage
                Bullet bullet1 = bulletInstance1.GetComponent<Bullet>();
                bullet1.SetDamage(weaponType.bulletDamage);

                bulletInstance1.transform.parent = null;

                bulletRB1.AddForce(angle1);
                Destroy(bulletInstance1, weaponType.bulletTravelTime);

                GameObject bulletInstance2 = Instantiate(weaponType.bulletPrefab, bulletSpawn);
                Rigidbody2D bulletRB2 = bulletInstance2.GetComponent<Rigidbody2D>();

                //sets the bullets damage
                Bullet bullet2 = bulletInstance2.GetComponent<Bullet>();
                bullet2.SetDamage(weaponType.bulletDamage);

                bulletInstance2.transform.parent = null;
                bulletRB2.AddForce(angle2);
                Destroy(bulletInstance2, weaponType.bulletTravelTime);

                //yield return new WaitForSeconds  << Crazy Weapon Variation
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    #endregion

    #region Cone-Shot

    // WEAPONS THAT SHOOTS BULLET AT A RANDOM PATH WITHIN A SPECIFIED CONE
    private void ShootConeShot(Vector3 velocity, Transform spawn)
    {
        StartCoroutine(ShootCone(velocity, spawn));
    }

    IEnumerator ShootCone(Vector3 bulletVelocity, Transform bulletSpawn)
    {
        for (int i = 0; i < weaponType.linesOfFire; i++)
        {
            for(int j = 0; j < weaponType.bulletsPerShot; j++)
            {
                float randomAngle = Random.Range(weaponType.coneAngle * -1f, weaponType.coneAngle);

                Vector3 angle = Quaternion.AngleAxis(randomAngle, Vector3.forward) * bulletVelocity;

                GameObject bulletInstance = Instantiate(weaponType.bulletPrefab, bulletSpawn);
                Rigidbody2D bulletRB = bulletInstance.GetComponent<Rigidbody2D>();

                //sets the bullets damage
                Bullet bullet = bulletInstance.GetComponent<Bullet>();
                bullet.SetDamage(weaponType.bulletDamage);

                bulletInstance.transform.parent = null;
                bulletRB.AddForce(angle);
                Destroy(bulletInstance, weaponType.bulletTravelTime);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    #endregion

    #region Sweep-Shot

    // WEAPONS THAT SHOOT IN A SWEEPING PATTERN (EX: MACHINE GUN)
    private void ShootSweepingShot(Vector3 velocity, float sweepAngle)
    {

    }

    IEnumerator SweepingShot(int numBullets, Vector3 bulletVelocity, Transform bulletSpawn)
    {
        Vector3 angle1 = Quaternion.AngleAxis(weaponType.coneAngle, Vector3.forward) * bulletVelocity;
        Vector3 angle2 = Quaternion.AngleAxis(-1f * weaponType.coneAngle, Vector3.forward) * bulletVelocity;

        Vector3 angle = Vector3.zero;

        for (int i = 0; i < weaponType.bulletsPerShot; i++)
        {
            for (int j = 0; j < weaponType.linesOfFire; j++)
            {
                GameObject bulletInstance = Instantiate(weaponType.bulletPrefab, bulletSpawn);
                Rigidbody2D bulletRB = bulletInstance.GetComponent<Rigidbody2D>();

                //sets the bullets damage and piercing
                Bullet bullet = bulletInstance.GetComponent<Bullet>();
                bullet.SetDamage(weaponType.bulletDamage);
                if (weaponType.piercing) bullet.setPiercing(weaponType.piercing);

                bulletInstance.transform.parent = null;
                bulletRB.AddForce(angle);
                Destroy(bulletInstance, weaponType.bulletTravelTime);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    #endregion

    #region Beam

    // WEAPONS THAT SHOOT ONE, CONINOUS BEAM RATHER THAN INDIVIDUAL BULLETS (EX: FLAMETHROWER)
    private void ShootBeam(Vector3 velocity, float beamRange, float beamWidth)
    {

    }

    IEnumerator Beam(int numBullets, Vector3 bulletVelocity, Transform bulletSpawn)
    {
        for (int i = 0; i < numBullets; i++)
        {
            
        }

        yield return null;
    }

    #endregion

}
