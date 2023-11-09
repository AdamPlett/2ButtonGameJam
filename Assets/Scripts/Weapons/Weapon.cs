using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using Unity.VisualScripting;
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

    private bool isReloading = false;

    private void Start()
    {
        ammoCount = weaponType.totalAmmo;
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
        if (!isReloading)
        {
            if (ammoCount==0) StartCoroutine(Reload());
        }
    }

    #region General Shooting

    public void Shoot(Vector3 bulletForward, Transform weaponBarrel)
    {
        Vector3 bulletVelocity = GetBulletVelocity(bulletForward);
        Vector3 recoilDirection = bulletForward * -1f;

        CheckCanFire();

        if(canFire)
        {
            DepleteAmmo(weaponType.ammoPerShot);

            // CHECKS THE FIRE PATTERN OF THE WEAPON AND CALLS CORRESPONDING SHOOT FUNCTION
            switch (weaponType.shootingPattern)
            {
                case eShot.straightShot:
                    ShootStraightShot(bulletVelocity, weaponBarrel, recoilDirection);
                    break;

                case eShot.spread:
                    ShootSpreadShot(bulletVelocity, weaponBarrel, recoilDirection);
                    break;

                case eShot.cone:
                    ShootConeShot(bulletVelocity, weaponBarrel, recoilDirection);
                    break;

                case eShot.sweeping:
                    ShootSweepingShot(bulletVelocity, weaponBarrel, recoilDirection);
                    break;

                case eShot.beam:
                    ShootBeam(bulletVelocity, weaponBarrel, recoilDirection);
                    break;
            }
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
        if(weaponType.requiresAmmo)
        {
            if (ammoCount > 0)
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
        else
        {
            canFire = true;
        }
    }

    #endregion

    #region Ammo & Reloading

    private void DepleteAmmo(int bullets)
    {
        ammoCount -= bullets;

        fireTimer = weaponType.timeBetweenShots;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        reloadTimer = weaponType.timeToReload;

        while(reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;
            
            yield return null;
        }

        ammoCount = weaponType.totalAmmo;
        isReloading = false;
    }

    #endregion


    // SPECIFIC FUNCTIONS FOR FIRING PATTERNS

    #region Straight-Shot

    // WEAPONS THAT FIRE BULLETS IN A STRIGHT LINE (EX: STANDARD RIFLE)
    private void ShootStraightShot(Vector3 velocity, Transform spawn, Vector3 recoil)
    {
        StartCoroutine(StraightShot(velocity, spawn, recoil));
    }

    IEnumerator StraightShot(Vector3 bulletVelocity, Transform bulletSpawn, Vector3 recoilDir)
    {       
        for(int i = 0; i < weaponType.ammoPerShot; i++)
        {
            GameObject bulletInstance = Instantiate(weaponType.bulletPrefab, bulletSpawn);
            Rigidbody2D bulletRB = bulletInstance.GetComponent<Rigidbody2D>();

            //sets the bullets damage
            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            bullet.SetDamage(weaponType.bulletDamage);

            bulletInstance.transform.parent = null;
            bulletRB.AddForce(bulletVelocity);
            Destroy(bulletInstance, weaponType.bulletTravelTime);

            GameManager.gm.player.KnockbackPlayer(recoilDir * weaponType.recoilForce);

            yield return new WaitForSeconds(weaponType.timeBetweenBullets);
        }
    }

    #endregion

    #region Spread-Shot

    // WEAPONS THAT SHOOT SEVERAL LINES OF BULLETS IN A CONE-LIKE FASHION (EX: SHOTGUN)
    private void ShootSpreadShot(Vector3 velocity, Transform spawn, Vector3 recoil)
    {
        StartCoroutine(SpreadShot(velocity, spawn, recoil));
    }

    IEnumerator SpreadShot(Vector3 bulletVelocity, Transform bulletSpawn, Vector3 recoilDir)
    {             
        Vector3 angle1, angle2 = Vector3.zero;

        for (int i = 0; i < weaponType.ammoPerShot; i++)
        {
            for (int j = 0; j < weaponType.linesOfFire; j++)
            {
                angle1 = Quaternion.AngleAxis(weaponType.angleBetweenBullets * j, Vector3.forward) * bulletVelocity;
                angle2 = Quaternion.AngleAxis(-1f * weaponType.angleBetweenBullets * j, Vector3.forward) * bulletVelocity;

                // Remove the * j from above for wild weapon type

                GameObject bulletInstance1 = Instantiate(weaponType.bulletPrefab, bulletSpawn);
                Rigidbody2D bulletRB1 = bulletInstance1.GetComponent<Rigidbody2D>();

                //sets the bullets damage
                Bullet bullet1 = bulletInstance1.GetComponent<Bullet>();
                bullet1.SetDamage(weaponType.bulletDamage);

                bulletInstance1.transform.parent = null;

                bulletRB1.AddForce(angle1);
                Destroy(bulletInstance1, weaponType.bulletTravelTime);
                GameManager.gm.player.KnockbackPlayer(recoilDir * weaponType.recoilForce);

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

            GameManager.gm.player.KnockbackPlayer(recoilDir * weaponType.recoilForce);
            yield return new WaitForSeconds(weaponType.timeBetweenBullets);
        }
    }

    #endregion

    #region Cone-Shot

    // WEAPONS THAT SHOOTS BULLET AT A RANDOM PATH WITHIN A SPECIFIED CONE
    private void ShootConeShot(Vector3 velocity, Transform spawn, Vector3 recoil)
    {
        StartCoroutine(ShootCone(velocity, spawn, recoil));
    }

    IEnumerator ShootCone(Vector3 bulletVelocity, Transform bulletSpawn, Vector3 recoilDir)
    {
        for (int i = 0; i < weaponType.linesOfFire; i++)
        {
            for(int j = 0; j < weaponType.ammoPerShot; j++)
            {
                float randomAngle = Random.Range(weaponType.coneAngle * -1f, weaponType.coneAngle);

                Vector3 angle = Quaternion.AngleAxis(randomAngle, Vector3.forward) * bulletVelocity;
                Vector3 recoil = Quaternion.AngleAxis(-1f * randomAngle, Vector3.forward) * recoilDir;

                GameObject bulletInstance = Instantiate(weaponType.bulletPrefab, bulletSpawn);
                Rigidbody2D bulletRB = bulletInstance.GetComponent<Rigidbody2D>();

                //sets the bullets damage
                Bullet bullet = bulletInstance.GetComponent<Bullet>();
                bullet.SetDamage(weaponType.bulletDamage);

                bulletInstance.transform.parent = null;
                bulletRB.AddForce(angle);
                Destroy(bulletInstance, weaponType.bulletTravelTime);

                GameManager.gm.player.KnockbackPlayer(recoil * weaponType.recoilForce);
            }

            yield return new WaitForSeconds(weaponType.timeBetweenBullets);
        }
    }

    #endregion

    #region Sweep-Shot

    // WEAPONS THAT SHOOT IN A SWEEPING PATTERN (EX: MACHINE GUN)
    private void ShootSweepingShot(Vector3 velocity, Transform spawn, Vector3 recoil)
    {
        StartCoroutine(SweepingShot(velocity, spawn, recoil));
    }

    IEnumerator SweepingShot(Vector3 bulletVelocity, Transform bulletSpawn, Vector3 recoilDir)
    {
        Vector3 angle1 = Quaternion.AngleAxis(weaponType.sweepAngle, Vector3.forward) * bulletVelocity;
        Vector3 angle2 = Quaternion.AngleAxis(-1f * weaponType.sweepAngle, Vector3.forward) * bulletVelocity;

        Vector3 angle = Vector3.zero, recoil = Vector3.zero;
        float angleBetweenshots = weaponType.sweepAngle * 2f / weaponType.ammoPerShot;

        bool sweepRight = true;

        for (int i = 0; i < weaponType.linesOfFire; i++)
        {
            if(sweepRight)
            {
                sweepRight = false;
                angle = angle1;
            }
            else
            {
                sweepRight = true;
                angle = angle2;
            }

            for (int j = 0; j < weaponType.ammoPerShot; j++)
            {
                if(sweepRight)
                {
                    angle = Quaternion.AngleAxis(angleBetweenshots, Vector3.forward) * angle;
                }
                else
                {
                    angle = Quaternion.AngleAxis(-1f * angleBetweenshots, Vector3.forward) * angle;
                }

                GameObject bulletInstance = Instantiate(weaponType.bulletPrefab, bulletSpawn);
                Rigidbody2D bulletRB = bulletInstance.GetComponent<Rigidbody2D>();

                //sets the bullets damage and piercing
                Bullet bullet = bulletInstance.GetComponent<Bullet>();
                bullet.SetDamage(weaponType.bulletDamage);
                if (weaponType.piercing) bullet.setPiercing(weaponType.piercing);

                bulletInstance.transform.parent = null;
                bulletRB.AddForce(angle);
                Destroy(bulletInstance, weaponType.bulletTravelTime);

                GameManager.gm.player.KnockbackPlayer(angle.normalized * -1f * weaponType.recoilForce);

                yield return new WaitForSeconds(weaponType.timeBetweenBullets);
            }
        }
    }

    #endregion

    #region Beam

    // WEAPONS THAT SHOOT ONE, CONINOUS BEAM RATHER THAN INDIVIDUAL BULLETS (EX: FLAMETHROWER)
    private void ShootBeam(Vector3 velocity, Transform spawn, Vector3 recoil)
    {
        StartCoroutine(Beam(velocity, spawn, recoil));
    }

    IEnumerator Beam(Vector3 bulletVelocity, Transform bulletSpawn, Vector3 recoilDir)
    {
        float beamTimer = weaponType.beamDuration;
        float shotTimer = weaponType.timeBetweenBullets;
        float releaseTime = shotTimer / 10f;

        while (beamTimer > 0f)
        {
            if(shotTimer > 0f)
            {
                shotTimer -= Time.deltaTime;
            }
            else
            {              
                GameObject bulletInstance = Instantiate(weaponType.bulletPrefab, bulletSpawn);
                Rigidbody2D bulletRB = bulletInstance.GetComponent<Rigidbody2D>();

                Bullet bullet = bulletInstance.GetComponent<Bullet>();
                bullet.SetDamage(weaponType.bulletDamage);

                StartCoroutine(UnparentBullet(bulletInstance, releaseTime));

                bulletRB.AddForce(GetBulletVelocity(bulletSpawn.right * -1f));
                Destroy(bulletInstance, weaponType.bulletTravelTime);

                shotTimer = weaponType.timeBetweenBullets;

                GameManager.gm.player.KnockbackPlayer(recoilDir * weaponType.recoilForce);

            }

            beamTimer -= Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator UnparentBullet(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);

        bullet.transform.parent = null;
    }

    #endregion

}
