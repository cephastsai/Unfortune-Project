using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Manager;

public class CardScript : MonoBehaviour {

	public GameObject cardObject;
	public Card myCard;

	//Update
	private bool trigger = true;
	public  CardManager.cardSection myCardSectionShow;
	private CardManager.cardSection premyCardSection = CardManager.cardSection.nil;
	public CardManager.cardSection TargetSectionShow;
	private bool isSectionOver = false;

	//Card Manager
	public CardManager CM = GameManager.Instance.cardmanager;

	//initialize CardScript
	public void init(Card Ncard, GameObject NcardGameObject){
		//init card variable setting
		myCard = Ncard;
		cardObject = NcardGameObject;
		GameManager.Instance.UpdateList +=Update_CardTrigger;
		GameManager.Instance.UpdateList +=Update_CardScript;
		GameManager.Instance.UpdateList +=Update_CardSection;

		//card gameObject setting
			//name
		gameObject.name = "Card"+myCard.ID.ToString();
			//is Card Up
		CardsUp(false);
			//Position
		if(myCard.Place == CardManager.cardSection.Deck){
			gameObject.transform.position = CM.DUI.GetDeckCardPosition();
		}else if(myCard.Place == CardManager.cardSection.Deadwood){
			gameObject.transform.position = CM.DUI.GetDeckCardPosition();
		}			
	}
		

	public void CardsUp(bool isCardUp){//isCardUp -> true:Up,false:Back
		if(isCardUp && transform.localRotation.y == 180){
			transform.localRotation = Quaternion.Euler(0,-180,0);
		}else if(!isCardUp && transform.localRotation.y == 0){
			transform.localRotation = Quaternion.Euler(0,180,0);
		}
	}

	//section
	public void SectionOver(){
		isSectionOver = true;
	}

	/// <summary>
	/// Updates
	/// </summary>
	public void Update_CardSection(){		
		
		if(myCard.CardQue.Count >0){
			if(!isSectionOver){
				myCard.targetPlace = myCard.CardQue.Dequeue();
			}	
			
		}

	}

	public void Update_CardTrigger(){
		if(myCard.Place != premyCardSection){
			trigger = true;
			isSectionOver = false;

			myCardSectionShow = myCard.Place;
			premyCardSection = myCard.Place;
		}
		TargetSectionShow = myCard.targetPlace;
	}

	public void Update_CardScript(){
		if(myCard.Place != myCard.targetPlace){
			switch(myCard.Place){
			case CardManager.cardSection.nil:			
				break;
			case CardManager.cardSection.Deck:
				if(GameManager.Instance.cardmanager.isDeckCardReady()){
					myCard.Place = CardManager.cardSection.Drawing;
				}
				break;
			case CardManager.cardSection.Hand:
				if(trigger){				
					//gameObject.AddComponent<Browsing>().init();
					trigger = false;
				}

				if(GameManager.Instance.cardmanager.isHandCardReady()){
					if(myCard.targetPlace == CardManager.cardSection.Deadwood){
						myCard.Place = CardManager.cardSection.Discard_H;
					}else if(myCard.targetPlace == CardManager.cardSection.Table){

					}
				}

				break;
			case CardManager.cardSection.Deadwood:
				//if(GameManager.Instance.cardmanager.isDeadwoodCardReady()){
					myCard.Place = CardManager.cardSection.Shuffle;
				//}
				break;
			case CardManager.cardSection.Table:
				break;
			case CardManager.cardSection.Drawing:
				if(trigger){
					//Add Component
					gameObject.AddComponent<CardMoving>().GetInf(
						CardManager.cardSection.Drawing, 
						GameManager.Instance.cardmanager.HUI.GetHandCardPosition()
					);
					trigger = false;
				}

				if(isSectionOver){
					if(GetComponent<CardMoving>() != null){
						Destroy(GetComponent<CardMoving>());
					}else{
						myCard.Place = CardManager.cardSection.Hand;
					}

				}
				break;
			case CardManager.cardSection.BacktoDeck:
				break;
			case CardManager.cardSection.Playing:
				break;
			case CardManager.cardSection.Shuffle:
				if(trigger){
					gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(GameManager.Instance.cardmanager.DeckPosition.localPosition);
					trigger = false;
				}

				if(isSectionOver){				
					if(GetComponent<GameObjectMoving>() != null){
						Destroy(GetComponent<GameObjectMoving>());
					}else{
						myCard.Place = CardManager.cardSection.Deck;
					}

				}

				break;
			case CardManager.cardSection.Discard_H:			
				if(trigger){
					gameObject.AddComponent<CardMoving>().GetInf(
						CardManager.cardSection.Discard_H,
						GameManager.Instance.cardmanager.DeadwoodPosition.position
					);
					trigger = false;
				}		

				if(isSectionOver){				
					if(GetComponent<CardMoving>() != null){
						Destroy(GetComponent<CardMoving>());	
					}else{
						myCard.Place = CardManager.cardSection.Deadwood;
					}					
				}
				break;
			case CardManager.cardSection.Discard_T:
				break;
			case CardManager.cardSection.Discard_D:
				break;
			case CardManager.cardSection.Remove:
				//Remove UpdateList 
				GameManager.Instance.UpdateList -=Update_CardTrigger;
				GameManager.Instance.UpdateList -=Update_CardTrigger;

				//Destroy this

				break;
			}
		}

	}




}
