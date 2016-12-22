using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCardManager : MonoBehaviour {

	//Singleton
	private static PlayerCardManager _ins = null;

	public static PlayerCardManager Ins{

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

	//Class
	public class MainSection{
		public CardManager.cardSection MSection = CardManager.cardSection.nil;
		public List<Card> CheckLsit = new List<Card>();

		//Operator
		public int cardkind;

		public MainSection(CardManager.cardSection sec){
			MSection = sec;
		}
	}

	//Card CheckList
	public Queue<PlayerCardManager.MainSection> MainSectionQue = new Queue<PlayerCardManager.MainSection>();
	public PlayerCardManager.MainSection MainSec;
	public bool isMainSectionOver = true;
	public CardManager.cardSection MainSecShow;

	//CardList
	public List<Card> CardList = new List<Card>();

	//Turn
	public ThisTurn TTurn;
	public bool Endingflag = false;
	public AttackUI AUI;

	//Manager
	public GameObject CardEndingButton;


	//stuff
	public Material[] BurnMaterial;

	public void init(){
		



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
				MainSec.MSection = CardManager.cardSection.nil;
				MainSecShow = MainSec.MSection;
			}
		}

		//Ending

		if(Endingflag){
			if(MainSectionQue.Count ==0){
				//CardEndingButton.SetActive(true);
				Endingflag = false;
			}
		}			
	}

	public void SectionStart(){		
		switch(MainSec.MSection){
		case CardManager.cardSection.Drawing:		
			Deck.Ins.Drawing(MainSec);
			break;
		case CardManager.cardSection.Discard_H:			
			Hand.Ins.Discard_H_All(MainSec);
			break;
		case CardManager.cardSection.Discard_T:			
			Table.Ins.Discard_T_All(MainSec);
			break;
		case CardManager.cardSection.Shuffle:
			Deadwood.Ins.Shuffling(MainSec);
			break;
		case CardManager.cardSection.HandRemove:
			Hand.Ins.HandRemove(MainSec);
			break;
		case CardManager.cardSection.EndingTurn:
			TTurn.EndofTheTurn();
			break;		
		}
	}

	public void PrviewCardRemove(Card target){
		CardList.Remove(target);
		Destroy(target.gameObject);
	}

	public void AddMainQue(CardManager.cardSection sec){		
		PlayerCardManager.MainSection NSection = new PlayerCardManager.MainSection(sec);
		MainSectionQue.Enqueue(NSection);
	}

	public void AddMainQue(CardManager.cardSection sec, int Cardkind){		
		PlayerCardManager.MainSection NSection = new PlayerCardManager.MainSection(sec);
		MainSectionQue.Enqueue(NSection);
		NSection.cardkind = Cardkind;
	}


	//Fighting function

	public void TurnStart(){
		//Ending Skill
		//CardManager.Ins.CardsS.isSkillEnd = true;

		TTurn =  gameObject.AddComponent<ThisTurn>();
		AUI.TT = TTurn;

		Hand.Ins.isCardsCanPlay = true;

		//temp
		//Deck.Ins.DrawCards(2);
	}

	public void TurnEnd(){
		//Ending Skill
		CardManager.Ins.CardsS.isSkillEnd = true;

		foreach(Card i in Hand.Ins.HandList){
			i.UseSkill();
		}

		foreach(Card i in Table.Ins.TableList){
			i.UseSkill();
		}

		TTurn.EndofTheTurn();

		Hand.Ins.isCardsCanPlay = false;
	}

}
