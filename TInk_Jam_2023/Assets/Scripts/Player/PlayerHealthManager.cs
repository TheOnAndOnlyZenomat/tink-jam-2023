using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [SerializeField] private HealthBarManager _healthManager;
    

    private void Start()
    {
        _currentHealth = _maxHealth;
        if (_healthManager==null)
        {
            Debug.Log("HealthManager not found");
        }

        UpdateHealthBar();
    }

    public void PlayerTakeDamage(int damage)
    {
		Debug.Log("Taking Damage");
        _currentHealth -= damage;
        
        UpdateHealthBar();

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Player has died");
    }

    void UpdateHealthBar()
    {
        if (_healthManager != null)
        {
            _healthManager.UpdateHealthBar(_currentHealth,_maxHealth);
        }
    }
}
