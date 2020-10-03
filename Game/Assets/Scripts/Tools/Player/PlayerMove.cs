using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerRotate))]
public class PlayerMove : MonoBehaviour
{
    private Vector2 moveInput;

    [SerializeField]
    private float moveSpeed = 3.0f;
    [SerializeField]
    private float moveModifier = 1.0f;
    private Rigidbody playerRigidbody;
    private PlayerRotate playerRotate;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRotate = GetComponent<PlayerRotate>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        var mousePos3d = Input.mousePosition;
        mousePos3d.z = Mathf.Abs(Camera.main.transform.position.z);
        var mousePos = Camera.main.ScreenToWorldPoint(mousePos3d);
        playerRotate.SetTarget(mousePos);
    }

    private void FixedUpdate()
    {
        var velocityDir = moveInput.magnitude > 1.0f ? moveInput.normalized : moveInput;
        playerRigidbody.velocity = velocityDir * moveSpeed * moveModifier;
    }
}
