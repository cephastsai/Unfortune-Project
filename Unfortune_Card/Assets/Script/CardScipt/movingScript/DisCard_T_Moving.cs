using UnityEngine;
using System.Collections;
using System;

public class DisCard_T_Moving : MonoBehaviour {

	public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());

	public Vector3 Target;
	public bool StartM = false;
	public float TargetDistance;

	void Update () {
		if (StartM) 
		{
			TargetDistance = Vector2.Distance (transform.localPosition, Target);
			transform.localPosition = Vector2.MoveTowards (transform.localPosition , Target , TargetDistance/10);
			if (TargetDistance <= 0.1f)
			{
				GetComponent<Card>().SectionOver();
				StartM = false;
				Destroy(this);
			}	
		}
	}
	public void ReadyToDisCard_T(Vector3 TargetO)
	{
		Target = TargetO;
		StartM = true;	
	}
}
