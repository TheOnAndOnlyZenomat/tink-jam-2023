using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageScript : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void TakeDamage(int damage) {
		string name = this.name;
		if (name == "Light Enemy") {
			this.GetComponent<LightEnemy>().TakeDamage(damage);
		} else if (name == "MediumEnemy") {
			this.GetComponent<MediumEnemyBehavior>().TakeDamage(damage);
		} else if (name == "HeavyEnemy") {
			this.GetComponent<HeavyEnemy>().TakeDamage(damage);
		}
	}
}
