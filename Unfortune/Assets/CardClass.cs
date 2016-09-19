using UnityEngine;
using System.Collections.Generic;
using Manager;

public class Card{

	public int ID; //create card ID
	public int CardKind; //card kind
	public CardManager.cardSection Place; //card postion
	public CardManager.cardSection targetPlace; // card target position
	public bool isSectionOver;

	//Card Section Queue
	public Queue<CardManager.cardSection> CardQue = new Queue<CardManager.cardSection>();

	//Card Skill
	public int action = 1;
	public int attack = 1;
	public int cards = 1;

	public Card(){}

	public Card(int cardnum, int Cardkind, CardManager.cardSection sec){
		ID = cardnum;
		CardKind = Cardkind;
		Place = sec;
		isSectionOver = true;
	}

}
