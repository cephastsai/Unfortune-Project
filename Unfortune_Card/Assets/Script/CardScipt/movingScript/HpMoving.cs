using UnityEngine;
using System.Collections;

public class HpMoving : MonoBehaviour {

	public float TargetDistance;
	public float speedY = 20;


	void Update () {
		TargetDistance = Vector2.Distance (transform.localPosition, Deadwood.Ins.transform.localPosition);
		transform.position = Vector2.MoveTowards (transform.position , Deadwood.Ins.transform.localPosition , 0.3f);
		if (TargetDistance <= 0.01f)
		{		
			GetComponent<Card>().SetCardSortingLayer("Deadwood");
			Destroy(this);
		}			
	}
}
