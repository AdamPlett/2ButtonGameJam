using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // ACCESS "soWeapon" TO MODIFY GUN STATS
    public soWeapon weaponType;

    [SerializeField] int ammoCount;
    [SerializeField] float reloadTimer;

    [SerializeField] bool canFire;
    [SerializeField] float fireTimer;

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

    #region Shooting

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
                    //ShootScatterShot(bulletVelocity);
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

    #region Shooting Functions

    // WEAPONS THAT FIRE BULLETS IN A STRIGHT LINE (EX: STANDARD RIFLE)
    private void ShootStraightShot(Vector3 velocity, Transform spawn)
    {
        StartCoroutine(ShootBullets(weaponType.bulletsPerShot, velocity, spawn));
    }

    IEnumerator ShootBullets(int numBullets, Vector3 bulletVelocity, Transform bulletSpawn)
    {       
        for(int i = 0; i < numBullets; i++)
        {
            GameObject bulletInstance = Instantiate(weaponType.bulletPrefab, bulletSpawn);
            Rigidbody2D bulletRB = bulletInstance.GetComponent<Rigidbody2D>();

            bulletInstance.transform.parent = null;
            bulletRB.AddForce(bulletVelocity);
            Destroy(bulletInstance, weaponType.bulletTravelTime);

            yield return new WaitForSeconds(0.1f);
        }
    }

    // WEAPONS THAT SHOOT SEVERAL LINES OF BULLETS IN A CONE-LIKE FASHION (EX: SHOTGUN)
    private void ShootSpreadShot(Vector3 velocity, int bulletsPerShot, float spreadAngle)
    {

    }

    // WEAPONS THAT SHOOT IN A SWEEPING PATTERN (EX: MACHINE GUN)
    private void ShootSweepingShot(Vector3 velocity, float sweepAngle)
    {

    }

    // WEAPONS THAT SHOOT ONE, CONINOUS BEAM RATHER THAN INDIVIDUAL BULLETS (EX: FLAMETHROWER)
    private void ShootBeam(Vector3 velocity, float beamRange, float beamWidth)
    {

    }

    #endregion

}
