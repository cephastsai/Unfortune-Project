using UnityEngine;
using System.Collections;

public class LightSelfDestroy : MonoBehaviour {

	private int timer_i = 0;
	private bool start_timer3 = true;

	IEnumerator timer3()
	{
		yield return new WaitForSeconds (1);
		timer_i++;
		start_timer3 = true;
	}

	// Update is called once per frame
	void Update () {
		if (start_timer3) 
		{
			StartCoroutine ("timer3");
			start_timer3 = false;
		}
		if (timer_i > 2) 
		{
			Destroy(gameObject);
		}
	}
}
