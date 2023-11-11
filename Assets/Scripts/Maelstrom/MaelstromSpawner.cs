using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaelstromSpawner : MonoBehaviour
{
    [Tooltip("Try to keep within increments of 50 / 100")]
    public int widthBetweenSpawns;

    public float playerX;
    public float playerY;

    public int mX;
    public int mY;

    public GameObject maelstromPrefab;
    public List<GameObject> maelstroms;

    public List<int> spawnedX;
    public List<int> spawnedY;

    public void FixedUpdate()
    {
        playerX = GameManager.gm.player.playerTransform.position.x;
        playerY = GameManager.gm.player.playerTransform.position.y;

        mX = (int)(playerX / widthBetweenSpawns) * widthBetweenSpawns;
        mY = (int)(playerY / widthBetweenSpawns) * widthBetweenSpawns;

        bool usedX = false;
        bool usedY = false;

        for (int i = 0; i < spawnedX.Count - 1; i++)
        {
            if (mX == spawnedX[i])
            {
                usedX = true;
            }
        }

        for (int i = 0; i < spawnedY.Count - 1; i++)
        {
            if (mY == spawnedY[i])
            {
                usedY = true;
            }
        }

        if (!usedX && !usedY)
        {
            if (!(mX == 0 && mY == 0))
            {
                spawnedX.Add(mX);
                spawnedY.Add(mY);

                int randX = Random.Range(mX, mX + widthBetweenSpawns);
                int randY = Random.Range(mY, mY + widthBetweenSpawns);

                GameObject maelstromInstance = Instantiate(maelstromPrefab, gameObject.transform);
                maelstromInstance.transform.position = new Vector3(randX, randY, 0);

                maelstroms.Add(maelstromInstance);
            }
        }
    }
}
