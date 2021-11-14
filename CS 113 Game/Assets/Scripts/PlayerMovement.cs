using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 movement;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask dashLayerMask;
    private bool isDashButtonDown = false;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)) {
            isDashButtonDown = true;
        }
    }

    private void FixedUpdate()
    {
        if (movement.x > 0) playerTransform.localScale = new Vector2(1, 1);
        else if (movement.x < 0) playerTransform.localScale = new Vector2(-1, 1);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        if (isDashButtonDown)
        {
            float dashAmount = 0.65f;
            Vector3 dashPosition = rb.position + movement * dashAmount;

            RaycastHit2D raycastHit2d = Physics2D.Raycast(rb.position, movement, dashAmount, dashLayerMask);
            if (raycastHit2d.collider != null) {
                dashPosition = raycastHit2d.point;
            }
            rb.MovePosition(dashPosition);
            isDashButtonDown = false;
        }
    }
}
