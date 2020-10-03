using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrike : MonoBehaviour
{
    CharacterAnimator charAnim;

    // Start is called before the first frame update
    void Start()
    {
        charAnim = GetComponentInChildren<CharacterAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            charAnim.Strike();
        }
    }
}
