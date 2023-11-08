using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerHP = 100f;
    public GameObject playerArt;
    public Slider slider;
    public AudioSource hit;
    public AudioSource death;

    // Start is called before the first frame update

    public void SetHealth()
    {
        slider.value = playerHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHP <= 0 )
        {
            //death.Play();
            Die();
        }
    }
    public void Damage(float damage)
    {
        //hit.Play();
        playerHP -= damage;
        Debug.Log("Current Player HP:" + playerHP);
        //SetHealth();
    }
    public void giveHealth(float addedHealth)
    {
        if (playerHP + addedHealth <= 100)
        {
            playerHP += addedHealth;
            SetHealth();
        }
    }
    void Die()
    {
        Debug.Log("Player death!");
        //StartCoroutine(DeathSequence());
    }
    IEnumerator DeathSequence()
    {
        playerArt.SetActive(false);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Level01");
    }
}
