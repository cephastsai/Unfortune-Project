using UnityEngine;
using System.Collections;

public class MapMove : MonoBehaviour {

	public bool StartM = false;
	public float TargetDistance;
	public Vector3 Target;

	void Update () {
		if (StartM) 
		{
			TargetDistance = Vector3.Distance (transform.localPosition, Target);
			transform.localPosition = Vector3.MoveTowards (transform.localPosition , Target , TargetDistance/10);
			if (TargetDistance <= 0.1f)
			{
				StartM = false;
				GameManager.Instance.Mapmanager.StartOption();

			}	
		}
		//Move
	}
	public void ReadyToMove(Vector3 TargetO)
	{
		Target = TargetO;
		StartM = true;
	}
}
