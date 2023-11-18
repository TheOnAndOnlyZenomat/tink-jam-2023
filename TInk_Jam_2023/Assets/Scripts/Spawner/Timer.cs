using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	[SerializeField]
	public int delay;

	public UnityEvent onTimerDone;

    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(delay);

		onTimerDone.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
