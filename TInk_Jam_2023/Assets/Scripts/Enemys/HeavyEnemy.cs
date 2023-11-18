using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : MonoBehaviour
{
    [Tooltip("Max Health of the Enemy")]
    [SerializeField] private int maxHealth;

    [Tooltip("Movement Speed of the Enemy")]
    [SerializeField] private float moveSpeed;

    [Tooltip("Basic Player Attack Damage")] 
    [SerializeField] private int playerAttackDamage;
    
    [Tooltip("Basic Player Ability1 Damage")] 
    [SerializeField] private int playerAbility1Damage;
    
    [Tooltip("Basic Player Ability2 Damage")] 
    [SerializeField] private int playerAbility2Damage;
    
    [Tooltip("Basic Player Ability3 Damage")] 
    [SerializeField] private int playerAbility3Damage;

    [Tooltip("Detection Range for the player")] 
    [SerializeField] private float detectionRange;
    
    private int currentHealth;
    
    private Transform playerTransform;
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(currentHealth == 0)
            Destroy(gameObject);

        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= detectionRange)
            {
                MoveTowardsPlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("PlayerWeapon"))
        {
            currentHealth = maxHealth - playerAttackDamage;
        }
        
        if (CompareTag("PlayerAbility1"))
        {
            currentHealth = maxHealth - playerAbility1Damage;
        }
        
        if (CompareTag("PlayerAbility2"))
        {
            currentHealth = maxHealth - playerAbility2Damage;
        }
        
        if (CompareTag("PlayerAbility3"))
        {
            currentHealth = maxHealth - playerAbility3Damage;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 moveDirection = (playerTransform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
    }
}
