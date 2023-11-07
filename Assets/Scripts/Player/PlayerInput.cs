using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerController player;

    [Header("Booleans")]
    [SerializeField] bool rightKeyPressed;
    [SerializeField] bool rightKeyHeld;
    [SerializeField] bool leftKeyPressed;
    [SerializeField] bool leftKeyHeld;

    private float timePressed = 0f;
    private float holdMinimum = 0.25f;   // Determines how long a key must be pressed to be considered held


    void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        // CHECK TO SEE IF RIGHT KEY HAS BEEN PRESSED
        if (DetectRightKeyDown())
        {
            timePressed = Time.timeSinceLevelLoad;

            rightKeyPressed = true;
            rightKeyHeld = false;
        }
        else if (DetectRightKeyUp())
        {
            // ONCE RIGHT KEY IS RELEASED, CHECK TO SEE IF IT WAS PRESSED OR HELD
            // IF NOT HELD, THEN FIRE
            if (!rightKeyHeld)
            {
                player.FireRight();
            }

            rightKeyPressed = false;
            rightKeyHeld = false;
        }

        // CHECKS TO SEE IF RIGHT KEY HAS BEEN PRESSED LONG ENOUGH TO BE CONSIDERED HELD
        // IF HELD, THEN ROTATE
        if (DetectRightKey())
        {
            if (Time.timeSinceLevelLoad - timePressed > holdMinimum)
            {
                rightKeyHeld = true;

                player.RotateRight();
            }
        }

        // CHECK TO SEE IF LEFT KEY HAS BEEN PRESSED
        if (DetectLeftKeyDown())
        {
            timePressed = Time.timeSinceLevelLoad;

            leftKeyPressed = true;
            leftKeyHeld = false;
        }
        else if (DetectLeftKeyUp())
        {
            // ONCE LEFT KEY IS RELEASED, CHECK TO SEE IF IT WAS PRESSED OR HELD
            // IF NOT HELD, THEN FIRE
            if (!leftKeyHeld)
            {
                player.FireLeft();
            }

            leftKeyPressed = false;
            leftKeyHeld = false;
        }

        // CHECKS TO SEE IF LEFT KEY HAS BEEN PRESSED LONG ENOUGH TO BE CONSIDERED HELD
        // IF HELD, THEN ROTATE
        if (DetectLeftKey())
        {
            if (Time.timeSinceLevelLoad - timePressed > holdMinimum)
            {
                leftKeyHeld = true;

                player.RotateLeft();
            }
        }
    }

    #region Key-Detection Methods

    // DETECTING LEFT KEY INPUT

    private bool DetectLeftKey()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            return true;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            return true;
        }
        else if(Input.GetKey(KeyCode.X))
        {
            return true;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            return true;
        }
        else if(Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }

        return false;
    }

    private bool DetectLeftKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            return true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            return true;
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            return true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            return true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            return true;
        }

        return false;
    }

    private bool DetectLeftKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            return true;
        }

        return false;
    }


    // DETECTING RIGHT KEY INPUT

    private bool DetectRightKey()
    {
        if (Input.GetKey(KeyCode.E))
        {
            return true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            return true;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            return true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            return true;
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            return true;
        }

        return false;
    }

    private bool DetectRightKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            return true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            return true;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            return true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            return true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            return true;
        }

        return false;
    }

    private bool DetectRightKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            return true;
        }

        return false;
    }

    #endregion
}
