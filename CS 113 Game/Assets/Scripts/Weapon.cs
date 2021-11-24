using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator attackAnim;
    [SerializeField] private Image attackIcon;
    [SerializeField] private Sprite attackSprite, greenAttackSprite;

    // Damage
    public int damageValue = 5;
    public float pushForce = 2f;

    // Cooldown
    private float cooldown = 0.5f;
    private float lastAttack = -0.5f;

    // Upgrade
    public int weaponLevel = 0;

    void Update()
    {
        if (Time.time - lastAttack <= cooldown)
        {
            attackAnim.SetBool("press", true);
            attackIcon.sprite = greenAttackSprite;
        }
        else
        {
            attackAnim.SetBool("press", false);
            attackIcon.sprite = attackSprite;
        }
    }

    public void Attack()
    {
        if (Time.time - lastAttack > cooldown)
        {
            lastAttack = Time.time;
            animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag != "Enemy") return;

        Damage dmg = new Damage
        {
            damageAmount = damageValue,
            pushForce = pushForce
        };

        Enemy enemy = collider2D.GetComponent<Enemy>();
        enemy.TakeDamage(dmg);
    }
}
