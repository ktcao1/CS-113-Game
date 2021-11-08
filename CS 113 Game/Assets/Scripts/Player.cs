using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private HealthBar hp;
    [SerializeField] private Weapon weapon;

    // Inputs
    public KeyCode upKey, downKey, leftKey, rightKey;
    public KeyCode fireKey;
    public KeyCode menuKey = KeyCode.Escape;

    // Stats and Combat
    private int healthPoints;
    private int maxHealthPoints;
    private float damageCoolDown = 1f;
    private float damageLastTaken = -1f;

    private void Start()
    {
        // TODO: Change these when rebinding and saving/loading is implemented
        upKey = KeyCode.W;
        downKey = KeyCode.S;
        leftKey = KeyCode.A;
        rightKey = KeyCode.D;
        fireKey = KeyCode.F;

        maxHealthPoints = 5;
        healthPoints = maxHealthPoints;

        hp.SetMaxHealth(maxHealthPoints);
    }

    private void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            weapon.Attack();
        }
        if (Input.GetKeyDown(menuKey))
        {
            GameManager.instance.PauseGame();
        }
    }

    // Combat functions
    // TODO: Add functionality for rebinding everytime player takes damage
    public void TakeDamage(Damage dmg)
    {
        if (Time.time - damageLastTaken > damageCoolDown)
        {
            damageLastTaken = Time.time;
            healthPoints -= dmg.damageAmount;
            if (healthPoints <= 0) Die();
            hp.SetHealth(Math.Max(healthPoints, 0));
        }
    }

    // TODO: Add animation and game over screen?
    private void Die()
    {
        Destroy(this.gameObject);
        GameManager.instance.EndScreen();
    }
}
