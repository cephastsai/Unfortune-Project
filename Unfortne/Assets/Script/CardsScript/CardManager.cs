using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardManager : MonoBehaviour {

	//enum
	public enum cardSection{
		nil, 
		Deck, Hand, Deadwood, Table,
		Drawing, BacktoDeck, Playing, Shuffle,
		Discard_H, Discard_T, Discard_D,
		HandRemove, DeckRemove,
		Select,
		//MainSection
		PlayingSKill, EndingTurn
	};

	//Class
	public class MainSection{
		public cardSection MSection = cardSection.nil;
		public List<Card> CheckLsit = new List<Card>();

		//Operator
		public int cardkind;

		public MainSection(cardSection sec){
			MSection = sec;
		}
	}

	//Card CheckList
	public Queue<CardManager.MainSection> MainSectionQue = new Queue<CardManager.MainSection>();
	public CardManager.MainSection MainSec;
	public bool isMainSectionOver = true;
	public cardSection MainSecShow;

	//testing

	//CardList
	public List<Card> CardList = new List<Card>();
	private int CardID = 0;
	public CardsSkill CardsS;

	//Cards Object List
	public List<GameObject> CardsobjectList = new List<GameObject>();

	//Turn
	public ThisTurn TTurn;
	public bool Endingflag = false;

	//Manager
	public BrowsingManager BM;
	public Select select;

	//stuff
	public Text Status;
	public Material[] BurnMaterial;

	//Card Place
	//public GameObject Deck;
	//public GameObject Hand;

	void Start(){		
		//init
		CardsS = GetComponent<CardsSkill>();
		CardsS.init();
		BM = GetComponent<BrowsingManager>();
		select = GameObject.Find("Select").GetComponent<Select>();

		//set position
		Deck.Ins.transform.localPosition = Deck.Ins.DeckposE.position;
		Hand.Ins.transform.localPosition = Hand.Ins.HandposE.position;
		Deadwood.Ins.transform.localPosition = Deadwood.Ins.DeadwoodposE.position;


		for(int i=0; i<4;i++){
			CreateCard(100);
			CreateCard(101);
			CreateCard(102);
		}

		//Turn Start
		TTurn =  gameObject.AddComponent<ThisTurn>();
		Deck.Ins.DrawCards(5);
	}

	void Update(){
		//Change MainSection
		if(isMainSectionOver && MainSectionQue.Count >0){
			//print(MainSectionQue.Peek().MSection);
			MainSec = MainSectionQue.Peek();
			MainSecShow = MainSec.MSection;
			SectionStart();
			isMainSectionOver = false;
		}

		if(!isMainSectionOver){	
			bool tempflag = true;
			foreach(Card i in MainSec.CheckLsit){
				if(!i.isSectionOver){					
					tempflag = false;
				}
			}

			if(tempflag || MainSec.CheckLsit.Count ==0){
				if(MainSectionQue.Count != 0){
					MainSectionQue.Dequeue();
				}							
				isMainSectionOver = true;
				MainSec.MSection = cardSection.nil;
				MainSecShow = MainSec.MSection;
			}
		}

		//Ending

		if(Endingflag){
			if(MainSectionQue.Count ==0){
				GameManager.Instance.UImanager.FightToMap();
				CardUIExit();
				GameManager.Instance.SetGameSection(GameManager.GameSection.Map);
				Endingflag = false;
			}
		}
	}

	public void SectionStart(){
		switch(MainSec.MSection){
		case cardSection.Drawing:		
			Deck.Ins.Drawing(MainSec);
			break;
		case cardSection.Discard_H:			
			Hand.Ins.Discard_H_All(MainSec);
			break;
		case cardSection.Discard_T:			
			Table.Ins.Discard_T_All(MainSec);
			break;
		case cardSection.Shuffle:
			Deadwood.Ins.Shuffling(MainSec);
			break;
		case cardSection.HandRemove:
			Hand.Ins.HandRemove(MainSec);
			break;
		case cardSection.EndingTurn:
			TTurn.EndofTheTurn();
			break;
		}
	}

	//Create Card
	public void CreateCard(int CardKind){
		//testingcard = GetComponent<CardsSkill>().GetCardsObject(CardKind);
		GameObject NcardObject = Instantiate(GetCardsObject(CardKind));
		NcardObject.AddComponent<Card>().init(CardID, CardKind, cardSection.Deck);

		//Card name
		NcardObject.name = "Card"+CardID;
		//Card ID
		CardID++;
		//Card List
		CardList.Add(NcardObject.GetComponent<Card>());

	}

	public void CreateCPreviewCard(int CardKind){
		GameObject NcardObject = Instantiate(GetCardsObject(CardKind));
		NcardObject.AddComponent<Card>().init(CardID, CardKind, cardSection.Select);

		//Card name
		NcardObject.name = "Card"+CardID;
		//Card ID
		CardID++;
		//Card List
		CardList.Add(NcardObject.GetComponent<Card>());

	}

	public void PrviewCardRemove(Card target){
		CardList.Remove(target);
		Destroy(target.gameObject);
	}

	public void AddMainQue(cardSection sec){
		CardManager.MainSection NSection = new CardManager.MainSection(sec);
		MainSectionQue.Enqueue(NSection);
	}

	public void AddMainQue(cardSection sec, int Cardkind){
		CardManager.MainSection NSection = new CardManager.MainSection(sec);
		MainSectionQue.Enqueue(NSection);
		NSection.cardkind = Cardkind;
	}

	public GameObject GetCardsObject(int Cardkind){
		foreach(GameObject i in CardsobjectList){
			if(i.name == Cardkind.ToString()){
				return i;
			}
		}
		return null;
	}

	public void CardUIIN(){
		Deck.Ins.gameObject.AddComponent<UIMoving>().SetTergetPostion(Deck.Ins.Deckpos.position);
		Hand.Ins.gameObject.AddComponent<UIMoving>().SetTergetPostion(Hand.Ins.Handpos.position);
		Deadwood.Ins.gameObject.AddComponent<UIMoving>().SetTergetPostion(Deadwood.Ins.Deadwoodpos.position);
	}

	public void CardUIExit(){
		Deck.Ins.gameObject.AddComponent<UIMoving>().SetTergetPostion(Deck.Ins.DeckposE.position);
		Hand.Ins.gameObject.AddComponent<UIMoving>().SetTergetPostion(Hand.Ins.HandposE.position);
		Deadwood.Ins.gameObject.AddComponent<UIMoving>().SetTergetPostion(Deadwood.Ins.DeadwoodposE.position);
	}

}
