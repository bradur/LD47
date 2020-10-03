using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemGrabber : MonoBehaviour
{
    private PlayerStrike playerStrike;
    private CollisionDamager collisionDamager;

    // Start is called before the first frame update
    void Start()
    {
        playerStrike = GetComponent<PlayerStrike>();
        collisionDamager = GetComponentInChildren<CollisionDamager>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grab(Item item)
    {
        Debug.Log("Picked up " + item.ItemType);

        switch(item.ItemType)
        {
            case ItemType.BASIC_SWORD:
                playerStrike.EnableWeapon();
                break;
            case ItemType.PICKAXE:
                collisionDamager.DamageMultipliers[DamageableType.WALL] = 100.0f;
                break;
        }

    }
}
