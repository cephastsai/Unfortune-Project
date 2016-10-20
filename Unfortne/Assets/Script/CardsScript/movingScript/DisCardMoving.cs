using UnityEngine;
using System.Collections;
using System;

public class DisCardMoving : MonoBehaviour {
	
	public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());

	public float TargetDistance;
	public Vector3 Target;
	public bool StartM = false;
	public bool StartR = false;
	public int RotateZ = 0;
	public float PositionX;
	public float PositionY;
	public float y = 180;

	void Update () {		
		if (StartM) 
		{
			TargetDistance = Vector2.Distance (transform.localPosition, Target);
			transform.localPosition = Vector2.MoveTowards (transform.localPosition , Target , TargetDistance/5);
			if (TargetDistance <= 0.1f)
			{
				GetComponent<Card>().SectionOver();
				StartM = false;
				Destroy(this);
			}	
		}
		//Move
		if (StartR) 
		{
			if (transform.rotation.z != RotateZ) 
			{				
				transform.rotation = Quaternion.Euler (0f, 0f, (float)RotateZ);	
			}
			if (TargetDistance <= 0.1f) 
			{
				StartR = false;
			}
			//Rotate
		}
	}
	public void ReadyToDisCard_H(Vector3 TargetO)
	{
		PositionX = (float)(ran.Next (-20, 20))/100f;
		PositionY = (float)(ran.Next (-20, 20))/100f;
		Target = new Vector3(TargetO.x+PositionX,TargetO.y+PositionY,TargetO.z);
		RotateZ = ran.Next (-25, 25);
		StartM = true;
		StartR = true;
	}
}
