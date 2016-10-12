using UnityEngine;
using System.Collections;

public class PlayingMoving : MonoBehaviour {

	public float TargetDistance;
	public Vector3 Target;
	public bool StartM = false;
	public bool StartS = false;
	public float ScaleX = 1;
	public float ScaleY = 1;

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
		if(StartS)
		{
			transform.localScale = new Vector3 (ScaleX, ScaleY,1);
			if (ScaleX >= 0.7f) 
			{
				ScaleX -= 0.3f * Time.deltaTime;
				ScaleY -= 0.3f * Time.deltaTime;
			}
			if (ScaleX <= 0.7f) 
			{
				ScaleX = 0.7f;
				ScaleY = 0.7f;
				StartS = false;
			}
			//Change Scale
		}
	}
	public void ReadyToPlay(Vector3 TargetO)
	{
		Target = TargetO;
		StartM = true;
		StartS = true;
	}
}
