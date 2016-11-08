using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCardManager : MonoBehaviour {

	//Singleton
	private static EnemyCardManager _ins = null;

	public static EnemyCardManager Ins{

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
	public Queue<EnemyCardManager.MainSection> MainSectionQue = new Queue<EnemyCardManager.MainSection>();
	public EnemyCardManager.MainSection MainSec;
	public bool isMainSectionOver = true;
	public CardManager.cardSection MainSecShow;

	private int CardID = 0;

	//Turn
	public EThisTurn TTurn;
	public bool Endingflag = false;

	//Manager


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
						
	}

	public void SectionStart(){		
		switch(MainSec.MSection){
		case CardManager.cardSection.Drawing:		
			EDeck.Ins.Drawing(MainSec);
			break;
		case CardManager.cardSection.Discard_H:			
			EHand.Ins.Discard_H_All(MainSec);
			break;
		case CardManager.cardSection.Discard_T:			
			ETable.Ins.Discard_T_All(MainSec);
			break;
		case CardManager.cardSection.Shuffle:
			EDeadwood.Ins.Shuffling(MainSec);
			break;
		case CardManager.cardSection.HandRemove:
			EHand.Ins.HandRemove(MainSec);
			break;
		case CardManager.cardSection.EndingTurn:
			TTurn.EndofTheTurn();
			break;		
		}
	}		

	public void AddMainQue(CardManager.cardSection sec){		
		EnemyCardManager.MainSection NSection = new EnemyCardManager.MainSection(sec);
		MainSectionQue.Enqueue(NSection);
	}

	public void AddMainQue(CardManager.cardSection sec, int Cardkind){		
		EnemyCardManager.MainSection NSection = new EnemyCardManager.MainSection(sec);
		MainSectionQue.Enqueue(NSection);
		NSection.cardkind = Cardkind;
	}


	public void TurnStart(){
		//Turn Start
		TTurn =  gameObject.AddComponent<EThisTurn>();

		EHand.Ins.isCardsCanPlay = true;

		EDeck.Ins.DrawCards(5);

	}

	public void TurnEnd(){
		TTurn.EndofTheTurn();

		EHand.Ins.isCardsCanPlay = false;
	}
}
