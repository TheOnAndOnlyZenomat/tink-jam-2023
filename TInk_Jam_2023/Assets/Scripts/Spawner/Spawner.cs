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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
