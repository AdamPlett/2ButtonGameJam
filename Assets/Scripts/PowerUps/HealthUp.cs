using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : PowerUp
{
    public float healthBack;
    public void Awake()
    {
        isInstantUse = true;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            //collectVFX?.Play();
            //collectSFX?.Play();
            playerHealth.giveHealth(healthBack);
            Debug.Log("Giving " + healthBack + "HP");
            Invoke(nameof(DestoryPowerUp), .35f);
        }
    }
}
