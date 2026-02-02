using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    [SerializeField] private GameObject weaponPrefab;

    public GameObject GetWeaponPrefab() => weaponPrefab;
    
    public override void UseItem()
    {
        PlayerWeapon playerWeapon = FindObjectOfType<PlayerWeapon>();
        if(playerWeapon == null) return;
        
        playerWeapon.SetWeapon(this);
    }
}
