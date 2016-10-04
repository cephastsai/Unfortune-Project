using UnityEngine;
using System.Collections;

public class ThisTurn : MonoBehaviour {

	//Basic Variable
	public int Action = 0;
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
		//discard
		GameManager.Instance.Cardmanager.AddMainQue(CardManager.cardSection.Discard_H);
		GameManager.Instance.Cardmanager.AddMainQue(CardManager.cardSection.Discard_T);

		//drawing cards
		Deck.Ins.DrawCards(initCards);


		//Ending
		GameManager.Instance.Cardmanager.TTurn =  gameObject.AddComponent<ThisTurn>();
		Destroy(this);
	}


}
