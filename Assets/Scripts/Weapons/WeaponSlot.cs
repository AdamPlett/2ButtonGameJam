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


    public void ShootWeapons()
    {
        if (weapons.Count > 0)
        {
            foreach (var weapon in weapons)
            {
                if (weapon != null)
                {
                    weapon.GetComponent<Weapon>().Shoot(this.transform.right * -1f, gameObject.transform);
                }
            }
        }
    }
}
