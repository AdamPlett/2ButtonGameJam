using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : PowerUp
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            //collectVFX?.Play();
            //collectSFX?.Play();
            StartCoroutine(PowerupSequence(playerController));

        }
    }
    IEnumerator PowerupSequence(PlayerController playerController)
    {
        playerController.SetShield(true);
        DisablePowerupInteraction();
        yield return new WaitForSeconds(duration);
        playerController.SetShield(false);
        Invoke(nameof(DestoryPowerUp), .1f);
    }
}
