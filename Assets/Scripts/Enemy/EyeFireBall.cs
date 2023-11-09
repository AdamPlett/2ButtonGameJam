using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFireball : MonoBehaviour
{
    public AudioSource hitSound;
    public float damage = 5f;
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public float GetDamage()
    {
        return damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Detected");
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Damage(damage);
            Debug.Log("damage applied");
            DestroyBullet();
        }
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
