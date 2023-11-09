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
    // ALSO ADDS PLAYER VELOCITY TO PRESERVE SHIP MOMENTUM
    private Vector3 GetBulletVelocity(Vector3 direction)
    {
        Vector3 playerVelocity = Vector3.zero;

        playerVelocity.x = GameManager.gm.player.playerRB.velocity.x;
        playerVelocity.y = GameManager.gm.player.playerRB.velocity.y;
        playerVelocity.z = 0;

        return (100f * weaponType.bulletSpeed * direction) + playerVelocity;
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
            bulletInstance.transform.localScale = Vector3.one;
            bulletRB.AddForce(bulletVelocity);
            bullet.DestroyBullet(weaponType.bulletTravelTime);

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
                bulletInstance1.transform.localScale = Vector3.one;
                bulletRB1.AddForce(angle1);
                bullet1.DestroyBullet(weaponType.bulletTravelTime);

                GameObject bulletInstance2 = Instantiate(weaponType.bulletPrefab, bulletSpawn);
                Rigidbody2D bulletRB2 = bulletInstance2.GetComponent<Rigidbody2D>();

                //sets the bullets damage
                Bullet bullet2 = bulletInstance2.GetComponent<Bullet>();
                bullet2.SetDamage(weaponType.bulletDamage);

                bulletInstance2.transform.parent = null;
                bulletInstance2.transform.localScale = Vector3.one;
                bulletRB2.AddForce(angle2);
                bullet2.DestroyBullet(weaponType.bulletTravelTime);

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
                bulletInstance.transform.localScale = Vector3.one;
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

        List<Vector3> linesOfFire = new List<Vector3>();

        for(int i = (int)-weaponType.sweepAngle; i < (int)weaponType.sweepAngle; i += (int)angleBetweenshots)
        {
            Vector3 newLine = Quaternion.AngleAxis(i, Vector3.forward) * bulletVelocity;

            linesOfFire.Add(newLine);
        }

        bool sweepRight = GetRandomBool();

        int index = Random.Range(0, linesOfFire.Count - 1);

        for (int i = 0; i < weaponType.linesOfFire; i++)
        {
            for (int j = 0; j < weaponType.ammoPerShot; j++)
            {
                angle = linesOfFire[index];

                GameObject bulletInstance = Instantiate(weaponType.bulletPrefab, bulletSpawn);
                Rigidbody2D bulletRB = bulletInstance.GetComponent<Rigidbody2D>();

                //sets the bullets damage and piercing
                Bullet bullet = bulletInstance.GetComponent<Bullet>();
                bullet.SetDamage(weaponType.bulletDamage);
                if (weaponType.piercing) bullet.setPiercing(weaponType.piercing);

                bulletInstance.transform.parent = null;
                bulletInstance.transform.localScale = Vector3.one;
                bulletRB.AddForce(angle);
                bullet.DestroyBullet(weaponType.bulletTravelTime);

                GameManager.gm.player.KnockbackPlayer(angle.normalized * -1f * weaponType.recoilForce);

                yield return new WaitForSeconds(weaponType.timeBetweenBullets);

                if(sweepRight)
                {
                    index++;

                    if(index >= linesOfFire.Count)
                    {
                        index = linesOfFire.Count - 1;

                        sweepRight = false;
                    }
                }
                else
                {
                    index--;

                    if (index <= 0)
                    {
                        index = 0;

                        sweepRight = true;
                    }
                }
            }
        }
    }

    private bool GetRandomBool()
    {
        int num = Random.Range(0, 1);

        if(num == 0)
        {
            return true;
        }
        else
        {
            return false;
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
                bullet.DestroyBullet(weaponType.bulletTravelTime);

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
