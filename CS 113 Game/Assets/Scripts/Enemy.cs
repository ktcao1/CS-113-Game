using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthBarEnemy hp;

    // Stats
    public int healthPoints;
    public int maxHealthPoints;
    public int damage;

    private void Start()
    {
        maxHealthPoints = 15;
        healthPoints = maxHealthPoints;
        damage = 1;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(new Damage{damageAmount = damage, pushForce = 0});
        }
    }

    public void TakeDamage(Damage dmg)
    {
        healthPoints -= dmg.damageAmount;
        if (healthPoints <= 0) Die();
        hp.UpdateHealth((float)healthPoints / maxHealthPoints);
    }

    private void Die()
    {
        hp.UpdateHealth(1f/1f);
        Instantiate(this.gameObject.transform, new Vector3(0,0,0), Quaternion.identity);
        Destroy(this.gameObject);
    }
}
