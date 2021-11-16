using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 movement;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Vector3 lastMoveDir;

    // I-Frame dash
    [SerializeField] private LayerMask dashLayerMask;
    private float lastDash = -1.5f;
    private float dashCooldown = 1.5f;
    private float immuneTime = 0.5f;
    private float phaseTime = 0.2f;
    public bool isImmune = false;
    private float dashAmount = 1.5f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        bc = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        HandleMovement();
        HandleDash();
    }

    private void HandleMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x > 0) playerTransform.localScale = new Vector2(1, 1);
        else if (movement.x < 0) playerTransform.localScale = new Vector2(-1, 1);
        if (movement != Vector2.zero)
            lastMoveDir = movement;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void HandleDash()
    {
        // Immunity to enemies while dashing
        if (isImmune && immuneTime > 0) {
            immuneTime -= Time.deltaTime;
        }
        else {
            isImmune = false;
            immuneTime = 0.5f;
        }
        if (bc.isTrigger && phaseTime > 0) {
            phaseTime -= Time.deltaTime;
        }
        else {
            bc.isTrigger = false;
            phaseTime = 0.2f;
        }

        // Dash logic
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Time.time - lastDash > dashCooldown)
            {
                lastDash = Time.time;
                isImmune = true;

                Vector3 dashPosition = transform.position + lastMoveDir * dashAmount;
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, lastMoveDir, dashAmount, dashLayerMask);

                if (raycastHit2D.collider != null) 
                {
                    rb.MovePosition(raycastHit2D.point);
                }
                else
                {
                    bc.isTrigger = true;
                    rb.MovePosition(dashPosition);
                }
            }
        }
    }
}
