using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWeapons : MonoBehaviour
{
    public GameObject upgradeBox;
    public GameObject upgradeChoice1, upgradeChoice2;

    public GameObject weaponBox;
    public GameObject weaponChoice1, weaponChoice2;

    public GameObject player;
    public InputUI uiInput;

    [Header("Weapon Selector")]
    public int activeSlot = 0;
    public List<WeaponSlot> weaponSlots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetUpgradeChoices()
    {
        weaponBox.SetActive(false);
        upgradeBox.SetActive(false);

        upgradeChoice1.SetActive(true);
        upgradeChoice2.SetActive(true);
    }

    public void SelectUpgrade1()
    {
        upgradeChoice1.SetActive(false);
        upgradeChoice2.SetActive(false);
    }

    public void SelectUpgrade2()
    {
        upgradeChoice1.SetActive(false);
        upgradeChoice2.SetActive(false);
    }

    public void GetWeapoonChoices()
    {
        weaponBox.SetActive(false);
        upgradeBox.SetActive(false);

        weaponChoice1.GetComponent<WeaponChoice>().RandomizeWeaponChoices();
        weaponChoice1.SetActive(true);

        weaponChoice2.GetComponent<WeaponChoice>().RandomizeWeaponChoices();
        weaponChoice2.SetActive(true);
    }

    public void SelectWeapon1()
    {
        weaponChoice1.SetActive(false);
        weaponChoice2.SetActive(false);

        uiInput.SetWeapon(weaponChoice1.GetComponent<WeaponChoice>().randomWeapon);
    }

    public void SelectWeapon2()
    {
        weaponChoice1.SetActive(false);
        weaponChoice2.SetActive(false);

        uiInput.SetWeapon(weaponChoice2.GetComponent<WeaponChoice>().randomWeapon);
    }

    private void ResetUI()
    {
        weaponBox.SetActive(true);
        upgradeBox.SetActive(true);

        GameManager.gm.ui.ActivateUpgradeScreen(false);
    }


    public void SelectSlot(soWeapon weapon)
    {
        if(weaponSlots[activeSlot].GetNumWeapons() == 0)
        {
            weaponSlots[activeSlot].AddWeapon(weapon);

            uiInput.selectorActive = false;

            foreach(var slot in weaponSlots)
            {
                slot.HighlightSlot(false);
            }

            ResetUI();
        }
    }

    public void ShiftActiveSlot(int increment)
    {
        weaponSlots[activeSlot].HighlightSlot(false);
        activeSlot += increment;

        if (activeSlot >= weaponSlots.Count)
        {
            activeSlot = 0;
            weaponSlots[activeSlot].HighlightSlot(true);
        }
        else if(activeSlot < 0)
        {
            activeSlot = weaponSlots.Count - 1;
            weaponSlots[activeSlot].HighlightSlot(true);
        }

        weaponSlots[activeSlot].HighlightSlot(true);
    }
}
