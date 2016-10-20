using UnityEngine;
using System.Collections;

public class LockingFrame : MonoBehaviour {

	public bool XLock = false;
	public bool YLock = false;
	public float ScaleX;
	public float ScaleY;
	// Use this for initialization
	void Start () {		
		XLock = true;
		YLock = true;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3 (ScaleX,ScaleY,1);	
		if (XLock) 
		{
			ScaleX -= 0.06f * Time.deltaTime;
		}

		if (YLock) 
		{
			ScaleY -= 0.06f * Time.deltaTime;
		}

		if (transform.localScale.x <= 0.11f) 
		{
			XLock = false;
		}

		if (transform.localScale.y <= 0.11f) 
		{
			YLock = false;
		}
	}

	public void FrameLock()
	{
		/*ScaleX = transform.localScale.x;
		ScaleY = transform.localScale.y;*/
		ScaleX = 0.12f;
		ScaleY = 0.12f;
		XLock = true;	
		YLock = true;	
	}
}
