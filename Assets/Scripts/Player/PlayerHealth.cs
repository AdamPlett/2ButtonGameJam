using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP = 100f;
    public GameObject playerArt;

    [Header("Audio")]
    public AudioSource hit;
    public AudioSource death;


    // Update is called once per frame
    void Update()
    {
        if (currentHP <= 0)
        {
            //death.Play();

            Die();
        }
    }

    public void Damage(float damage)
    {
        //hit.Play();

        currentHP -= damage;
        Debug.Log("Current Player HP:" + currentHP);

        GameManager.gm.ui.UpdateHealthBar(currentHP);
    }

    public void giveHealth(float addedHealth)
    {
        if (currentHP + addedHealth <= maxHP)
        {
            currentHP += addedHealth;
            GameManager.gm.ui.UpdateHealthBar(currentHP);
        }
        else
        {
            currentHP = maxHP;
            GameManager.gm.ui.UpdateHealthBar(currentHP);
        }
    }

    void Die()
    {
        Debug.Log("Player death!");
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        GameManager.gm.ui.ActivateDeathScreen();
        playerArt.SetActive(false);

        yield return new WaitForSeconds(1f);
    }
}
