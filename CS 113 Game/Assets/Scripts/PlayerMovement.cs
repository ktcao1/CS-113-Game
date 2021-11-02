using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 movement;
    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (movement.x > 0) playerTransform.localScale = new Vector2(1, 1);
        if (movement.x < 0) playerTransform.localScale = new Vector2(-1, 1);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }
}
