using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUI : MonoBehaviour
{
    public bool selectorActive;
    public UpgradeWeapons upgradeScript;
    public soWeapon selectedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gm.ui.uiActive)
        {
            if(selectorActive)
            {                
                if(DetectLeftKey() && DetectRightKey())
                {
                    upgradeScript.SelectSlot(selectedWeapon);
                }
                else if (DetectLeftKeyUp())
                {
                    upgradeScript.ShiftActiveSlot(1);
                }
                else if(DetectRightKeyUp())
                {
                    upgradeScript.ShiftActiveSlot(-1);
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.L))
            {
                GameManager.gm.ui.ActivateUpgradeScreen(true);
            }
        }
    }

    public void SetWeapon(soWeapon weapon)
    {
        selectedWeapon = weapon;
        selectorActive = true;
    }


    #region Key-Detection Methods

    // DETECTING LEFT KEY INPUT

    private bool DetectLeftKey()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            return true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            return true;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            return true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            return true;
        }
        else if (Input.GetKey(KeyCode.Mouse0))
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
