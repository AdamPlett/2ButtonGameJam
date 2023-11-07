using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // ACCESS "soWeapon" TO MODIFY GUN STATS
    public soWeapon weaponType;

    public void Shoot(Vector3 bulletForward)
    {
        Vector3 bulletVelocity = GetBulletVelocity(bulletForward);

        switch(weaponType.shootingPattern)
        {
            case eShot.straightShot:
                ShootStraightShot(bulletVelocity);
                break;

            case eShot.burstShot:
                //ShootTripleShot(bulletVelocity);
                break;

            case eShot.scatter:
                //ShootScatterShot(bulletVelocity);
                break;

            case eShot.sweeping:
                //ShootSweepingShot(bulletVelocity);
                break;

            case eShot.beam:
                //ShootBeam(bulletVelocity);
                break;
        }

        GameManager.gm.player.KnockbackPlayer(weaponType.recoilForce, bulletForward * -1f);
    }

    // MULTIPLIES BULLET SPEED BY THE WEAPON BARREL'S "FORWARD" TO GET BULLET VELOCITY
    private Vector3 GetBulletVelocity(Vector3 direction)
    {
        return direction * weaponType.bulletSpeed * 100f;
    }


    // CALLED FOR STRAIGHT SHOOTING WEAPONS THAT FIRE A SINGULAR BULLET AT A TIME (EX: STANDARD RIFLE)
    private void ShootStraightShot(Vector3 velocity)
    {
        GameObject bulletInstance = Instantiate(weaponType.bulletPrefab, GameManager.gm.player.playerTransform);

        bulletInstance.transform.parent = null;

        Destroy(bulletInstance, weaponType.bulletTravelTime);

        Rigidbody2D bulletRB = bulletInstance.GetComponent<Rigidbody2D>();

        bulletRB.AddForce(velocity);
    }

    // CALLED FOR STRAIGHT SHOOTING WEAPONS THAT FIRE NUMEROUS BULLETS AT A TIME (EX: BURST RIFLE)
    private void ShootBurstShot(Vector3 velocity, int bulletsPerShot)
    {

    }

    // CALLED FOR SPREAD WEAPONS THAT SHOOT MULTIPLE BULLETS AT A TIME, WITH SEVERAL ANGLES OF FIRE (EX: SHOTGUN)
    private void ShootScatterShot(Vector3 velocity, int bulletsPerShot, float spreadAngle)
    {

    }

    // CALLED FOR WEAPONS THAT SHOOT IN A SWEEPING PATTERN (EX: MACHINE GUN)
    private void ShootSweepingShot(Vector3 velocity, float sweepAngle)
    {

    }

    // CALLED FOR WEAPONS THAT SHOOT ONE, CONINOUS BEAM RATHER THAN INDIVIDUAL BULLETS (EX: FLAMETHROWER)
    private void ShootBeam(Vector3 velocity, float beamRange, float beamWidth)
    {

    }

}
