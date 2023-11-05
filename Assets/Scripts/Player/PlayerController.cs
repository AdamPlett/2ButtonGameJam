using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public GameObject playerInstance;
    public Transform playerTransform;

    [Header("Player Variables")]
    public float rotateSpeed;
    public float knockbackForce;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public GameObject bulletRange;

    // USED FOR DETECTING KEY INPUT
    private bool keyHeld = false;
    private float timePressed = 0f;
    private float holdMinimum = 0.25f;

    void Update()
    {
        
        // CHECK TO SEE IF Q KEY HAS BEEN PRESSED
        if (Input.GetKeyDown(KeyCode.Q))
        {
            timePressed = Time.timeSinceLevelLoad;
            keyHeld = false;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            // ONCE Q KEY IS RELEASED, CHECK TO SEE IF IT WAS PRESSED OR HELD
            // IF NOT HELD, THEN FIRE
            if (!keyHeld)
            {
                FireRight();
            }

            keyHeld = false;
        }

        // CHECKS TO SEE HOW IF Q KEY HAS BEEN PRESSED LONG ENOUGH TO BE CONSIDERED HELD
        // IF HELD, THEN ROTATE
        if (Input.GetKey(KeyCode.Q))
        {
            if (Time.timeSinceLevelLoad - timePressed > holdMinimum)
            {
                keyHeld = true;

                RotateRight();
            }
        }

        // CHECK TO SEE IF E KEY HAS BEEN PRESSED
        if (Input.GetKeyDown(KeyCode.E))
        {
            timePressed = Time.timeSinceLevelLoad;
            keyHeld = false;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            // ONCE E KEY IS RELEASED, CHECK TO SEE IF IT WAS PRESSED OR HELD
            // IF NOT HELD, THEN FIRE
            if (!keyHeld)
            {
                FireLeft();
            }

            keyHeld = false;
        }

        // CHECKS TO SEE IF E KEY HAS BEEN PRESSED LONG ENOUGH TO BE CONSIDERED HELD
        // IF HELD, THEN ROTATE
        if (Input.GetKey(KeyCode.E))
        {
            if (Time.timeSinceLevelLoad - timePressed > holdMinimum)
            {
                keyHeld = true;

                RotateLeft();
            }
        }
    }

    private void FireRight()
    {
        Debug.Log("SHOTS FIRED FROM THE RIGHT!");

        StartCoroutine(ShootBullet(playerTransform.right * -1f));
        StartCoroutine(MovePlayer(playerTransform.right));
    }

    private void FireLeft()
    {
        Debug.Log("SHOTS FIRED FROM THE LEFT!");

        StartCoroutine(ShootBullet(playerTransform.right));
        StartCoroutine(MovePlayer(playerTransform.right * -1f));
    }

    private void RotateRight()
    {
        playerTransform.Rotate(0, 0, rotateSpeed);
    }

    private void RotateLeft()
    {
        playerTransform.Rotate(0, 0, rotateSpeed * -1f);
    }

    IEnumerator MovePlayer(Vector3 moveDirection)
    {
        Vector3 playerStart = playerTransform.position;
        Vector3 playerEnd = playerTransform.position + moveDirection * knockbackForce;

        float timer = 0f;

        while (timer < 1f)
        {
            playerTransform.transform.position = Vector3.Lerp(playerStart, playerEnd, timer);

            timer += Time.deltaTime * 5f;

            yield return null;
        }
    }

    IEnumerator ShootBullet(Vector3 fireDirection)
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, playerTransform);

        Vector3 bulletStart = playerTransform.position;
        Vector3 bulletEnd = playerTransform.position + (fireDirection * 20f);

        float timer = 0f;

        while (timer < 1f)
        {
            bulletInstance.transform.position = Vector3.Lerp(bulletStart, bulletEnd, timer);

            timer += Time.deltaTime * 2f;

            yield return null;
        }

        Destroy(bulletInstance);
    }
}
