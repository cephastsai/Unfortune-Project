using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetCards : MonoBehaviour {

	//List
	public List<Card> GetCardsList = new List<Card>();

	//Timer
	private Queue<int> GetCardsIn = new Queue<int>();
	private float Timer = 0;
	private float preTime = 0.5f;
	private bool QueStart = false;

	//Variable
	public float constantX = 2f;
	public GameObject NextButton;

	public void init(List<int> CardsID){
		foreach(int i in CardsID){
			GetCardsIn.Enqueue(i);
		}
	}


	void Update(){

		//Timer
		if(GetCardsIn.Count >0){
			if(!QueStart){
				Timer = Time.time;
				QueStart =true;
			}
		}

		if(QueStart){
			if(Time.time - Timer >preTime){
				//create Cards
				GameManager.Instance.Cardmanager.CreateGetCard(GetCardsIn.Dequeue());
				Timer = Time.time;
			}

			if(GetCardsIn.Count ==0){
				QueStart = false;

				//button
				NextButton.SetActive(true);
			}
		}			

	}

	public void SetgetCard(Card target){
		//add list
		GetCardsList.Add(target);

		//set transform
		target.transform.SetParent(transform);
		target.transform.localPosition = new Vector3(constantX*(GetCardsList.Count-1), 0f, 0f);

		//Fade
		target.gameObject.AddComponent<FadeIn_Out>().StartFadeIn();
		target.transform.GetChild(0).gameObject.AddComponent<FadeIn_Out>().StartFadeIn();

		//browsing
		target.gameObject.AddComponent<Browsing>();
	}


	public void NextButtonClick(){
		//UI
		NextButton.SetActive(false);
		GameManager.Instance.Cardmanager.CardUIIN();
		GameManager.Instance.UImanager.ChoseToFight();

		foreach(Card i in GetCardsList){
			if(i.isOptionCard){
				GameManager.Instance.Cardmanager.TTurn.OptionCardList.Add(i);
			}
		}			

		//cards
		/*foreach(Card i in GetCardsList){
			//goto deadwood
			GameManager.Instance.Cardmanager.AddMainQue(CardManager.cardSection.GetCard);
		}*/
		GameManager.Instance.Cardmanager.AddMainQue(CardManager.cardSection.GetCard);

		//variable setting
		Hand.Ins.isCardsCanPlay = true;
		if(Hand.Ins.HandList.Count ==0){
			Deck.Ins.DrawCards(5);
		}

	}

	public void GetCard(CardManager.MainSection Tsec){		
		foreach(Card i in GetCardsList){			
			i.isSectionOver = false;
			Tsec.CheckLsit.Add(i);
			i.Place = CardManager.cardSection.GetCard;
			i.GetCard();
		}
		GetCardsList.Clear();
	}
}
