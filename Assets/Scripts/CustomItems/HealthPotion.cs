using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item
{
    [SerializeField] private float healValue;
    
    public override void UseItem()
    {
        if(DataManager.Instance.playerData.IsDead) return;

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if(playerHealth == null) return;
        
        SoundEffectManager.Play("PotionDrink");
        playerHealth.Heal(healValue);
        InventoryController.Instance.RemoveItemsFromInventory(ID, 1);
    }
}
