using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy : MonoBehaviour
{
    [SerializeField] private float slowSpeed = 2f; // Speed when moving slowly
    [SerializeField] private float chargeSpeed = 6f; // Speed when charging
    [SerializeField] private float chargeDistance = 5f; // Distance to start charging
    [SerializeField] private float chargeDuration = 2f; // Duration of the charge
    [SerializeField] private float cooldownDuration = 3f; // Cooldown duration after charging
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float stabDuration = 1f;
    [SerializeField] private float stabDistance = 2f;
    [SerializeField] private float stabDamage = 10f;

    [SerializeField] private int maxHealth;

    private bool isCharging = false;
    private float chargeTimer = 0f;
    private float cooldownTimer = 0f;
    private int currentHealth;

    private Transform player;

    void Start()
    {
        // Assuming your player has a "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            // If in cooldown, decrement the cooldown timer and move slowly
            cooldownTimer -= Time.deltaTime;
            MoveSlowly();
        }
        else
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < chargeDistance)
            {
                if (!isCharging)
                {
                    // Start charging
                    isCharging = true;
                    chargeTimer = chargeDuration;
                    ChargeAtPlayer();
                }

                // While charging, move at charge speed
                ChargeAtPlayer();

                // Decrement the charge timer
                chargeTimer -= Time.deltaTime;

                if (chargeTimer <= 0)
                {
                    // Stop charging, start cooldown
                    isCharging = false;
                    cooldownTimer = cooldownDuration;
                }
            }
            else
            {
                // If the player is out of range, move slowly
                MoveSlowly();
            }
        }
    }

    void MoveSlowly()
    {
        // Move at slow speed
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * slowSpeed * Time.deltaTime);
    }

    void ChargeAtPlayer()
    {
        // Move towards the player at charge speed
        transform.position = Vector2.MoveTowards(transform.position, player.position, chargeSpeed * Time.deltaTime);
        
        /*if (isCharging)
        {
            //stabbing logic
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer < stabDistance)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(stabDamage);
                }
                
                // Play attack animation here
            }
        }*/
    }

    public void TakeDamage(int damage)
    {
		Debug.Log("taking damage in light: " + damage);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
} 

