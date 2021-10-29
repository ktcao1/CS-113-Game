using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthBar hp;

    // Stats
    public int healthPoints;
    public int maxHealthPoints;

    private void Start()
    {
        maxHealthPoints = 15;
        healthPoints = maxHealthPoints;
    }

    private void Update()
    {
    }

    public void TakeDamage(Damage dmg)
    {
        healthPoints -= dmg.damageAmount;
        if (healthPoints <= 0) Die();
        hp.UpdateHealth((float)healthPoints / maxHealthPoints);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
