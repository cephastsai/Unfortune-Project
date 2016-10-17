using UnityEngine;
using System.Collections;

public class FireSparkSelfDestroy : MonoBehaviour {

	private int timer_i = 0;
	private bool start_timer3 = true;

	// Use this for initialization
	void Start () {
		
	}

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
		if (timer_i > 3) 
		{
			Destroy(gameObject);
		}
	}
}
