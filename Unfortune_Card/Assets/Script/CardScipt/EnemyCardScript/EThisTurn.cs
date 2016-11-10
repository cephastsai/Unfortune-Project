using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EThisTurn : MonoBehaviour {

	//Basic Variable
	public int Attack = 0;
	public string Will = "";

	//Fighting Variable
	public int TrophyConstant = 0;

	//Option Variable
	public int ActionConstant = 0;
	public int AttackConstant = 0;

	//OptionCard
	public List<Card> OptionCardList  =new List<Card>();

	//Next Turn
	public int initCards = 5;

	public void EndofTheTurn(){
		EHand.Ins.isCardsCanPlay = false;
		//option card destroy
		foreach(Card i in OptionCardList){
			if(i.Place == CardManager.cardSection.Hand){				
				EHand.Ins.HandList.Remove(i);
				i.gameObject.AddComponent<StartBurn>().GetMat();
				i.transform.GetChild(0).gameObject.AddComponent<StartBurn>().GetMat();
			}

			if(i.Place == CardManager.cardSection.Table){				
				ETable.Ins.TableList.Remove(i);
				i.gameObject.AddComponent<StartBurn>().GetMat();
				i.transform.GetChild(0).gameObject.AddComponent<StartBurn>().GetMat();
			}
		}


		//discard
		EnemyCardManager.Ins.AddMainQue(CardManager.cardSection.Discard_H);
		EnemyCardManager.Ins.AddMainQue(CardManager.cardSection.Discard_T);

		//Variable setting
		ETable.Ins.initTable();
		FightingManager.Ins.EnemyAttack = Attack;

		//drawing cards
		EDeck.Ins.DrawCards(initCards);


		//Ending
		EnemyCardManager.Ins.Endingflag = true;	
		EnemyCardManager.Ins.TTurn =  gameObject.AddComponent<EThisTurn>();

		FightingManager.Ins.TurnEnding();

		Destroy(this);
	}		


}
