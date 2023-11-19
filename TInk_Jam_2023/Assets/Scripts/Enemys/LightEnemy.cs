using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy : MonoBehaviour
{
	[SerializeField] private float slowSpeed = 2f; // Speed when moving slowly
	[SerializeField] private float chargeSpeed = 6f; // Speed when charging
	[SerializeField] private float chargeDistance = 5f; // Distance to start charging
	[SerializeField] private float chargeDuration = 2f; // Duration of the charge
	[SerializeField] private float cooldownDuration = 5f; // Cooldown duration after charging
	[SerializeField] private float attackCooldown = 3f;
	[SerializeField] private float stabDuration = 1f;
	[SerializeField] private float stabDistance = 2f;
	[SerializeField] private float stabDamage = 10f;

	[SerializeField] private int maxHealth;

	private bool isCharging = false;
	private float chargeTimer = 0f;
	private float cooldownTimer = 0f;
	private int currentHealth;

	private Transform player;

	private float attackCooldownTimer;

	void Start()
	{
		// Assuming your player has a "Player" tag
		player = GameObject.FindGameObjectWithTag("Player").transform;
		currentHealth = maxHealth;
	}

	bool CanAttack()
	{
		// Check if the attack cooldown has expired and the player is within attack range
		return attackCooldownTimer <= 0 && Vector2.Distance(transform.position, player.position) < stabDistance;
	}

	bool CanCharge()
	{
		// Check if the attack cooldown has expired and the player is within attack range
		return cooldownTimer <= 0 && Vector2.Distance(transform.position, player.position) < chargeDistance;
	}

	void FixedUpdate()
	{
		// Update the attack cooldown timer
		if (attackCooldownTimer > 0)
		{
			attackCooldownTimer -= Time.fixedDeltaTime;
		}

		if (cooldownTimer > 0)
		{
			cooldownTimer -= Time.fixedDeltaTime;
		}
	}

	void Update()
	{
		if (cooldownTimer > 0)
		{
			MoveSlowly();
		}
		else
		{
			if (CanCharge()) {
				if (!isCharging) {
					isCharging = true;
					chargeTimer = chargeDuration;
					ChargeAtPlayer();
				}
				ChargeAtPlayer();
				chargeTimer -= Time.deltaTime;

				if (chargeTimer <= 0) {
					isCharging = false;
					cooldownTimer = cooldownDuration;
				}
			} else {
				MoveSlowly();
			}
		}
	}

	void MoveSlowly()
	{
		// Move at slow speed
		Vector2 direction = (player.position - transform.position).normalized;
		transform.Translate(direction * slowSpeed * Time.deltaTime);
	}

	void ChargeAtPlayer()
	{
		// Move towards the player at charge speed
		transform.position = Vector2.MoveTowards(transform.position, player.position, chargeSpeed * Time.deltaTime);

		if (isCharging && CanAttack())
		{
			attackCooldownTimer = attackCooldown;
			//stabbing logic
			float distanceToPlayer = Vector2.Distance(transform.position, player.position);
			if (distanceToPlayer < stabDistance)
			{
				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>().PlayerTakeDamage((int)this.stabDamage);
			}
		}
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}
}

