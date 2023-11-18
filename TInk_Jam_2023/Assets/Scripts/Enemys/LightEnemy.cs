using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int playerAttackDamage;
    
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float idleSpeed = 2f;
    [SerializeField] private float chargeSpeed = 8f;
    [SerializeField] private float chargeDistance = 7f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float daggerSpeed = 10f;

    [SerializeField] private GameObject daggerPrefab;
    
    private Transform playerTransform;
    private Vector2 chargeDirection;
    private bool isCharging = false;
    
    private bool canTakeDamage = true;
    private bool canAttack = true;
    
    private int currentHealth;
    
    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!isCharging)
        {
            RotateTowardsPlayer();
            CheckForCharge();
            Move(idleSpeed);
        }
        else
        {
            Move(chargeSpeed);
            Charge();
        }
    }

    // method to handle the rotation towards the player
    private void RotateTowardsPlayer()
    {
        Vector2 directionToPlayer = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    // method to check if enemy should start charging towards player
    private void CheckForCharge()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= chargeDistance)
        {
            chargeDirection = (playerTransform.position - transform.position).normalized;
            isCharging = true;
        }
    }

    // method to handle the actual charging
    private void Charge()
    {
        transform.position += (Vector3)chargeDirection * chargeSpeed * Time.deltaTime;
        if (canAttack == true)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("Enemy is attacking!");

        GameObject dagger = Instantiate(daggerPrefab, transform.position, Quaternion.identity, transform);

        Vector2 attackDirection = (playerTransform.position - transform.position).normalized;

        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
        dagger.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        canAttack = false;
        StartCoroutine(ResetAttackCooldown());
    }
    
    // method to handle movement
    private void Move(float speed)
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with wall");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with Player");
            canTakeDamage = false;
            currentHealth -= playerAttackDamage;
            StartCoroutine(ResetDamageCooldown());
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy Destroyed");
            Destroy(gameObject);
        }
    }

    private IEnumerator ResetDamageCooldown()
    {
        yield return new WaitForSeconds(2f);
        canTakeDamage = true;
    }
    
    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canTakeDamage = true;
    }
} 

