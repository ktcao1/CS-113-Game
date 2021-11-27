using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Weapon weapon;
    public Vector3 shootDirection;
    private float travelSpeed = 7f;

    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>();
    }

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
        else if (collider2D.tag == "Enemy")
        {
            Damage dmg = new Damage
            {
                damageAmount = weapon.damageValue,
                pushForce = 0f,
                origin = gameObject.transform.position
            };

            Enemy enemy = collider2D.GetComponent<Enemy>();
            enemy.TakeDamage(dmg);
            Destroy(gameObject);
        }
    }
}
