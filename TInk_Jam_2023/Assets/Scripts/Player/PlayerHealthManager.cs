using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void PlayerTakeDamage(int damage)
    {
		Debug.Log("Taking Damage");
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Player has died");
    }
}
