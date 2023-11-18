using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	[SerializeField]
	public int delay;
	public bool repeat;

	public UnityEvent onTimerDone;

	private bool running = true;

	public IEnumerator StartTimer()
	{
		if (repeat) {
			while (running) {
				yield return new WaitForSeconds(delay);

				onTimerDone.Invoke();
			}
		} else {
			yield return new WaitForSeconds(delay);

			onTimerDone.Invoke();
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
