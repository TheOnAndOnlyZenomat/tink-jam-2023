using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackOrbManager : MonoBehaviour
{
	private Transform player;
	private bool rotate = true;

	[SerializeField]
	private float rotateSpeed = 100;

	// Start is called before the first frame update
	void Start()
	{
		player = this.transform.parent;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate() {
		if (rotate){
			this.transform.RotateAround(player.position, new Vector3(0, 0, 1), this.rotateSpeed * Time.deltaTime);
		}
	}

	public void setRotate(bool rotate) {
		this.rotate = rotate;
	}

	public void hideOrb(bool hide) {
		this.GetComponent<SpriteRenderer>().enabled = !hide;
	}
}
