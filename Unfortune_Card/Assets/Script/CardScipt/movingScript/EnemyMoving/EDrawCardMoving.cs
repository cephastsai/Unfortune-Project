using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDrawCardMoving : MonoBehaviour {

	public float TargetDistance;
	public Vector3 Target;
	public bool StartM = false;

	void Update () {
		if (StartM) 
		{
			TargetDistance = Vector2.Distance (transform.localPosition, Target);
			transform.localPosition = Vector2.MoveTowards (transform.localPosition , Target , TargetDistance/4);
			if (TargetDistance <= 0.1f)
			{				
				GetComponent<Card>().SectionOver();
				StartM = false;
				Destroy(this);
			}	
		}
		//Move	
	}

	public void ReadyToDrawing(Vector3 TargetO)
	{
		Target = TargetO;
		StartM = true;	
	}
}
