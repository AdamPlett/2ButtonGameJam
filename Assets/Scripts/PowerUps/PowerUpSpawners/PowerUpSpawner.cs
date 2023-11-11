using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpSpawner : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float powerupSpawnTime = 25;
    [SerializeField] float maxTimeBetweenSpawn = 120;
    [SerializeField] float spawnRateDecreaser = 5;

    [Header("Setup")]
    public GameObject powerupPrefab;

    private Camera cam;
    private float currentSpawnerTime;
    public virtual void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        currentSpawnerTime = powerupSpawnTime;
    }
    public virtual void Update()
    {
        if(!GameManager.gm.ui.uiActive)
        {
            currentSpawnerTime -= Time.deltaTime;
            if (currentSpawnerTime <= 0) SpawnPowerup();
        }
    }

    public virtual void SpawnPowerup()
    {
        Vector3 spawnPoint = cam.transform.position;
        spawnPoint.x += Random.Range(-57, 57);
        spawnPoint.y += Random.Range(-30, 30);
        spawnPoint.z=0;
        Instantiate(powerupPrefab, spawnPoint, Quaternion.identity);

        ResetTimer();
    }
    public virtual void ResetTimer()
    {
        currentSpawnerTime = powerupSpawnTime;
        if (powerupSpawnTime < maxTimeBetweenSpawn) powerupSpawnTime += spawnRateDecreaser;
    }
}
