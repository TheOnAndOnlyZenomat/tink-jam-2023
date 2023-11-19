using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    public void UpdateHealthBar(int _currentHealth, int _maxHealth)
    {
        if (_healthBar != null)
        {
            float fillAmount = (float)_currentHealth / _maxHealth;
            _healthBar.fillAmount = fillAmount;
        }
    }
}
