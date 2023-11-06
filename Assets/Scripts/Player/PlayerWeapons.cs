using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSide { left, right }

public class PlayerWeapons : MonoBehaviour
{
    public List<WeaponSlot> rightWeapons = new List<WeaponSlot>();
    public List<WeaponSlot> leftWeapons = new List<WeaponSlot>();
}
