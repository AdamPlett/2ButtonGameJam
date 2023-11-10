using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChoice : MonoBehaviour
{
    public List<soWeapon> allWeapons;
    public soWeapon randomWeapon;
    public Image gunImage;
    public TextMeshProUGUI gunName;
    public TextMeshProUGUI gunDesc;

    void Start()
    {
        RandomizeWeaponChoices();
    }

    public void RandomizeWeaponChoices()
    {
        randomWeapon = GetRandomWeapon();

        gunImage = randomWeapon.weaponSprite;
        gunName.text = randomWeapon.weaponName;
        gunDesc.text = randomWeapon.weaponDescription;
    }

    public soWeapon GetRandomWeapon()
    {
        return allWeapons[Random.Range(0, allWeapons.Count-1)];
    }
}
