using UnityEngine;
using System.Collections;

public class DrawCardMoving : MonoBehaviour {
	
	public float TargetDistance;
	public Vector3 Target;
	public bool StartM = false;
	public bool StartR = false;
	public float y = 180;

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
		if (StartR) 
		{ 
			this.transform.rotation = Quaternion.Euler (0f, y, 0f);
			if (y != 0) 
			{
				y -= 10;
			}
			if (y <= 0) 
			{
				y = 0;
				StartR = false;
			}

		}
		//Rotate
	}

	public void ReadyToDrawing(Vector3 TargetO)
	{
		Target = TargetO;
		StartM = true;
		StartR = true;
	}
}
