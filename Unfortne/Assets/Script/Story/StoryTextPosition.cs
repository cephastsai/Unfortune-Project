using UnityEngine;
using System.Collections;

public class StoryTextPosition : MonoBehaviour {

	public GameObject LOG;

	void Update () {
		if(gameObject.transform.position != LOG.transform.position){
			gameObject.transform.position = LOG.transform.position;
		}
	}
}
