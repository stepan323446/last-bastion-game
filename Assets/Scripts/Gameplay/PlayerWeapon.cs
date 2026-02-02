using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject weaponPointer;
    [SerializeField] private GameObject weaponSpawner;
    
    private PlayerMovement playerMovement;
    private Animator playerAnimator;

    public UnityAction<WeaponItem?> onWeaponChanged;

    [CanBeNull]
    private Weapon currentWeapon
    {
        get
        {
            Weapon weapon = weaponSpawner.GetComponentInChildren<Weapon>();
            
            return weapon;
        }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseController.IsGamePaused)
            return;
        
        weaponPointer.transform.rotation = Quaternion.Euler(0f, 0f, WeaponAngleAttack());
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentWeapon?.Attack();
        }
    }

    public void SetWeapon(WeaponItem weaponItem)
    {
        DeleteWeapon();
        Instantiate(weaponItem.GetWeaponPrefab(), weaponSpawner.transform);
        onWeaponChanged?.Invoke(weaponItem);
    }

    public void DeleteWeapon()
    {
        if(currentWeapon == null) return;
        Destroy(currentWeapon.gameObject);
        onWeaponChanged?.Invoke(null);
    }
    
    public float WeaponAngleAttack()
    {
        bool isWalking = playerAnimator.GetBool("isWalking");
        float inputX = playerAnimator.GetFloat("InputX");
        float inputY = playerAnimator.GetFloat("InputY");
        float lastInputX = playerAnimator.GetFloat("LastInputX");
        float lastInputY = playerAnimator.GetFloat("LastInputY");

        Vector2 dir;
        if(isWalking)
            dir = new Vector2(inputX, inputY);
        else
            dir = new Vector2(lastInputX, lastInputY);
        

        float angle = Mathf.Atan2(dir.x, -dir.y) * Mathf.Rad2Deg;
        return angle;
    }
}
