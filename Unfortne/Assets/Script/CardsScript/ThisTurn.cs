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

	//OptionCard
	public List<Card> OptionCardList  =new List<Card>();

	//Next Turn
	public int initCards = 5;

	public void EndofTheTurn(){
		Hand.Ins.isCardsCanPlay = false;
		//option card destroy
		foreach(Card i in OptionCardList){
			if(i.Place == CardManager.cardSection.Hand){
				GameManager.Instance.Cardmanager.CardList.Remove(i);
				Hand.Ins.HandList.Remove(i);
				i.gameObject.AddComponent<StartBurn>().GetMat();
				i.transform.GetChild(0).gameObject.AddComponent<StartBurn>().GetMat();
			}

			if(i.Place == CardManager.cardSection.Table){
				GameManager.Instance.Cardmanager.CardList.Remove(i);
				Table.Ins.TableList.Remove(i);
				i.gameObject.AddComponent<StartBurn>().GetMat();
				i.transform.GetChild(0).gameObject.AddComponent<StartBurn>().GetMat();
			}
		}


		//discard
		GameManager.Instance.Cardmanager.AddMainQue(CardManager.cardSection.Discard_H);
		GameManager.Instance.Cardmanager.AddMainQue(CardManager.cardSection.Discard_T);

		//Variable setting
		Table.Ins.initTable();

		//drawing cards
		Deck.Ins.DrawCards(initCards);


		//Ending
		GameManager.Instance.Cardmanager.Endingflag = true;	
		GameManager.Instance.Cardmanager.TTurn =  gameObject.AddComponent<ThisTurn>();
		Destroy(this);
	}		


}
