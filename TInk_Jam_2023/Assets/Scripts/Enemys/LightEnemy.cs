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
	[SerializeField] private float stabDistance = 2f;
	[SerializeField] private float stabDamage = 10f;

	[SerializeField] private int maxHealth;

	[SerializeField] private int _scoreValue;

	private bool isCharging = false;
	private float chargeTimer = 0f;
	private float cooldownTimer = 0f;
	private int currentHealth;

	private Transform player;

	private float attackCooldownTimer;

	private Animator animController;

	[SerializeField]
	private AnimationClip attackAnim;
	[SerializeField]
	private AnimationClip deathAnim;

	[SerializeField] private AudioClip _attackSound;
	[SerializeField] private AudioClip _deathSound;
	private AudioSource _audioSource;

	private bool dying = false;
	private bool died = false;
	

	void Start()
	{
		// Assuming your player has a "Player" tag
		player = GameObject.FindGameObjectWithTag("Player").transform;
		currentHealth = maxHealth;

		this.animController = this.gameObject.GetComponent<Animator>();
		
		_audioSource = GetComponent<AudioSource>();
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
		if (dying == true) {
			foreach (BoxCollider2D collider in this.gameObject.GetComponents<BoxCollider2D>()) {
				collider.enabled = false;
			}

			if (died != true) {
				StartCoroutine(PlayDeath());
			}
			return;
		}

		if (cooldownTimer > 0)
		{
			MoveAway();
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

	void MoveAway()
	{
		// Move at slow speed
		Vector2 direction = -(player.position - transform.position).normalized;
		transform.Translate(direction * slowSpeed * Time.deltaTime);
	}

	void ChargeAtPlayer()
	{
		// Move towards the player at charge speed
		transform.position = Vector2.MoveTowards(transform.position, player.position, chargeSpeed * Time.deltaTime);

		if (isCharging && CanAttack())
		{
			float distanceToPlayer = Vector2.Distance(transform.position, player.position);

			animController.SetBool("isAttacking", true);
			_audioSource.PlayOneShot(_attackSound);
			attackCooldownTimer = attackCooldown;

			animController.transform.localScale = new Vector3(animController.transform.localScale.x * -1, animController.transform.localScale.y, animController.transform.localScale.z);

			//stabbing logic
			if (distanceToPlayer < stabDistance)
			{
				player.GetComponent<PlayerHealthManager>().PlayerTakeDamage((int)this.stabDamage);
			}
			StartCoroutine(WaitForAttackAnim());
		}
	}

	IEnumerator WaitForAttackAnim() {
		yield return new WaitForSeconds(this.attackAnim.length);
		animController.SetBool("isAttacking", false);
		animController.transform.rotation = new Quaternion(0, 0, 0, 0);
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0) {
			this.dying = true;
		}
	}

	IEnumerator PlayDeath() {
		this.died = true;
		animController.SetBool("isDying", true);
		_audioSource.PlayOneShot(_deathSound);
		yield return new WaitForSeconds(this.deathAnim.length);
		FindObjectOfType<ScoreManager>().IncreaseScore(_scoreValue);
		Destroy(this.gameObject);
	}
}

