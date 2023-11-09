using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFireBall : MonoBehaviour
{
    public AudioSource hitSound;
    public float damage = 5f;
    int collisions = 0;
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Damage(damage);
            Debug.Log("damage applied");
            DestroyBullet();
        }
    }
    public void setDamage(float dmg)
    {
        damage = dmg;
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
