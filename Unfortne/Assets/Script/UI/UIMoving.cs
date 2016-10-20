using UnityEngine;
using System.Collections;

public class UIMoving : MonoBehaviour {

	private Vector3 TargetPosition;

	public void SetTergetPostion(Vector3 target){
		TargetPosition = target;
	}

	//update
	void Update(){
		if(Vector2.Distance(transform.localPosition,TargetPosition) <0.1f){				
			Destroy(this);
		}

		//speed -=0.1f;
		transform.localPosition = Vector3.MoveTowards(transform.localPosition,TargetPosition, Vector2.Distance(transform.localPosition,TargetPosition)/8f);
	}
}
