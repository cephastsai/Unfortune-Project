using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardsSkill : MonoBehaviour {

	public class Skill{
		//Bacis Variable
		public int action = 0;
		public int attack = 0;
		public int cards = 0;

		//Card Skill
		public bool isEndingSkill = false;
		public string skillkind = "";


		public Skill(int act, int att, int car){
			action = act;
			attack = act;
			cards = car;
		}
	}

	//Cards Skill List 
	public Dictionary<int, Skill> CardsSkillList = new Dictionary<int, Skill>();

	//EndingSkill
	public delegate void SKillD();
	public SKillD EndingSkill;

	public void init(){
		
		//testing cards
		Skill Nskill100 = new Skill(2,1,0);
		Nskill100.isEndingSkill = true;
		Nskill100.skillkind = "Hunger";
		CardsSkillList.Add(100, Nskill100);

		Skill Nskill101 = new Skill(1,0,1);
		Nskill101.skillkind = "PlusInitCard";
		CardsSkillList.Add(101, Nskill101);

		Skill Nskill102 = new Skill(0,2,2);
		Nskill102.skillkind = "HandRemove_100";
		CardsSkillList.Add(102, Nskill102);

	}


	public void UseCardSkill(Card i){
		
		if(CardsSkillList[i.CardKind].skillkind == ""){
			i.SectionOver();
		}else{
			Invoke(CardsSkillList[i.CardKind].skillkind, 0f);
			i.SectionOver();
		}			
	}

	public void TurnEndingSkill(){
		
	}





	//Cards Skill Function

	private void PlusInitCard(){
		GameManager.Instance.Cardmanager.TTurn.initCards +=1;
	}

	private void HandRemove_100(){
		GameManager.Instance.Cardmanager.AddMainQue(CardManager.cardSection.HandRemove,100);
	}

	private void Hunger(){
		
	}
}
