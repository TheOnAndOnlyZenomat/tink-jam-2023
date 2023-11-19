using System.Collections;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
	private Transform attackOrb;
	private PlayerAttackOrbManager attackOrbScript;
	
	[SerializeField]
	private GameObject stabAttack;
	[SerializeField]
	private AudioClip stabAudio;
	private AttackInfo stabAttackInfo;

	[SerializeField]
	private GameObject coneAttack;
	[SerializeField]
	private AudioClip coneAudio;
	private AttackInfo coneAttackInfo;

	[SerializeField]
	private GameObject circleAttack;
	[SerializeField]
	private AudioClip circleAudio;
	private AttackInfo circleAttackInfo;

	private AudioSource audioSource;

	[SerializeField]
	private float maxStamina = 100;
	[SerializeField]
	private float staminaRegenInterval = 1;
	[SerializeField]
	private float staminaRegenPerInterval = 1;
	private float currentStamina;

	private float staminaTimer = 1;

	private bool canAttack = true;

	[SerializeField]
	private StaminaBarManager staminaBar;

    // Start is called before the first frame update
    void Start()
    {
		this.currentStamina = maxStamina;

        foreach (Transform child in this.transform) {
			if (child.gameObject.tag == "PlayerAttackOrb") {
				this.attackOrb = child;
				this.attackOrbScript = child.GetComponent<PlayerAttackOrbManager>();
			}
		}

		foreach (Transform child in this.stabAttack.transform) {
			if (child.GetComponent<AttackInfo>() != null) {
				this.stabAttackInfo = child.GetComponent<AttackInfo>();
				break;
			}
		}
		foreach (Transform child in this.coneAttack.transform) {
			if (child.GetComponent<AttackInfo>() != null) {
				this.coneAttackInfo = child.GetComponent<AttackInfo>();
				break;
			}
		}
		foreach (Transform child in this.circleAttack.transform) {
			if (child.GetComponent<AttackInfo>() != null) {
				this.circleAttackInfo = child.GetComponent<AttackInfo>();
				break;
			}
		}

		audioSource = GetComponent<AudioSource>();
    }

	void FixedUpdate() {
		staminaTimer -= Time.deltaTime;
		if (staminaTimer <= 0) {
			if ((currentStamina <= maxStamina) == true) {
				currentStamina = Mathf.Clamp(currentStamina + staminaRegenPerInterval, currentStamina + staminaRegenPerInterval, maxStamina);
				staminaBar.UpdateStaminaBar(currentStamina, maxStamina);
			}
			staminaTimer = staminaRegenInterval;
		}
	}

	IEnumerator OnStabAttack() {
		if (!canAttack) yield break;
		if ((currentStamina >= stabAttackInfo.getStaminaCost()) == false) {
			yield break;
		}

		canAttack = false;

		subtractStamina(stabAttackInfo.getStaminaCost());

		Quaternion attackDir = attackOrb.rotation;
		attackOrbScript.setRotate(false);
		attackOrbScript.hideOrb(true);

		GameObject attackObject = Instantiate(stabAttack, attackOrb.transform.position, attackDir, this.transform);
		audioSource.PlayOneShot(this.stabAudio);

		yield return new WaitForSeconds(stabAttackInfo.getAttackDuration());

		Destroy(attackObject);

		attackOrbScript.hideOrb(false);
		attackOrbScript.setRotate(true);
		canAttack = true;
	}

	IEnumerator OnConeAttack() {
		if (!canAttack) yield break;
		if ((currentStamina >= coneAttackInfo.getStaminaCost()) == false) {
			yield break;
		}

		canAttack = false;

		subtractStamina(coneAttackInfo.getStaminaCost());

		Quaternion attackDir = attackOrb.rotation;
		attackOrbScript.setRotate(false);
		attackOrbScript.hideOrb(true);

		GameObject attackObject = Instantiate(coneAttack, attackOrb.transform.position, attackDir, this.transform);
		audioSource.PlayOneShot(this.coneAudio);

		yield return new WaitForSeconds(coneAttackInfo.getAttackDuration());

		Destroy(attackObject);
		attackOrbScript.hideOrb(false);
		attackOrbScript.setRotate(true);
		canAttack = true;
	}

	IEnumerator OnCircleAttack() {
		if (!canAttack) yield break;
		if ((currentStamina >= circleAttackInfo.getStaminaCost()) == false) {
			yield break;
		}

		canAttack = false;

		subtractStamina(circleAttackInfo.getStaminaCost());

		Quaternion attackDir = attackOrb.rotation;
		attackOrbScript.setRotate(false);
		attackOrbScript.hideOrb(true);

		GameObject attackObject = Instantiate(circleAttack, this.transform.position, attackDir, this.transform);
		audioSource.PlayOneShot(this.circleAudio);

		yield return new WaitForSeconds(circleAttackInfo.getAttackDuration());

		Destroy(attackObject);
		attackOrbScript.hideOrb(false);
		attackOrbScript.setRotate(true);
		canAttack = true;
	}

	public void hitEnemy(AttackInfo attack, Transform hitEnemy) {
		TakeDamageScript damageScript = hitEnemy.gameObject.GetComponent<TakeDamageScript>();
		damageScript.TakeDamage(attack.getDamage());
	}

	public void subtractStamina(float amount) {
		currentStamina -= amount;
		staminaBar.UpdateStaminaBar(currentStamina, maxStamina);
	}

	public float getStamina() {
		return currentStamina;
	}
}
