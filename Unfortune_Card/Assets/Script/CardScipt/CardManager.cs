using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardManager : MonoBehaviour {

	//Singleton
	private static CardManager _ins = null;

	public static CardManager Ins{

		get{
			return _ins;
		}
	}//Instance

	void Awake(){
		if(_ins == null){
			_ins = this;

			DontDestroyOnLoad(gameObject);
		}else if(_ins != this){
			Destroy(gameObject);
		}
	}

	//enum
	public enum cardSection{
		nil, 
		Deck, Hand, Deadwood, Table,
		Drawing, BacktoDeck, Playing, Shuffle, GetCard,
		Discard_H, Discard_T, Discard_D,
		HandRemove, DeckRemove,
		Select, Get,
		//MainSection
		PlayingSKill, EndingTurn
	};

	public CreateCard CCard;
	private int CardID = 0;

	//Card Place
	//public GameObject Deck;
	//public GameObject Hand;

	void Start(){
		CCard = GetComponent<CreateCard>();
		CCard.init();


		//temp
		CreateCard(2, true);
		CreateCard(2, true);
		CreateCard(2, true);
		CreateCard(0, true);

		CreateCard(2, false);
		CreateCard(4, false);
		CreateCard(5, false);
		CreateCard(6, false);
		CreateCard(4, false);
		CreateCard(7, false);
		CreateCard(3, false);
		CreateCard(8, false);
				
	}

	
	//Create Card
	public void CreateCard(int CardKind, bool isPlayerCard){
		//testingcard = GetComponent<CardsSkill>().GetCardsObject(CardKind);
		GameObject NcardObject = CCard.Createcard(CardKind);
		NcardObject.AddComponent<Card>().init(CardID, CardKind, cardSection.Deck, isPlayerCard);
		NcardObject.AddComponent<BoxCollider>();

		//Card name
		NcardObject.name = "Card"+CardID;
		//Card ID
		CardID++;
		//Card List
		PlayerCardManager.Ins.CardList.Add(NcardObject.GetComponent<Card>());

	}



}
