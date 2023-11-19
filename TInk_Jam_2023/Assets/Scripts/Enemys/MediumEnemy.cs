using System.Collections;
using UnityEngine;

public class MediumEnemyBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f; // Speed of the medium enemy

    [SerializeField] private int maxHealth;
    
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float slashDistance = 2f;
    [SerializeField] private float slashDamage = 10f;
    
    private Transform player;

    private float attackCooldownTimer;
    private int currentHealth;

	private Animator animController;

	[SerializeField]
	private AnimationClip attackAnim;
	[SerializeField]
	private AnimationClip deathAnim;

	private bool dying;
	private bool died;

	private bool hitAlready = false;
	[SerializeField] private int _scoreValue;
	
	[SerializeField] private AudioClip _attackSound;
	[SerializeField] private AudioClip _deathSound;
	private AudioSource _audioSource;

	void Start()
    {
        // Assuming your player has a "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;

		this.animController = this.gameObject.GetComponent<Animator>();
		_audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
		if (dying) {
			foreach (BoxCollider2D collider in this.gameObject.GetComponents<BoxCollider2D>()) {
				collider.enabled = false;
			}

			if (died != true) {
				StartCoroutine(PlayDeath());
			}
			return;
		}

        if (CanAttack())
        {
            Attack();
        }
        // Move towards the player at a constant speed
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        // Move towards the player at the specified speed
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
    
    void Attack()
    {
        // Add your attack logic here

        // Optionally, deal damage to the player or trigger an attack animation
        // You can replace this with your own attack logic
        
		this.animController.SetBool("isAttacking", true);
		_audioSource.PlayOneShot(_attackSound);
		StartCoroutine(WaitForAttackAnim());

        // Reset the attack cooldown timer
		//
        attackCooldownTimer = attackCooldown;
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && hitAlready == false) {
			hitAlready = true;
			player.gameObject.GetComponent<PlayerHealthManager>().PlayerTakeDamage((int)this.slashDamage);
		}
	}

	IEnumerator WaitForAttackAnim() {
		yield return new WaitForSeconds(this.attackAnim.length);
		animController.SetBool("isAttacking", false);
	}

	IEnumerator PlayDeath() {
		this.died = true;
		animController.SetBool("isDying", true);
		_audioSource.PlayOneShot(_deathSound);
		yield return new WaitForSeconds(this.deathAnim.length);
		FindObjectOfType<ScoreManager>().IncreaseScore(_scoreValue);
		Destroy(this.gameObject);
	}

    bool CanAttack()
    {
        // Check if the attack cooldown has expired and the player is within attack range
        return attackCooldownTimer <= 0 && Vector2.Distance(transform.position, player.position) < slashDistance;
    }

    void FixedUpdate()
    {
        // Update the attack cooldown timer
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.fixedDeltaTime;
        }
		if (attackCooldownTimer <= 0) {
			this.hitAlready = false;
		}
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
			this.dying = true;
        }
    }

}
