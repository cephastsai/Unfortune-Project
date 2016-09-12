using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Manager;

public class CardScript : MonoBehaviour {

	public GameObject cardObject;
	public Card myCard;

	//Card Manager
	public CardManager CM = GameManager.Instance.cardmanager;

	//Update
	private bool trigger = true;
	public  CardManager.cardSection myCardSectionShow = CardManager.cardSection.Deck;
	private CardManager.cardSection premyCardSection = CardManager.cardSection.Deck;



	public void init(Card Ncard, GameObject NcardGameObject){
		//init card variable setting
		myCard = Ncard;
		cardObject = NcardGameObject;
		GameManager.Instance.UpdateList +=Update_CardScript;
		GameManager.Instance.UpdateList +=Update_CardTrigger;

		//card gameObject setting
		//name
		gameObject.name = "Card"+myCard.ID.ToString();
		//card is up
		CardsUp(false);
		//position
		if(myCard.Place == CardManager.cardSection.Deck){
			gameObject.transform.position = CM.DUI.GetDeckCardPosition();
		}
	}

	public void SectionOver(){
		print("sectionChange:"+ myCard.isSectionOver);
		myCard.isSectionOver = true;
	}

	public void CardsUp(bool isCardUp){//isCardUp -> true:Up,false:Back
		if(isCardUp && transform.localRotation.y == 180){
			transform.localRotation = Quaternion.Euler(0,-180,0);
		}else if(!isCardUp && transform.localRotation.y == 0){
			transform.localRotation = Quaternion.Euler(0,180,0);
		}
	}

	//Update
	public void Update_CardTrigger(){
		//if section change
		if(myCard.Place != premyCardSection){			
			trigger = true;
			myCard.isSectionOver = false;

			myCardSectionShow = myCard.Place;
			premyCardSection = myCard.Place;
		}
	}

	public void Update_CardScript(){

		switch(myCard.Place){
		case CardManager.cardSection.Hand:
			SectionOver();
			break;
		case CardManager.cardSection.Deadwood:
			SectionOver();
			break;
		}

		if(CM.MainSection == myCard.Place){
			switch(myCard.Place){
			case CardManager.cardSection.Drawing:
				if(trigger){					
					//Add Component
					gameObject.AddComponent<CardMoving>().GetInf(
						CardManager.cardSection.Drawing, 
						GameManager.Instance.cardmanager.HUI.GetHandCardPosition(myCard)
					);
					trigger = false;
				}
				print("sec:"+myCard.isSectionOver);
				if(myCard.isSectionOver){			
					if(GetComponent<CardMoving>() != null){						
						Destroy(GetComponent<CardMoving>());
					}else{						
						myCard.Place = CardManager.cardSection.Hand;					
					}

				}
				break;			
			case CardManager.cardSection.Discard_H:			
				if(trigger){
					print("intoOver");
					gameObject.AddComponent<CardMoving>().GetInf(
						CardManager.cardSection.Discard_H,
						GameManager.Instance.cardmanager.DeadwoodPosition.position
					);
					trigger = false;
				}		

				if(myCard.isSectionOver){					
					if(GetComponent<CardMoving>() != null){
						Destroy(GetComponent<CardMoving>());	
					}else{
						myCard.Place = CardManager.cardSection.Deadwood;
					}					
				}
				break;
			}
		}
	}


}
