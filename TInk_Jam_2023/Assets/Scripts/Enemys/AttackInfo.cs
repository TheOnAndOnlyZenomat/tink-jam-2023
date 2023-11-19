using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfo : MonoBehaviour
{
	[SerializeField]
	private int damage = 10;
	[SerializeField]
	private RuntimeAnimatorController animController;
	[SerializeField]
	private AnimationClip stabAnimation;
	[SerializeField]
	private float staminaCost;

	private PlayerAttackManager playerAttackScript;

    // Start is called before the first frame update
    void Start()
    {
		this.playerAttackScript = this.transform.parent.parent.GetComponent<PlayerAttackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			this.playerAttackScript.hitEnemy(this, other.transform);
		}
	}

	public int getDamage() {
		return this.damage;
	}

	public float getAttackDuration() {
		return this.stabAnimation.length;
	}

	public RuntimeAnimatorController getAnimatorController() {
		return this.animController;
	}

	public float getStaminaCost() {
		return this.staminaCost;
	}
}
