using UnityEngine;
using System.Collections;
using System;

public class DisCard_T : MonoBehaviour {

	public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());

	public float TargetDistance;
	public Transform Target;
	public bool StartM = false;
	public bool StartR = false;
	private int z = 0;
	public float y = 180;

	void Update () {
		if (StartM) 
		{
			TargetDistance = Vector2.Distance (transform.localPosition, Target.localPosition);
			transform.localPosition = Vector2.MoveTowards (transform.localPosition , Target.localPosition , TargetDistance/10);
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
	}
	public void ReadyToDisCard_T(Transform TargetO)
	{
		Target = TargetO;
		z = ran.Next (-25, 25);
		StartM = true;
		StartR = true;
	}
}
