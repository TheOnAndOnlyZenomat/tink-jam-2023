using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy : MonoBehaviour
{
    [Tooltip("Max Health of the Enemy")]
    [SerializeField] private int maxHealth;

    [Tooltip("Damage of the Enemy")]
    [SerializeField] private int damage;

    [Tooltip("Movement Speed of the Enemy")]
    [SerializeField] private float moveSpeed;

    private Transform playerTransform;
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectsWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("PlayerWeapon"))
        {
            
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 moveDirection = (playerTransform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
    }
}
