using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStrike : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        charAnim = GetComponentInChildren<CharacterAnimator>();
        inventory = Configs.main.PlayerInventory;
    }

    // Update is called once per frame
    void Update()
    {
        if (equippedWeapon != null && Input.GetKey(KeyCode.Mouse0))
        {
            charAnim.Strike();
        }

        var weapon = inventory.PlayerItems.FindAll(x => x.Slot == InventorySlot.WEAPON)
            .OrderByDescending(x => x.ItemLevel)
            .First();
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



}
