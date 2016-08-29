using UnityEngine;
using System.Collections;
using Manager;

public class GameObjectMoving : MonoBehaviour {

	public float speed = 10;
	private Vector3 TargetPosition;


	public void SetTergetPostion(Vector3 target){
		TargetPosition = target;
		GameManager.Instance.UpdateList += Update_moving;
	}

	//update
	public void Update_moving(){
		if(Vector2.Distance(transform.localPosition,TargetPosition) <0.1f){
			GetComponent<CardScript>().SectionOver();
			GameManager.Instance.UpdateList -= Update_moving;
		}

		//speed +=0.1f;
		transform.localPosition = Vector3.MoveTowards(transform.localPosition,TargetPosition, speed);
	}

}
