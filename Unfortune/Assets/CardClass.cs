using UnityEngine;
using System.Collections.Generic;
using Manager;

public class Card{

	public int ID; //create card ID
	public int CardKind; //card kind
	public CardManager.cardSection Place; //card postion
	public CardManager.cardSection targetPlace; // card target position

	//Card Section Queue
	public Queue<CardManager.cardSection> CardQue = new Queue<CardManager.cardSection>();

	public Card(){}

	public Card(int cardnum, int Cardkind, CardManager.cardSection sec){
		ID = cardnum;
		CardKind = Cardkind;
		Place = sec;
	}
	/*
	public void AddCardQue(CardManager.cardSection targetpos){
		//check pos Section
		switch(Place){
		case CardManager.cardSection.Deck:
			//check target
			if(targetpos == CardManager.cardSection.Hand){				
				CardQue.Enqueue(CardManager.cardSection.Drawing);
				CardQue.Enqueue(CardManager.cardSection.Hand);
			}else if(targetpos == CardManager.cardSection.Deadwood){
				CardQue.Enqueue(CardManager.cardSection.Discard_D);
				CardQue.Enqueue(CardManager.cardSection.Deadwood);
			}
			break;
		case CardManager.cardSection.Hand:
			if(targetpos == CardManager.cardSection.Deadwood){
				CardQue.Enqueue(CardManager.cardSection.Discard_H);
				CardQue.Enqueue(CardManager.cardSection.Deadwood);
			}else if(targetpos == CardManager.cardSection.Table){
				CardQue.Enqueue(CardManager.cardSection.Playing);
				CardQue.Enqueue(CardManager.cardSection.Table);
			}
			break;
		case CardManager.cardSection.Deadwood:
			if(targetpos == CardManager.cardSection.Deck){
				CardQue.Enqueue(CardManager.cardSection.Shuffle);
				CardQue.Enqueue(CardManager.cardSection.Deck);
			}
			break;
		case CardManager.cardSection.Table:
			break;
		}
	}
	*/
}
