using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStrike : MonoBehaviour, AnimationListener, DamageListener
{
    [SerializeField]
    GameObject Club;
    [SerializeField]
    GameObject Sword;
    [SerializeField]
    GameObject PickAxe;
    
    InventoryItem equippedWeapon;

    CharacterAnimator charAnim;

    PlayerInventory inventory;

    int targetsHit;

    // Start is called before the first frame update
    void Start()
    {
        charAnim = GetComponentInChildren<CharacterAnimator>();
        inventory = Configs.main.PlayerInventory;

        charAnim.RegisterListener(this);
        GetComponentInChildren<CollisionDamager>(true).RegisterListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (equippedWeapon != null && Input.GetKey(KeyCode.Mouse0))
        {
            charAnim.Strike();
        }

        var weapon = inventory.GetWeapon();
        if (weapon != null)
        {
            EnableWeapon(weapon);
        }
    }

    public void EnableWeapon(InventoryItem weapon)
    {
        if (weapon.Slot != InventorySlot.WEAPON)
        {
            return;
        }

        equippedWeapon = weapon;
        switch (weapon.ItemLevel)
        {
            case 0:
                Club.SetActive(true);
                Sword.SetActive(false);
                PickAxe.SetActive(false);
                break;
            case 1:
                Club.SetActive(false);
                Sword.SetActive(true);
                PickAxe.SetActive(false);
                break;
            case 2:
                Club.SetActive(false);
                Sword.SetActive(false);
                PickAxe.SetActive(true);
                break;

        }
    }

    public void StrikeDone()
    {
        if (targetsHit > 0)
        {
            PlayerResources.main.SpendEnergy(PlayerResources.main.GetEnergyPerStrike());
        }
        targetsHit = 0;
    }

    public void DamageableHit(Damageable damageable)
    {
        targetsHit++;
    }
}
