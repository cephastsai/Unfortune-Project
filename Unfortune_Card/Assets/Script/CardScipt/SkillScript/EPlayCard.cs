using UnityEngine;
using System.Collections;

public class EPlayCard : MonoBehaviour {

	private Card myCard;

	void Start () {
		//init
		myCard = GetComponent<Card>();
	}
	
	// Update is called once per frame
	void Update () {
		if(myCard.Place != CardManager.cardSection.Hand){			
			Destroy(this);
		}
	}

	public void PlayingCard(){
		
		if(EHand.Ins.isCardsCanPlay){
			if(ETable.Ins.ActionNumber >0){
				//playing
				myCard.Place = CardManager.cardSection.Playing;

				//set Main section
				EnemyCardManager.Ins.AddMainQue(CardManager.cardSection.Playing);
				myCard.isSectionOver = false;
				EnemyCardManager.Ins.MainSec.CheckLsit.Add(myCard);

				transform.SetParent(ETable.Ins.transform);
				gameObject.AddComponent<PlayingMoving>().ReadyToPlay(
					ETable.Ins.GetTableCardposition(myCard)	
				);

				EHand.Ins.HandList.Remove(EHand.Ins.HandList.Find(x =>x == myCard));

				//Skill
				myCard.UseSkill();

				if(myCard.BS != null){
					myCard.BS.UseSkill();
				}


			}

		}
					
	}	
}
