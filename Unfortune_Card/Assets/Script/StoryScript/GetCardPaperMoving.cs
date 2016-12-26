using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCardPaperMoving : MonoBehaviour {

	public float Speed = 40;
	private Vector3 TargetPosition = new Vector3(164, 20, 0);

	//update
	void Update(){
		if(Vector2.Distance(transform.localPosition,TargetPosition) <0.01f){
			StoryManager.Ins.SIManager.GetCardinfo();

			Destroy(this);
		}

		//speed -=0.1f;
		transform.localPosition = Vector2.MoveTowards(transform.localPosition,TargetPosition, Speed);
	}
}
