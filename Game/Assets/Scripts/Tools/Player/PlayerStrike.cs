using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStrike : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerWeapon;

    [SerializeField]
    bool WeaponEnabled;

    CharacterAnimator charAnim;

    PlayerInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        charAnim = GetComponentInChildren<CharacterAnimator>();
        PlayerWeapon.SetActive(WeaponEnabled);
        inventory = Configs.main.PlayerInventory;
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponEnabled && Input.GetKey(KeyCode.Mouse0))
        {
            charAnim.Strike();
            PlayerResources.main.Spend(PlayerResourceType.Energy, Configs.main.Game.EnergySpentByHit);
        }

        if (inventory.PlayerItems.Any(x => x.Slot == InventorySlot.WEAPON))
        {
            EnableWeapon();
        }
    }

    public void EnableWeapon()
    {
        WeaponEnabled = true;
        PlayerWeapon.SetActive(WeaponEnabled);
    }

}
