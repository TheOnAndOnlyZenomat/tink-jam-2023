using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnBehaviour {
	public GameObject enemyType;
	public int amount;
	public bool repeat;
	public int intervalSec;
}

public class Spawner : MonoBehaviour
{
	[SerializeField]
	private int spawnDelaySec = 0;

	// TODO(adrian): actually implement this
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
		Timer spawnDelayTimer = Instantiate(timer, this.transform);
		spawnDelayTimer.delay = spawnDelaySec;
		spawnDelayTimer.onTimerDone.AddListener(() => { StartSpawnRoutine(); });
		StartCoroutine(spawnDelayTimer.StartTimer());
	}

	// Update is called once per frame
	void Update()
	{

	}

	void StartSpawnRoutine() {
		foreach (SpawnBehaviour behaviour in spawnBehaviours) {
			StartCoroutine(SpawnBehaviourHandler(behaviour));
		}
	}

	IEnumerator SpawnBehaviourHandler(SpawnBehaviour behaviour) {
		Timer spawnTimer = Instantiate(timer, this.transform);
		spawnTimer.repeat = behaviour.repeat;
		spawnTimer.delay = behaviour.intervalSec;
		spawnTimer.onTimerDone.AddListener(() => {
			for (int i = 0; i < behaviour.amount; i++) {
				Instantiate(behaviour.enemyType, this.transform.position, Quaternion.identity);
			}
		});
		StartCoroutine(spawnTimer.StartTimer());
		yield return null;
	}
}
