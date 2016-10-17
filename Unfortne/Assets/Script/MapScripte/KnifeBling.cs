using UnityEngine;
using System.Collections;

public class KnifeBling : MonoBehaviour {

	public bool BlingStart = false;
	public float a;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		if(BlingStart)
		GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, a);
		a = Mathf.PingPong (Time.time, 0.7f)+0.3f;
	}
	public void StartBling()
	{
		BlingStart = true;
	}

	public void StopBling()
	{
		GetComponent<SpriteRenderer> ().color = new Vector4 (1, 1, 1, 1);
		a =1;
		BlingStart = false;
	}
}
