using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _dashingPower = 15f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;
    private Vector2 _movementInput;
    private Rigidbody2D _rb;
    private bool _canDash = true;
    private bool _isDashing;
    
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
            return;
        }
        _rb.velocity = _movementInput * _speed;
    }

    private void OnPlayerMovement(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }

    private void OnDash()
    {
        if(_canDash)
            StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        _rb.velocity = _movementInput * _dashingPower;
        yield return new WaitForSeconds(_dashingTime);
        _isDashing = false;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
}
