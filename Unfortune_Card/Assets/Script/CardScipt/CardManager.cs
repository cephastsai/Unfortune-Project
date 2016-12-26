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
		Deck, Hand, Deadwood, Table, Hp,
		Drawing, BacktoDeck, Playing, Shuffle, GetCard,
		Discard_H, Discard_T, Discard_D,
		HandRemove, DeckRemove,
		Select, Get,
		//MainSection
		PlayingSKill, EndingTurn
	};

	public CreateCard CCard;
	private int CardID = 0;
	public BrowsingManager BM;
	public CardsSkill CardsS;

	//Get Card
	public Queue<int> GetCardQue = new Queue<int>();
	private bool isGettingCard = false;
	public GameObject GetCardGO;
	public GameObject GetCardText;
	public GameObject GetCardButton;


	void Start(){
		//init
		CCard = GetComponent<CreateCard>();
		CCard.init();
		BM = GetComponent<BrowsingManager>();
		CardsS = GetComponent<CardsSkill>();
		CardsS.init();
		GetComponent<CardsInfoLoader>().loading();

		//temp
		CreateCard(2, true);
		CreateCard(6, true);
		CreateCard(2, true);
		CreateCard(6, true);
		CreateCard(2, true);
		CreateCard(2, true);
		CreateCard(2, true);

		/*CreateCard(2, false);
		CreateCard(4, false);
		CreateCard(5, false);*/


		//test GetCard in story
		//GetCard(3);
	}

	void Update(){
		if(GetCardQue.Count >0){
			if(!isGettingCard){
				GetCard(GetCardQue.Dequeue());
				isGettingCard = true;
			}
		}
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

	public void CreateHpCard(){
		//testingcard = GetComponent<CardsSkill>().GetCardsObject(CardKind);
		GameObject NcardObject = CCard.Createcard(10);
		NcardObject.AddComponent<Card>().init(CardID, 10, cardSection.Hp, true);
		NcardObject.AddComponent<BoxCollider>();

		//Card name
		NcardObject.name = "Card"+CardID;
		//Card ID
		CardID++;
		//Card List
		PlayerCardManager.Ins.CardList.Add(NcardObject.GetComponent<Card>());

	}

	// Get Cards in Story

	public GameObject CreateGetCard(int CardKind, cardSection Cardsec){
		GameObject NcardObject = CCard.Createcard(CardKind);
		NcardObject.AddComponent<Card>().init(CardID, CardKind, cardSection.Deadwood, true);
		NcardObject.AddComponent<BoxCollider>();

		//Card name
		NcardObject.name = "Card"+CardID;
		//Card ID
		CardID++;
		//Card List
		PlayerCardManager.Ins.CardList.Add(NcardObject.GetComponent<Card>());

		return NcardObject;
	}

	public void GetCard(int GetCardkind){		
		//Create Card
		GameObject getcard = CreateGetCard(GetCardkind, cardSection.Deadwood);
		GetCardGO = getcard;

		//Card Setting
		getcard.transform.localScale = new Vector3(0.3f, 0.3f, 1);
		getcard.transform.position = new Vector3(4.9f, 0.2f, 50);

		//Card Fade In
		getcard.AddComponent<FadeIn_Out>().StartFadeIn();
		getcard.transform.GetChild(0).gameObject.AddComponent<FadeIn_Out>().StartFadeIn();

		//Card Infomation
		//GetCardText.gameObject.SetActive(true);
		//GetCardText.transform.GetChild(0).GetComponent<Text>().text = getcard.GetComponent<Card>().Info;

		//Card Button
		//GetCardButton.gameObject.SetActive(true);
		//GameManager.Instance.TE.TEDObjectCL += GetCardButtonDown;
	}

	public void GetCardButtonDown(){
			
		//set Card to Deadwood
		Deadwood.Ins.DeadwoodList.Add(GetCardGO.GetComponent<Card>());
		GetCardGO.transform.SetParent(Deadwood.Ins.transform);
		GetCardGO.transform.localScale = new Vector3(0.0913f, 0.0913f, 1);

		//Moving
		GetCardGO.AddComponent<GetCardMoving>().SetTergetPostion(new Vector3(0,0,0), 0.2f);
				
		isGettingCard = false;

	}

}
