using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardsSkill : MonoBehaviour {

	public class Skill{		
		//Card Skill
		public string skillkind = "";
		public bool HaveEndingSkill = false;
	}

	//Cards Skill List 
	public Dictionary<int, Skill> CardsSkillList = new Dictionary<int, Skill>();

	//EndingSkill
	public bool isSkillEnd = false;

	public void init(){		

	}

	/*void Update(){
		if(runningEndingSkill){
			if(EndingSkill ==null){
				PlayerCardManager.Ins.TTurn.EndofTheTurn();

				runningEndingSkill = false;
			}
		}
	}*/


	public void UseCardSkill(Card i){
		
		if(CardsSkillList[i.CardKind].skillkind == ""){
			i.SectionOver();
		}else{
			//gameObject.SendMessage(CardsSkillList[i.CardKind].skillkind);

			Invoke(CardsSkillList[i.CardKind].skillkind, 0f);
			i.SectionOver();
		}			
	}			


	//Cards Skill Function

	private void PlusInitCard(){
		PlayerCardManager.Ins.TTurn.initCards +=1;
	}

	private void HandRemove_108(){
		PlayerCardManager.Ins.AddMainQue(CardManager.cardSection.HandRemove,108);
	}

	private void Hunger(){
		/*print("12");
		if(!runningEndingSkill){
			print("123");
			EndingSkill += Hunger;
		}
		PlayerCardManager.Ins.AddMainQue(CardManager.cardSection.HandRemove,100);
		EndingSkill -= Hunger;*/
	}

	public void AddMoney(){
		GameManager.Instance.Money +=10;
	}

	public void BladeofRevenge(){
		int Rnum = 0;
		foreach(Card i in Table.Ins.TableList){
			if(i.CardKind == 112){
				Rnum++;
			}
		}
		PlayerCardManager.Ins.TTurn.Attack +=Rnum;
	}
}
