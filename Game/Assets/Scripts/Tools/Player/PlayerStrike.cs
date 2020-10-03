using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrike : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerWeapon;

    [SerializeField]
    bool WeaponEnabled;

    CharacterAnimator charAnim;

    // Start is called before the first frame update
    void Start()
    {
        charAnim = GetComponentInChildren<CharacterAnimator>();
        PlayerWeapon.SetActive(WeaponEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponEnabled && Input.GetKey(KeyCode.Mouse0))
        {
            charAnim.Strike();
        }
    }

    public void EnableWeapon()
    {
        WeaponEnabled = true;
        PlayerWeapon.SetActive(WeaponEnabled);
    }

}
