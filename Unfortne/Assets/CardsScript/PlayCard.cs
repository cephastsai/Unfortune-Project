using UnityEngine;
using System.Collections;

public class PlayCard : MonoBehaviour {

	private Card myCard;

	void Start () {
		//init
		myCard = GetComponent<Card>();
		//touch list
		GameManager.Instance.TE.TEDObjectCL += PlayingCard;
	}

	void Update () {
		//destory self
		if(myCard.Place != CardManager.cardSection.Hand){
			GameManager.Instance.TE.TEDObjectCL -= PlayingCard;
			Destroy(this);
		}
	}

	public void PlayingCard(Transform target){
		if(target == transform){
			if(Hand.Ins.isCardsCanPlay){
				if(Table.Ins.ActionNumber >0){
					//playing
					myCard.Place = CardManager.cardSection.Playing;

					//set Main section
					GameManager.Instance.Cardmanager.AddMainQue(CardManager.cardSection.Playing);
					myCard.isSectionOver = false;
					GameManager.Instance.Cardmanager.MainSec.CheckLsit.Add(myCard);

					transform.SetParent(Table.Ins.transform);
					gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(
						Table.Ins.GetTableCardposition(myCard)
				
					);

					Hand.Ins.HandList.Remove(Hand.Ins.HandList.Find(x =>x == myCard));

					//Skill
					myCard.BS.UseSkill();
					myCard.UseSkill();
				}

			}
		}			
	}		
}
