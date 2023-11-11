using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelUp : PowerUp
{
    public float fuelBack=50;
    public void Awake()
    {
        isInstantUse = true;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            //collectVFX?.Play();
            //collectSFX?.Play();
            playerController.AddBoost(fuelBack);
            Debug.Log("Giving " + fuelBack + "fuel");
            Invoke(nameof(DestoryPowerUp), .35f);
        }
    }
}
