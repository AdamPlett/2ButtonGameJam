using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float damage = 0;
    public bool piercing = false;
    public bool explode;

    public GameObject explosion;

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void setPiercing(bool piercer)
    {
        piercing = piercer;
    }

    public bool GetPiercing()
    {
        return piercing;
    }

    public void DestroyBullet(float time)
    {
        if(explode)
        {
            StartCoroutine(Explode(time));
        }

        Destroy(this.gameObject, time + 0.1f);
    }

    IEnumerator Explode(float time)
    {
        yield return new WaitForSeconds(time);

        if(explode)
        {
            GameObject boom = Instantiate(explosion, gameObject.transform);
            boom.transform.parent = null;
        }
    }

}
