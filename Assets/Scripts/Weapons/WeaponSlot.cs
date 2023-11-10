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

    public Color highlightColor, defaultColor, fullColor;
    public SpriteRenderer barrel;


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

    public void AddWeapon(soWeapon weapon)
    {
        GameObject weaponInstance = Instantiate(GetWeaponFromSO(weapon), gameObject.transform);
        weapons.Add(weaponInstance);
    }

    private GameObject GetWeaponFromSO(soWeapon weapon)
    {
        // This is so ugly but im lazy so we gonna deal with it

        GameObject weaponToAdd = new GameObject();

        switch(weapon.weaponClass)
        {
            case eWeapon.rifle:
                weaponToAdd = GameManager.gm.player.weapons.WeaponPrefabs[0];
                break;

            case eWeapon.burstRifle:
                weaponToAdd = GameManager.gm.player.weapons.WeaponPrefabs[1];
                break;

            case eWeapon.shotty:
                weaponToAdd = GameManager.gm.player.weapons.WeaponPrefabs[2];
                break;

            case eWeapon.autoShotty:
                weaponToAdd = GameManager.gm.player.weapons.WeaponPrefabs[3];
                break;

            case eWeapon.machineGun:
                weaponToAdd = GameManager.gm.player.weapons.WeaponPrefabs[4];
                break;

            case eWeapon.miniGun:
                weaponToAdd = GameManager.gm.player.weapons.WeaponPrefabs[5];
                break;

            case eWeapon.railGun:
                weaponToAdd = GameManager.gm.player.weapons.WeaponPrefabs[6];
                break;

            case eWeapon.flamethrower:
                weaponToAdd = GameManager.gm.player.weapons.WeaponPrefabs[7];
                break;

            case eWeapon.RPG:
                weaponToAdd = GameManager.gm.player.weapons.WeaponPrefabs[8];
                break;
        }

        return weaponToAdd;
    }

    public void UpgradeWeapon()
    {

    }

    public void HighlightSlot(bool highlight)
    {
        if(highlight)
        {
            if(weapons.Count == 0)
            {
                barrel.color = highlightColor;
            }
            else
            {
                barrel.color = fullColor;
            }
        }
        else
        {
            barrel.color = defaultColor;
        }
    }

    public int GetNumWeapons()
    {
        return weapons.Count;
    }
}
