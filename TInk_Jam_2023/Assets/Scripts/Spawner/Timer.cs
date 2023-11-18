using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	[SerializeField]
	private int delay;

	private UnityEvent onTimerDone;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);

		onTimerDone.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
