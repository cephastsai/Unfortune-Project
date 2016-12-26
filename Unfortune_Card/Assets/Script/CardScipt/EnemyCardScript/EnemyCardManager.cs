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
	public EAttackUI AUI;

	//Manager
	public ETableSlot ETS;

	//AI Section
	public bool isEnemyAIOn = false;

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
		if(GetComponent<EThisTurn>() == null){
			TTurn =  gameObject.AddComponent<EThisTurn>();
		}
		AUI.TT = TTurn;

		EHand.Ins.isCardsCanPlay = true;

		isEnemyAIOn = true;

	}

	public void TurnEnd(){
		print("EnemyManager");
		//Ending Skill
		CardManager.Ins.CardsS.isSkillEnd = true;

		foreach(Card i in EHand.Ins.HandList){
			i.UseSkill();
		}

		foreach(Card i in ETable.Ins.TableList){
			i.UseSkill();
		}


		TTurn.EndofTheTurn();

		EHand.Ins.isCardsCanPlay = false;

		isEnemyAIOn = false;
	}

	public void SetEnemyCards(List<int> EnemyCardsList){

		DestroyGameObjectChild(EDeck.Ins.gameObject);
		DestroyGameObjectChild(EHand.Ins.gameObject);
		DestroyGameObjectChild(EDeadwood.Ins.gameObject);
		DestroyGameObjectChild(ETable.Ins.gameObject);

		foreach(int id in EnemyCardsList){
			CardManager.Ins.CreateCard(id, false);
		}
	}

	public void DestroyEnemyCards(){
		EDeck.Ins.DeckList.Clear();
		DestroyGameObjectChild(EDeck.Ins.gameObject);
		EHand.Ins.HandList.Clear();
		DestroyGameObjectChild(EHand.Ins.gameObject);
		EDeadwood.Ins.DeadwoodList.Clear();
		DestroyGameObjectChild(EDeadwood.Ins.gameObject);
		ETable.Ins.TableList.Clear();
		DestroyGameObjectChild(ETable.Ins.gameObject);
	}

	void DestroyGameObjectChild(GameObject target){
		if(target.transform.childCount >0){
			for(int i =0; i<target.transform.childCount; i++){
				Destroy(target.transform.GetChild(i).gameObject);
			}
		}
	}
}
