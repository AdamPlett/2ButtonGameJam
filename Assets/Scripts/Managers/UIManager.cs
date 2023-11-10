using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public int score;
    public float health;
    public float fuel;

    [Header("Timer")]
    public float totalTimeAlive;
    [Space(5)]
    public int minutesAlive;
    public int secondsAlive;

    [Header("Booleans")]
    public bool menuActive;
    public bool playerAlive;

    [Header("UI Components")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public Slider healthSlider;
    public Slider fuelslider;
    public Image powerup;


    private void Update()
    {
        if (playerAlive)
        {
            // Update timer text
            totalTimeAlive += Time.deltaTime;
            timerText.text = "Time Alive: " + GetTimerText();

            // Update score text
            scoreText.text = "Score: " + score.ToString();

            UpdateHealthBar(health);
            UpdateFuelBar(fuel);
        }
    }

    public void UpdateHealthBar(float currentHealth)
    {
        health = currentHealth;

        float healthPercent = GameManager.gm.player.playerHealth.currentHP / GameManager.gm.player.playerHealth.maxHP;
        healthSlider.value = healthPercent;
    }

    public void UpdateFuelBar(float currentFuel)
    {
        fuel = currentFuel;

        float fuelPercent = GameManager.gm.player.boosterFuel / GameManager.gm.player.maxFuel;
        fuelslider.value = fuelPercent;
    }

    public void AddToScore(int addition)
    {
        score += addition;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    private string GetTimerText()
    {
        minutesAlive = (int)(totalTimeAlive / 60);
        secondsAlive = (int)(totalTimeAlive % 60);

        return minutesAlive + "m " + secondsAlive + "s";
    }
}
