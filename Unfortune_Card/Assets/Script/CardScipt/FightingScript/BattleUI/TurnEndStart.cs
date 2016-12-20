using UnityEngine;
using System.Collections;

public class TurnEndStart : MonoBehaviour {

	public bool In = false;
	public float x;
	public float y;

	void Start()
	{
		x = transform.localScale.x;
		y = transform.localScale.y;
	}
	// Update is called once per frame
	void Update () 
	{
		if (In == true && x > 67) 
		{
			transform.localScale = new Vector3 (x, y, 100);
			x -= 3;
			y -= 3;
		}
		if (In == false && x < 103) 
		{
			transform.localScale = new Vector3 (x, y, 100);
			x += 3;
			y += 3;
		}
	}

	public void StartGetIn()
	{
		In = true;
		x = transform.localScale.x;
		y = transform.localScale.y;
	}

	public void StartGrtOut()
	{
		x = transform.localScale.x;
		y = transform.localScale.y;
		In = false;
	}
}
