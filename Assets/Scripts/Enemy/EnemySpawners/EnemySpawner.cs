using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawner: MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float enemySpawnTime = 10;
    [SerializeField] float minTimeBetweenSpawn = 1;
    [SerializeField] float spawnRateIncreaser = .1f;

    [Header("Setup")]
    public GameObject enemyPrefab;

    private Camera cam;
    private float currentSpawnerTime;
    private int spawnSide;


    public virtual void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        currentSpawnerTime = enemySpawnTime;
        spawnSide = Random.Range(0, 4);
    }
    public virtual void Update()
    {
        currentSpawnerTime -= Time.deltaTime;
        if (currentSpawnerTime <= 0) SpawnEnemy();
    }
    public virtual void SpawnEnemy()
    {
        Vector3 spawnPoint = cam.transform.position;
        //spawns above the camera on a random x value
        if (spawnSide == 0)
        {
            spawnPoint.y += 36;
            spawnPoint.x += Random.Range(-63, 63);
            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        }
        //spawns below the camera on a random x value
        else if (spawnSide==1)
        {
            spawnPoint.y -= 36;
            spawnPoint.x += Random.Range(-63, 63);
            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        }
        //spawns to the left on the camera on a random y value
        else if (spawnSide==2)
        {
            spawnPoint.x += 63;
            spawnPoint.y += Random.Range(-36, 36);
            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        }
        //spawns to the left of the camera on a random y value;
        else
        {
            spawnPoint.x -= 63;
            spawnPoint.y += Random.Range(-36, 36);
            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        }
        ResetTimer();
    }
    public virtual void ResetTimer()
    {
        spawnSide = Random.Range(0, 4);
        currentSpawnerTime = enemySpawnTime;
        if (enemySpawnTime > minTimeBetweenSpawn) enemySpawnTime -= spawnRateIncreaser;
    }
}
