using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerRotate))]
public class PlayerMove : MonoBehaviour
{
    private Vector2 moveInput;
    
    [SerializeField]
    private float moveModifier = 1.0f;
    private Rigidbody2D playerRigidbody;
    private PlayerRotate playerRotate;

    CharacterAnimator charAnim;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerRotate = GetComponent<PlayerRotate>();
        charAnim = GetComponentInChildren<CharacterAnimator>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        var mousePos3d = Input.mousePosition;
        mousePos3d.z = Mathf.Abs(Camera.main.transform.position.z);
        var mousePos = Camera.main.ScreenToWorldPoint(mousePos3d);
        playerRotate.SetTarget(mousePos);

        if (playerRigidbody.velocity.magnitude > 0.01f)
        {
            charAnim.Walk();
        }
        else
        {
            charAnim.Stop();
        }
    }

    private void FixedUpdate()
    {
        var moveSpeed = PlayerResources.main.GetMoveSpeed();

        var velocityDir = moveInput.magnitude > 1.0f ? moveInput.normalized : moveInput;
        playerRigidbody.velocity = velocityDir * moveSpeed * moveModifier;
    }
}
