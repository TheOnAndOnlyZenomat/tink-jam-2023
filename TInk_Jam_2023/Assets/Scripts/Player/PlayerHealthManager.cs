using System;
using System.Collections;
using System.Collections.Generic;
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
        _currentHealth = _currentHealth - damage;
    }
}
