using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaelstromDetector : MonoBehaviour
{
    [Space(10)]
    [Header("Maelstrom Collision")]
    [SerializeField] GameObject maelstrom;
    [Space(5)]
    [SerializeField] bool inMaelstrom = false;
    [SerializeField] bool allowShipRotation = false;

    private void Update()
    {
        if (inMaelstrom && !GameManager.gm.ui.uiActive)
        {
            RotateWithMaelstrom();
        }
    }

    private void RotateWithMaelstrom()
    {
        transform.RotateAround(maelstrom.transform.position, Vector3.forward, maelstrom.GetComponent<MaelstromRotation>().rotationSpeed * Time.deltaTime);

        if (!allowShipRotation)
        {
            gameObject.transform.up = GameManager.gm.player.playerForward;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // IF PLAYER ENTERS THE 'MAELSTROM' TRIGGER, ROTATE PLAYER AROUND CENTER POINT OF MAELSTROM
        if (collision.gameObject.CompareTag("Maelstrom"))
        {
            //maelstrom = collision.gameObject;
            inMaelstrom = true;
        }

        // IF PLAYER ENTERS THE 'DEATH' TRIGGER, DESTROY PLAYER AND ACTIVATE DEATH SCREEN
        if (collision.gameObject.CompareTag("Death"))
        {
            Debug.Log("Player has been swallowed by the Maelstrom...");

            GameManager.gm.player.playerHealth.Die();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // IF PLAYER EXITS THE 'MAELSTROM' TRIGGER, STOP PLAYER ROTATION
        if (collision.gameObject.CompareTag("Maelstrom"))
        {
            inMaelstrom = false;
        }
    }
}
