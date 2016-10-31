using UnityEngine;
using System.Collections;

public class PlayingMoving : MonoBehaviour {

	public float TargetDistance;
	public Vector3 Target;
	public bool StartM = false;

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

	}

	public void ReadyToPlay(Vector3 TargetO)
	{
		Target = TargetO;
		StartM = true;		
	}
}
