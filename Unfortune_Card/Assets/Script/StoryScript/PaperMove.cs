using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperMove : MonoBehaviour {

	public Vector3 SelfPosition;
	public Vector3 TargetPosition;

	public float TargetDistance;
	private float x;
	private float y;
	private float z ;
	private float a ;
	private float b ;
	private float XDistance;
	private float YDistance;

	private bool StartM = false;
	private bool StartR = false;

	public RectTransform recttransform;


	
	// Update is called once per frame
	void Update () {
		if (StartM) 
		{
			TargetDistance = Vector2.Distance (transform.localPosition, TargetPosition);
			transform.localPosition = new Vector3 (x, y, 0);
			if (x >= 160) 
			{
				x += (XDistance/120)+a;
			}
			if (x <= 160) 
			{
				x = 160;
			}

			if (y < 23) 
			{
				y += (YDistance/25)+b;
			}
			if (y >= 23) 
			{
				y = 23;
			}
			a -= 40*Time.deltaTime;
			b -= 3 * Time.deltaTime;
			if (TargetDistance <= 0.1f) 
			{
				StoryManager.Ins.SIManager.GetPaperInfo();
				StartM = false;
			}
		}
		//Move

		if (StartR) 
		{ 
			transform.rotation = Quaternion.Euler (0f, 0f, z);
			if (z > 0) 
			{
				z -= 20*Time.deltaTime;
			}
			if (z <0) 
			{
				z = 0;
				StartR = false;
			}

		}
		//Rotate
	}


	public void Move(float _z)
	{
		a = 0;
		b = 0;
		z = _z;
		recttransform = GetComponent<RectTransform> ();
		x = recttransform.localPosition.x;
		y = recttransform.localPosition.y;
		recttransform.localRotation = Quaternion.Euler (0f, 0f, z);
		SelfPosition = transform.localPosition;
		TargetPosition = new Vector3 (160f,23f,53);
		StartM = true;
		StartR = true;
		XDistance = transform.localPosition.x - TargetPosition.x;
		YDistance = TargetPosition.y - transform.localPosition.y;
		TargetDistance = Vector3.Distance (transform.localPosition, TargetPosition);


	}

	public void GetInfo()
	{
		Move (7.8f);
	}
}
