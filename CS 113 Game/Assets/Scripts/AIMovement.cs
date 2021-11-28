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

    public Damage damage;
    public bool pushed = false;
    public Vector3 pushedDestination;
    
    void Start()
    {
        agent = GetComponent<Transform>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        movespeed = 2f;
    }

    void Update()
    {
        if (target != null && !pushed) 
        {
            spriteRenderer.flipX = (target.transform.position.x < transform.position.x) ? true : false;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, movespeed * Time.deltaTime);
        }
        else if (target != null && pushed && Vector3.Distance(transform.position, pushedDestination) >= 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, pushedDestination, (movespeed+1) * Time.deltaTime);
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (target != null && pushed && Vector3.Distance(transform.position, pushedDestination) < 0.1f)
        {
            pushed = false;
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    public void PushForce(Damage dmg)
    {
        if (!target.GetComponent<Player>().hasKnockBack) return;
        pushed = true;
        damage = dmg;
        pushedDestination = transform.position + (transform.position - damage.origin).normalized * damage.pushForce;
    }
}
