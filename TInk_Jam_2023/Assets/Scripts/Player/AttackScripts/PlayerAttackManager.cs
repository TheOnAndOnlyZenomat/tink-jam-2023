using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
	private Transform attackOrb;
	private PlayerAttackOrbManager attackOrbScript;
	
	[SerializeField]
	private GameObject stabAttack;
	private AttackInfo stabAttackInfo;

	[SerializeField]
	private GameObject coneAttack;
	private AttackInfo coneAttackInfo;

	[SerializeField]
	private GameObject circleAttack;
	private AttackInfo circleAttackInfo;

	[SerializeField]
	private float maxStamina = 100;
	[SerializeField]
	private float staminaRegenInterval = 1;
	[SerializeField]
	private float staminaRegenPerInterval = 1;
	private float currentStamina;

	private float staminaTimer = 1;

	private bool canAttack = true;

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
    }

	void FixedUpdate() {
		staminaTimer -= Time.deltaTime;
		if (staminaTimer <= 0) {
			if ((currentStamina <= maxStamina) == true) {
				currentStamina = Mathf.Clamp(currentStamina + staminaRegenPerInterval, currentStamina + staminaRegenPerInterval, maxStamina);
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
		currentStamina -= stabAttackInfo.getStaminaCost();
		Quaternion attackDir = attackOrb.rotation;
		attackOrbScript.setRotate(false);
		GameObject attackObject = Instantiate(stabAttack, attackOrb.transform.position, attackDir, this.transform);
		yield return new WaitForSeconds(stabAttackInfo.getAttackDuration());
		Destroy(attackObject);
		attackOrbScript.setRotate(true);
		canAttack = true;
	}

	IEnumerator OnConeAttack() {
		if (!canAttack) yield break;
		if ((currentStamina >= coneAttackInfo.getStaminaCost()) == false) {
			yield break;
		}
		canAttack = false;
		currentStamina -= coneAttackInfo.getStaminaCost();
		Quaternion attackDir = attackOrb.rotation;
		attackOrbScript.setRotate(false);
		GameObject attackObject = Instantiate(coneAttack, attackOrb.transform.position, attackDir, this.transform);
		yield return new WaitForSeconds(coneAttackInfo.getAttackDuration());
		Destroy(attackObject);
		attackOrbScript.setRotate(true);
		canAttack = true;
	}

	IEnumerator OnCircleAttack() {
		if (!canAttack) yield break;
		if ((currentStamina >= circleAttackInfo.getStaminaCost()) == false) {
			yield break;
		}
		canAttack = false;
		currentStamina -= circleAttackInfo.getStaminaCost();
		Quaternion attackDir = attackOrb.rotation;
		attackOrbScript.setRotate(false);
		GameObject attackObject = Instantiate(circleAttack, this.transform.position, attackDir, this.transform);
		yield return new WaitForSeconds(circleAttackInfo.getAttackDuration());
		Destroy(attackObject);
		attackOrbScript.setRotate(true);
		canAttack = true;
	}

	public void hitEnemy(AttackInfo attack, Transform hitEnemy) {
		TakeDamageScript damageScript = hitEnemy.gameObject.GetComponent<TakeDamageScript>();
		damageScript.TakeDamage(attack.getDamage());
	}
}
