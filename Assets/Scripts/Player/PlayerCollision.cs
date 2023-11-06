using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] CapsuleCollider2D playerCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // IF PLAYER ENTERS THE 'MAELSTROM' TRIGGER, MAKE IT A CHILD OF THE WHIRLPOOL SO IT INHERITS ITS ROTATION
        if(collision.gameObject.CompareTag("Maelstrom"))
        {
            transform.parent = collision.gameObject.transform;
        }

        // IF PLAYER ENTERS THE 'DEATH' TRIGGER, DESTROY PLAYER AND ACTIVATE DEATH SCREEN
        if(collision.gameObject.CompareTag("Death"))
        {
            Debug.Log("Player has been swallowed by the Maelstrom...");

            //Destroy(this.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        // IF PLAYER EXITS THE 'MAELSTROM' TRIGGER, REMOVE ITS PARENT SO IT NO LONGER INHERITS ROTATION
        if (collision.gameObject.CompareTag("Maelstrom"))
        {
            transform.parent = null;
        }
    }
}
