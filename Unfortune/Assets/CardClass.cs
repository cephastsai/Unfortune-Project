using UnityEngine;
using Manager;

public class Card{

	public int ID; //create card ID
	public int CardKind; //card kind
	public CardManager.cardSection Place; //card postion
	public CardManager.cardSection targetPlace; // card target position

	public Card(){}

	public Card(int cardnum, int Cardkind, CardManager.cardSection sec){
		ID = cardnum;
		CardKind = Cardkind;
		Place = sec;
	}
}
