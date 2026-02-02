using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HUDHealth : HealthBar
{
    [Header("UI Objects")]
    [SerializeField] TextMeshProUGUI _healthText;

    [Header("Weapon")] [SerializeField] private GameObject weaponSpawnerIcon;
    
    protected override float _MaxHealth { get => DataManager.Instance.playerData.MaxHealth; }
    protected override float _CurrentHealth { get => DataManager.Instance.playerData.CurrentHealth; }

    private void Awake()
    {
        GameEvents.OnHealthChanged += HealthChangedHandler;
        PlayerWeapon playerWeapon = FindObjectOfType<PlayerWeapon>();
        if (playerWeapon)
            playerWeapon.onWeaponChanged += SetWeaponItem;
    }

    protected override void UpdateHealthBar()
    {
        base.UpdateHealthBar();
        _healthText.text = (int)_CurrentHealth + "/" + (int)_MaxHealth;
    }

    public void SetWeaponItem([CanBeNull] WeaponItem weaponItem)
    {
        WeaponItem currentWeapon = weaponSpawnerIcon.GetComponentInChildren<WeaponItem>();
        if(currentWeapon != null)
            Destroy(currentWeapon);

        if(weaponItem)
            Instantiate(weaponItem, weaponSpawnerIcon.transform);
    }
}