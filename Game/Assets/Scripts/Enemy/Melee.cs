using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField]
    private float AggroDistance = 5.0f;

    GameObject player;
    CharacterAnimator charAnim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        charAnim = GetComponentInChildren<CharacterAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < AggroDistance) 
        {
            charAnim.Strike();
        }

        var targetDir = player.transform.position - transform.position;
        var angleDiff = Vector2.SignedAngle(-transform.up, targetDir);
        transform.Rotate(Vector3.forward, angleDiff);
    }
}
