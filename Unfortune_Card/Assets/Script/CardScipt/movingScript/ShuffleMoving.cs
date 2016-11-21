using UnityEngine;
using System.Collections;
using System;

public class ShuffleMoving : MonoBehaviour {

	public float TargetDistance;
	public Vector3 Target;
	public bool StartM = false;
	public bool StartR = false;
	public float y = 0;

	void Update () {
		if (StartM) 
		{
			TargetDistance = Vector2.Distance (transform.localPosition, Target);
			transform.localPosition = Vector2.MoveTowards (transform.localPosition , Target , TargetDistance/4);
			if (TargetDistance <= 0.1f)
			{
				GetComponent<Card>().SectionOver();
				this.transform.rotation = Quaternion.Euler (0f, 180, 0f);
				StartM = false;
				Destroy(this);
			}	
		}
		//Move
		if (StartR) 
		{ 
			this.transform.rotation = Quaternion.Euler (0f, y, 0f);
			if (y != 180) 
			{
				y -= 20;
			}
			if (y <= 0) 
			{
				y = 0;
				StartR = false;
			}

		}
		//Rotate
	}

	public void ReadyToShuffle(Vector3 TargetO)
	{
		Target = TargetO;
		StartM = true;
		StartR = true;
	}
}
