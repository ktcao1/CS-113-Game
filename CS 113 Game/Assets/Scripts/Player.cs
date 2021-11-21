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
    public KeyCode attackKey, interactKey, dashKey;
    public KeyCode menuKey = KeyCode.Escape;
    [SerializeField] private Animator attackAnim, interactAnim;

    // Stats and Combat
    private int healthPoints;
    private int maxHealthPoints;
    private float damageCoolDown = 1f;
    private float damageLastTaken = -1f;

    // Room
    public GameObject currentRoom;

    private void Start()
    {
        // TODO: Change these when rebinding and saving/loading is implemented
        upKey = KeyCode.W;
        downKey = KeyCode.S;
        leftKey = KeyCode.A;
        rightKey = KeyCode.D;

        attackKey = KeyCode.F;
        interactKey = KeyCode.E;
        dashKey = KeyCode.Space;

        // Initialize stats
        maxHealthPoints = 5;
        healthPoints = maxHealthPoints;
        hp.SetMaxHealth(maxHealthPoints);

        // Initialize room
        currentRoom = GameObject.FindGameObjectWithTag("StartRoom");
    }

    private void Update()
    {
        attackAnim.SetBool("press", Input.GetKey(attackKey) ? true : false);
        interactAnim.SetBool("press", Input.GetKey(interactKey) ? true : false);

        if (GameManager.instance.isLoading) return;
        
        if (Input.GetKeyDown(attackKey))
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
