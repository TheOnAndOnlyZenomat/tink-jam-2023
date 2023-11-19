using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarManager : MonoBehaviour
{
    [SerializeField] private Image _staminarBar;

    public void UpdateStaminaBar(float _currentStamina, float _maxStamina)
    {
        if (_staminarBar != null)
        {
            float fillAmount = _currentStamina / _maxStamina;
            _staminarBar.fillAmount = fillAmount;
        }
    }
}
