using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
	private Transform player;

	[SerializeField]
	private float smoothing;

    // Start is called before the first frame update
    void Start()
    {
		foreach(Transform child in this.transform.parent) {
			if (child.tag == "Player")
				this.player = child;
		}
    }

    void FixedUpdate()
    {
		Vector3 targetPosition = new Vector3(player.position.x, player.position.y, this.transform.position.z); 

		transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
