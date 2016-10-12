using UnityEngine;
using System.Collections;

public class testingButton : MonoBehaviour {

	public void DrwaingButton(){
		if(Deck.Ins.DeckList.Count +Deadwood.Ins.DeadwoodList.Count !=0){
			GameManager.Instance.Cardmanager.AddMainQue(CardManager.cardSection.Drawing);
		}
	}

	public void Discard_H(){
		GameManager.Instance.Cardmanager.AddMainQue(CardManager.cardSection.Discard_H);
	}

	public void Discard_T(){
		GameManager.Instance.Cardmanager.AddMainQue(CardManager.cardSection.Discard_T);
	}

	public void EndofTheTurn(){
		GameManager.Instance.Cardmanager.TTurn.EndofTheTurn();
	}
}
