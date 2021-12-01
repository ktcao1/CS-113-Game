using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public DemonEnemy demon;
    public Vector3 shootDirection;
    private bool hitWall;
    private float travelSpeed = 7f;

    void Update()
    {
        transform.position = transform.position + shootDirection * travelSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
        else if (collider2D.tag == "Player")
        {
            Damage dmg = new Damage
            {
                damageAmount = demon.damage,
                pushForce = 0f,
                origin = transform.position
            };

            Player player = collider2D.GetComponent<Player>();
            player.TakeDamage(dmg);
            Destroy(gameObject);
        }
    }
}

