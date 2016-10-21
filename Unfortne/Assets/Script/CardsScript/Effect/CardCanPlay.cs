using UnityEngine;
using System.Collections;

public class CardCanPlay : MonoBehaviour {

	public bool isCardhit = false;
	public float CardUpHeight = 0.2f;
	public Vector3 Cardposition;

	public void init(){
		Cardposition = transform.localPosition;
		GameManager.Instance.TE.TEDObjectHit += CardHit;
	}

	public void CardHit(Transform target){		
		if(Hand.Ins.isCardsCanPlay){			
			if(Table.Ins.ActionNumber >0){
				if(target == transform){
					if(!isCardhit){
						transform.localPosition = new Vector3(Cardposition.x, Cardposition.y+CardUpHeight, Cardposition.z);
						isCardhit = true;
					}
				}else if(target == null){
					if(isCardhit){
						transform.localPosition = new Vector3(Cardposition.x, Cardposition.y, Cardposition.z);
						isCardhit = false;
					}
				}else{
					if(isCardhit){
						transform.localPosition = new Vector3(Cardposition.x, Cardposition.y, Cardposition.z);
						isCardhit = false;
					}
				}
			}
		}
	}

	void Update(){
		
		if(GetComponent<Card>().Place != CardManager.cardSection.Hand){
			GameManager.Instance.TE.TEDObjectHit -= CardHit;
			Destroy(this);
		}
	}
}
