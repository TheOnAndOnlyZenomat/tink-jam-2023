using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f; // Speed of the medium enemy

    [SerializeField] private int maxHealth;
    
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float slamDuration = 1f;
    [SerializeField] private float slamDistance = 2f;
    [SerializeField] private float slamDamage = 10f;
    
    private Transform player;

    private float attackCooldownTimer;
    private int currentHealth;

    void Start()
    {
        // Assuming your player has a "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (CanAttack())
        {
            Attack();
        }
        // Move towards the player at a constant speed
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        // Move towards the player at the specified speed
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
    
    void Attack()
    {
        // Add your attack logic here

        // Optionally, deal damage to the player or trigger an attack animation
        // You can replace this with your own attack logic
        
		player.gameObject.GetComponent<PlayerHealthManager>().PlayerTakeDamage((int)this.slamDamage);

        // Reset the attack cooldown timer
		//
        attackCooldownTimer = attackCooldown;
    }

    bool CanAttack()
    {
        // Check if the attack cooldown has expired and the player is within attack range
        return attackCooldownTimer <= 0 && Vector2.Distance(transform.position, player.position) < slamDistance;
    }

    void FixedUpdate()
    {
        // Update the attack cooldown timer
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.fixedDeltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
