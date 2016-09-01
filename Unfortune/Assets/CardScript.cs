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
	private CardManager.cardSection preTargetSection = CardManager.cardSection.nil;
	private bool isSectionOver = true;

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
		print("sectionChange:"+ isSectionOver);
		isSectionOver = true;
	}

	/// <summary>
	/// Updates
	/// </summary>
	public void Update_CardSection(){		
		

	}

	public void Update_CardTrigger(){
		print("P:"+myCard.Place+", pP:"+premyCardSection+", T:"+myCard.targetPlace+", pT:"+preTargetSection);
		//place
		if(myCard.Place != premyCardSection){			
			trigger = true;
			isSectionOver = false;

			myCardSectionShow = myCard.Place;
			premyCardSection = myCard.Place;
		}

		//target place
		if(myCard.targetPlace != preTargetSection){
			if(preTargetSection != CardManager.cardSection.nil){
				myCard.CardQue.Enqueue(preTargetSection);
			}
			//print(preTargetSection);
			switch(preTargetSection){
			case CardManager.cardSection.nil:
				if(myCard.targetPlace == CardManager.cardSection.Hand){
					myCard.CardQue.Enqueue(CardManager.cardSection.Deck);
					myCard.CardQue.Enqueue(CardManager.cardSection.Drawing);
					myCard.CardQue.Enqueue(CardManager.cardSection.Hand);
				}
				break;
			case CardManager.cardSection.Deck:				
				//check target
				if(myCard.targetPlace == CardManager.cardSection.Hand){				
					myCard.CardQue.Enqueue(CardManager.cardSection.Drawing);
					myCard.CardQue.Enqueue(CardManager.cardSection.Hand);
				}else if(myCard.targetPlace == CardManager.cardSection.Deadwood){
					myCard.CardQue.Enqueue(CardManager.cardSection.Discard_D);
					myCard.CardQue.Enqueue(CardManager.cardSection.Deadwood);
				}
				break;
			case CardManager.cardSection.Hand:
				if(myCard.targetPlace == CardManager.cardSection.Deadwood){
					myCard.CardQue.Enqueue(CardManager.cardSection.Discard_H);
					myCard.CardQue.Enqueue(CardManager.cardSection.Deadwood);
				}else if(myCard.targetPlace == CardManager.cardSection.Table){
					myCard.CardQue.Enqueue(CardManager.cardSection.Playing);
					myCard.CardQue.Enqueue(CardManager.cardSection.Table);
				}
				break;
			case CardManager.cardSection.Deadwood:
				if(myCard.targetPlace == CardManager.cardSection.Deck){
					myCard.CardQue.Enqueue(CardManager.cardSection.Shuffle);
					myCard.CardQue.Enqueue(CardManager.cardSection.Deck);
				}
				break;
			case CardManager.cardSection.Table:
				break;
			}

			myCard.CardQue.Enqueue(myCard.targetPlace);

			//set pre
			preTargetSection = myCard.targetPlace;
		}

		//Card Queue
		if(myCard.CardQue.Count >0 && isSectionOver){
			//print(myCard.CardQue.Peek());
			myCard.Place = myCard.CardQue.Dequeue();
		}

		//show
		TargetSectionShow = myCard.targetPlace;
	}

	public void Update_CardScript(){
		if(myCard.Place != myCard.targetPlace){
			switch(myCard.Place){
			case CardManager.cardSection.nil:
				SectionOver();
				break;
			case CardManager.cardSection.Deck:
				if(GameManager.Instance.cardmanager.isDeckCardReady()){
					SectionOver();
				}
				break;
			case CardManager.cardSection.Hand:
				if(trigger){				
					//gameObject.AddComponent<Browsing>().init();
					trigger = false;
				}

				if(GameManager.Instance.cardmanager.isHandCardReady()){
					SectionOver();
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
