using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThisTurn : MonoBehaviour {

	//Basic Variable
	public int Attack = 0;
	public string Will = "";

	//Fighting Variable
	public int TrophyConstant = 0;

	//Option Variable
	public int ActionConstant = 0;
	public int AttackConstant = 0;

	//Next Turn
	public int initCards = 5;

	public void EndofTheTurn(){
		Hand.Ins.isCardsCanPlay = false;

		//discard
		PlayerCardManager.Ins.AddMainQue(CardManager.cardSection.Discard_H);
		PlayerCardManager.Ins.AddMainQue(CardManager.cardSection.Discard_T);
		Table.Ins.TS.InitSlot();

		//Variable setting
		Table.Ins.initTable();
		FightingManager.Ins.PlayerAttack =Attack;

		//drawing cards
		Deck.Ins.DrawCards(initCards);


		//Ending
		PlayerCardManager.Ins.Endingflag = true;	
		PlayerCardManager.Ins.TTurn =  gameObject.AddComponent<ThisTurn>();

		FightingManager.Ins.TurnEnding();

		Destroy(this);
	}		


}
