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

	[SerializeField]
	private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
		timer.delay = spawnDelaySec;
		timer.onTimerDone.AddListener(() => { Debug.Log("timer done"); });
		StartCoroutine(timer.StartTimer());

		while (true) {}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
