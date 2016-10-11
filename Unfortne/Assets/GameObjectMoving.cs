using UnityEngine;
using System.Collections;

public class GameObjectMoving : MonoBehaviour {

	public float Speed = 10;
	private Vector3 TargetPosition;


	public void SetTergetPostion(Vector3 target, float speed){
		TargetPosition = target;
		Speed = speed;
	}

	//update
	void Update(){
		if(Vector2.Distance(transform.localPosition,TargetPosition) <0.1f){
			GetComponent<Card>().SectionOver();
			Destroy(this);
		}

		//speed -=0.1f;
		transform.localPosition = Vector3.MoveTowards(transform.localPosition,TargetPosition, Speed);
	}

}
