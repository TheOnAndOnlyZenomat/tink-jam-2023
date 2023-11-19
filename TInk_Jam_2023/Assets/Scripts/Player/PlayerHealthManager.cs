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
    [SerializeField] private Animator _animator;

	[SerializeField] AnimationClip deathAnim;
    
	[SerializeField] GameObject deathScreen;

	[SerializeField] private GameObject scoreManager;
	private ScoreManager scoreManagerScript;

	[SerializeField]
	private AudioClip deathAudio;

	private AudioSource audioSource;

    private void Start()
    {
        _currentHealth = _maxHealth;
        if (_healthManager==null)
        {
            Debug.Log("HealthManager not found");
        }

        UpdateHealthBar();

		audioSource = GetComponent<AudioSource>();

		scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
    }

    public void PlayerTakeDamage(int damage)
    {
		Debug.Log("Player taking Damage");
        _currentHealth -= damage;
        _animator.SetInteger("AnimationCurrentHealth", _currentHealth);
        
        UpdateHealthBar();

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        _animator.SetBool("IsDead",true);
		StartCoroutine(DieAndEnd());
		audioSource.PlayOneShot(deathAudio);
    }

	IEnumerator DieAndEnd() {
		yield return new WaitForSeconds(deathAnim.length);
		Time.timeScale = 0f;
		deathScreen.SetActive(true);
		Destroy(this.gameObject);
	}
    
    void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
    }

    void UpdateHealthBar()
    {
        if (_healthManager != null)
        {
            _healthManager.UpdateHealthBar(_currentHealth,_maxHealth);
        }
    }
}
