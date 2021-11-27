using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    // Inputs
    public KeyCode upKey, downKey, leftKey, rightKey;
    public KeyCode attackKey, interactKey, dashKey;
    public KeyCode menuKey = KeyCode.Escape;
    [SerializeField] private Animator interactAnim;
    [SerializeField] private Image interactIcon;
    [SerializeField] private Sprite interactSprite, greenInteractSprite;

    // Stats and Combat
    private int healthPoints;
    private int maxHealthPoints;
    private float damageCoolDown = 1f;
    private float damageLastTaken = -1f;
    public bool hasKnockBack = false;

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
        ZeldaHealthBar.instance.SetupHearts(maxHealthPoints);

        // Initialize room
        currentRoom = GameObject.FindGameObjectWithTag("StartRoom");
    }

    private void Update()
    {
        if (GameManager.instance.disableInputs) return;

        interactAnim.SetBool("press", Input.GetKey(interactKey) ? true : false);
        interactIcon.sprite = interactAnim.GetBool("press") ? greenInteractSprite : interactSprite;

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
            ZeldaHealthBar.instance.RemoveHearts(dmg.damageAmount);
            if (ZeldaHealthBar.instance.currentHearts <= 0) Die();
        }
    }

    // TODO: Add animation and game over screen?
    private void Die()
    {
        Destroy(this.gameObject);
        GameManager.instance.EndScreen();
    }
}
