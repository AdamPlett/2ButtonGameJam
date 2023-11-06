using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // ACCESS "soWeapon" TO MODIFY GUN STATS
    public soWeapon weaponType;

    public void Shoot(eSide side, float fireAngle, Vector3 bulletForward)
    {
        Vector3 bulletVelocity = GetBulletVelocity(bulletForward);

        switch(weaponType.shootingPattern)
        {
            case eShot.straightShot:
                ShootStraightShot(bulletVelocity, bulletForward * -1f);
                break;

            case eShot.tripleShot:
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
    }

    // MULTIPLIES BULLET SPEED BY THE WEAPON BARREL'S "FORWARD" TO GET BULLET VELOCITY
    private Vector3 GetBulletVelocity(Vector3 direction)
    {
        return direction * weaponType.bulletSpeed * 100f;
    }

    // CALLED FOR STRAIGHT SHOOTING WEAPONS THAT FIRE A SINGULAR BULLET AT A TIME (EX: RIFLES, SNIPERS, ETC)
    private void ShootStraightShot(Vector3 velocity, Vector3 recoilDir)
    {
        GameObject bulletInstance = Instantiate(weaponType.bulletPrefab, GameManager.gm.player.playerTransform);

        bulletInstance.transform.parent = null;

        Destroy(bulletInstance, weaponType.bulletTravelTime);

        Rigidbody2D bulletRB = bulletInstance.GetComponent<Rigidbody2D>();

        bulletRB.AddForce(velocity);

        GameManager.gm.player.KnockbackPlayer(weaponType.recoilForce, recoilDir);
    }
}
