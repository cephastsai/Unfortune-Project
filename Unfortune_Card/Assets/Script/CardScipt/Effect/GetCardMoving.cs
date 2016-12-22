using UnityEngine;
using System.Collections;

public class GetCardMoving : MonoBehaviour {

	public float Speed = 1;
	private Vector3 TargetPosition;

	private float Scale = 0.3f;


	public void SetTergetPostion(Vector3 target, float speed){
		TargetPosition = target;
		Speed = speed;
	}

	//update
	void Update(){
		if(Vector2.Distance(transform.localPosition,TargetPosition) <0.01f){
			if(GetComponent<Card>() !=null){
				GetComponent<Card>().SectionOver();
			}
			transform.localScale = new Vector3(0.0913f, 0.0913f, 1);
			transform.localPosition  = new Vector3(0, 0, 0);
			Destroy(this);
		}

		transform.localPosition = Vector3.MoveTowards(transform.localPosition,TargetPosition, Speed);


		//Scale
		if(Scale >0.1f){
			Scale -= 0.6f*Time.deltaTime;
			transform.localScale = new Vector3(Scale, Scale, 1);
		}else{			
			transform.localScale = new Vector3(0.0913f, 0.0913f, 1);
		}

	}
}
