using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float damage=0;
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public float GetDamage()
    {
        return damage;
    }
}
