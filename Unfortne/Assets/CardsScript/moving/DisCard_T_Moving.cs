using UnityEngine;
using System.Collections;
using System;


public class DisCard_T_Moving : MonoBehaviour {

	public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());

	public float TargetDistance;
	public Vector3 Target;
	public bool StartM = false;
	public bool StartR = false;
	public bool StartS = false;
	public float ScaleX = 1;
	public float ScaleY = 1;
	private int z = 0;
	public float y = 180;

	void Update () {
		if (StartM) 
		{
			TargetDistance = Vector2.Distance (transform.localPosition, Target);
			transform.localPosition = Vector2.MoveTowards (transform.localPosition , Target , TargetDistance/10);
			if (TargetDistance <= 0.1f)
			{
				StartM = false;
			}	
		}
		//Move
		if (StartR) 
		{
			if (transform.rotation.z != z) 
			{				
				transform.rotation = Quaternion.Euler (0f, 0f, (float)z);	
			}
			if (TargetDistance <= 0.1f) 
			{
				StartR = false;
			}
			//Rotate
		}
		if(StartS)
		{
			transform.localScale = new Vector3 (ScaleX, ScaleY,1);
			if (ScaleX <= 1) 
			{
				ScaleX += 0.3f * Time.deltaTime;
				ScaleY += 0.3f * Time.deltaTime;
			}
			if (ScaleX >= 1) 
			{
				ScaleX = 1;
				ScaleY = 1;
				StartS = false;
			}
			//Change Scale
		}
	}
	public void ReadyToDisCard_T(Vector3 TargetO)
	{
		Target = TargetO;
		z = ran.Next (-25, 25);
		StartM = true;
		StartR = true;
		StartS = true;
	}
}
