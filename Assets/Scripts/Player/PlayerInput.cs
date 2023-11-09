using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerController player;

    [Header("Input Booleans")]
    [SerializeField] bool rightKeyPressed;
    [SerializeField] bool rightKeyHeld;
    [SerializeField] bool leftKeyPressed;
    [SerializeField] bool leftKeyHeld;

    [Space(5)] [Header("Action Booleans")] 
    [SerializeField] bool fireRight = false;
    [SerializeField] bool fireLeft = false;
    [SerializeField] bool rotateRight = false;
    [SerializeField] bool rotateLeft = false;
    [SerializeField] bool boostActive = false;

    private float holdMinimum = .25f;   // Determines how long a key must be pressed to be considered held
    private float boostHoldMin = .5f;   // same asHold minimum, but used specifically for the boost ability
    private float timePressedLeft = 0f;
    private float timePressedRight = 0f;

    void Update()
    {
        CheckForInput();
    }

    private void FixedUpdate()
    {
        if(boostActive)
        {
            player.UseBoost();
        }
        else if (fireRight)
        {
            player.FireRight();
            fireRight = false;
        }
        else if (rotateRight)
        {
            player.RotateRight();
            rotateRight = false;
        }
        if (fireLeft)
        {
            player.FireLeft();
            fireLeft = false;
        }
        else if (rotateLeft)
        {
            player.RotateLeft();
            rotateLeft = false;
        }
    }

    private void CheckForInput()
    {       
        if(DetectLeftKeyDown() && DetectRightKeyDown())
        { 
            if(!boostActive)
            {
                player.UsePowerup();

                leftKeyPressed = true;
                rightKeyPressed = true;
            }
        }
        else if(DetectLeftKeyUp() && DetectRightKeyUp())
        {
            if (!rightKeyHeld || !leftKeyHeld)
            {
                Debug.Log("ACTIVATE POWER-UP");
            }

            boostActive = false;
            leftKeyPressed = false;
            leftKeyHeld = false;
            rightKeyPressed = false;
            rightKeyHeld = false;
        }
        else if (DetectLeftKey() && DetectRightKey())
        {
            if (Time.timeSinceLevelLoad - timePressedLeft > boostHoldMin && Time.timeSinceLevelLoad - timePressedRight > boostHoldMin)
            {
                boostActive = true;
                leftKeyPressed = true;
                leftKeyHeld = true;
                rightKeyPressed = true;
                rightKeyHeld = true;
            }
        }
        else
        {
            // CHECK TO SEE IF RIGHT KEY HAS BEEN PRESSED
            if (DetectRightKeyDown())
            {
                timePressedRight = Time.timeSinceLevelLoad;

                rightKeyPressed = true;
                rightKeyHeld = false;
            }
            else if (DetectRightKeyUp())
            {
                // ONCE RIGHT KEY IS RELEASED, CHECK TO SEE IF IT WAS PRESSED OR HELD
                // IF NOT HELD, THEN FIRE
                if (!rightKeyHeld)
                {
                    fireRight = true;
                }

                rightKeyPressed = false;
                rightKeyHeld = false;
                boostActive = false;
            }

            // CHECKS TO SEE IF RIGHT KEY HAS BEEN PRESSED LONG ENOUGH TO BE CONSIDERED HELD
            // IF HELD, THEN ROTATE
            if (DetectRightKey())
            {
                if (Time.timeSinceLevelLoad - timePressedRight > holdMinimum)
                {
                    rightKeyHeld = true;

                    rotateRight = true;
                }
            }

            // CHECK TO SEE IF LEFT KEY HAS BEEN PRESSED
            if (DetectLeftKeyDown())
            {
                timePressedLeft = Time.timeSinceLevelLoad;

                leftKeyPressed = true;
                leftKeyHeld = false;
            }
            else if (DetectLeftKeyUp())
            {
                // ONCE LEFT KEY IS RELEASED, CHECK TO SEE IF IT WAS PRESSED OR HELD
                // IF NOT HELD, THEN FIRE
                if (!leftKeyHeld)
                {
                    fireLeft = true;
                }

                leftKeyPressed = false;
                leftKeyHeld = false;
                boostActive = false;
            }

            // CHECKS TO SEE IF LEFT KEY HAS BEEN PRESSED LONG ENOUGH TO BE CONSIDERED HELD
            // IF HELD, THEN ROTATE
            if (DetectLeftKey())
            {
                if (Time.timeSinceLevelLoad - timePressedLeft > holdMinimum)
                {
                    leftKeyHeld = true;

                    rotateLeft = true;
                }
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
