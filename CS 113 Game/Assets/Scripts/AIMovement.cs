using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public Transform agent;
    public Transform target;
    public float movespeed;
    public SpriteRenderer spriteRenderer;
    
    void Start()
    {
        agent = GetComponent<Transform>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        movespeed = 2.5f;
    }

    void Update()
    {
        if (target != null) 
        {
            spriteRenderer.flipX = (target.transform.position.x < transform.position.x) ? true : false;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, movespeed * Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        // if (collision2D.gameObject.tag == "Obstacle")
        // {
        //     transform.position = Vector2.Perpendicular
        // }
    }
}
