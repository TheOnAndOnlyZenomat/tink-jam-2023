using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnBehaviour {
	public GameObject enemyType;
	public int intervalSec;
}

public class Spawner : MonoBehaviour
{
	[SerializeField]
	private int spawnDelaySec = 0;

	[SerializeField]
	private int spawnDurationSec;
	[SerializeField]
	private int spawnRepeats;

	[SerializeField]
	private SpawnBehaviour[] spawnBehaviours;

	private GameObject timer;

    // Start is called before the first frame update
    void Start()
    {
		timer = Resources.Load("Timer") as GameObject;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
