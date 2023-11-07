using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] eSide side;

    [Header("Angle of Fire")]
    [SerializeField] float zAngle;

    [Header("Weapons in this Slot")]
    public List<GameObject> weapons = new List<GameObject>();

    private void Start()
    {
        if (weapons != null)
        {
            if (weapons.Count > 0)
            {
                foreach (var weapon in weapons)
                {
                    if (weapon != null)
                    {
                        InstantiateWeapon(weapon);
                    }
                }
            }
        }
    }

    public void ShootWeapons()
    {
        if (weapons != null)
        {
            if (weapons.Count > 0)
            {
                foreach (var weapon in weapons)
                {
                    if(weapon != null)
                    {
                        weapon.GetComponent<Weapon>().Shoot(this.transform.right * -1f);
                    }
                }
            }
        }
    }

    private void InstantiateWeapon(GameObject weapon)
    {
        GameObject weaponInstance = Instantiate(weapon);
        weaponInstance.transform.parent = gameObject.transform;
    }
}
