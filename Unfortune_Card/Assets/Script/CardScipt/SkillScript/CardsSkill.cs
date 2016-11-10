using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardsSkill : MonoBehaviour {

	public class Skill{
		//Bacis Variable
		public int action = 0;
		public int attack = 0;
		public int cards = 0;
		public string CardInfo ="";

		//Card Skill
		public bool isEndingSkill = false;
		public string skillkind = "";
		public bool isRemoveSelf = false;


		public Skill(int act, int att, int car){			
			action = act;
			attack = att;
			cards = car;
		}
	}

	//Cards Skill List 
	public Dictionary<int, Skill> CardsSkillList = new Dictionary<int, Skill>();

	//EndingSkill
	public delegate void SKillD();
	public SKillD EndingSkill;
	public bool runningEndingSkill = false;

	public void init(){
		
		//testing cards
		Skill Nskill100 = new Skill(2,1,0);
		Nskill100.isEndingSkill = true;
		Nskill100.skillkind = "Hunger";
		Nskill100.CardInfo ="豔陽高照練兵完畢，card+1";	
		CardsSkillList.Add(100, Nskill100);

		Skill Nskill101 = new Skill(1,0,1);
		Nskill101.skillkind = "TurnEndingSkill";
		Nskill101.CardInfo ="此回合結束";
		CardsSkillList.Add(101, Nskill101);

		Skill Nskill102 = new Skill(0,2,2);
		Nskill102.skillkind = "";
		Nskill102.CardInfo ="\n將手牌中一張[飢餓]移出遊戲";
		CardsSkillList.Add(102, Nskill102);

		Skill Nskill103 = new Skill(1,0,1);
		Nskill103.skillkind = "HandRemove_108";
		Nskill103.CardInfo ="<b>\n行動+1\n抽1張牌</b>\n\n將手牌中一張[飢餓]移出遊戲";
		CardsSkillList.Add(103, Nskill103);

		Skill Nskill104 = new Skill(2,1,0);
		Nskill104.skillkind = "";
		Nskill104.CardInfo ="<b>\n行動+2\n攻擊+1</b>";
		CardsSkillList.Add(104, Nskill104);

		Skill Nskill105 = new Skill(1,2,0);
		Nskill105.skillkind = "";
		Nskill105.CardInfo ="<b>\n行動+1\n攻擊+2</b>";
		CardsSkillList.Add(105, Nskill105);

		Skill Nskill106 = new Skill(0,0,1);
		Nskill106.skillkind = "AddMoney";
		Nskill106.isRemoveSelf = true;
		Nskill106.CardInfo ="<b>\n抽1張牌</b>";
		CardsSkillList.Add(106, Nskill106);

		Skill Nskill107 = new Skill(2,0,1);
		Nskill107.skillkind = "";
		Nskill107.CardInfo ="<b>\n行動+2\n抽1張牌</b>";
		CardsSkillList.Add(107, Nskill107);

		Skill Nskill108 = new Skill(0,0,0);
		Nskill108.skillkind = "Hunger";
		Nskill108.CardInfo ="<color=#A6C0FFFF>飢餓像是無情的騎士斬殺著人們，\n手中的天秤傾向死亡，無法回正。</color>\n\n[此卡無任何效果]";
		CardsSkillList.Add(108, Nskill108);

		Skill Nskill109 = new Skill(1,0,1);
		Nskill109.skillkind = "HandRemove_108";
		Nskill109.CardInfo ="<b>\n行動+1\n抽1張牌</b>\n\n將手牌中一張[飢餓]移出遊戲";
		CardsSkillList.Add(109, Nskill109);

		Skill Nskill110 = new Skill(0,1,0);
		Nskill110.skillkind = "BladeofRevenge";
		Nskill110.CardInfo ="<b>\n攻擊+1</b>\n\n此回合打出X張[復仇]，\n攻擊+X";
		CardsSkillList.Add(110, Nskill110);

		Skill Nskill111 = new Skill(0,0,1);
		Nskill111.skillkind = "";
		Nskill111.CardInfo ="<b>\n抽1張牌</b>";
		CardsSkillList.Add(111, Nskill111);

		Skill Nskill112 = new Skill(0,0,0);
		Nskill112.skillkind = "";
		Nskill112.CardInfo ="<color=#A6C0FFFF>刀已出鞘，人已不在；\n不為己尊，只為復仇。</color>\n\n[此卡無任何效果]";
		CardsSkillList.Add(112, Nskill112);

		Skill Nskill1001 = new Skill(0,0,0);
		Nskill1001.skillkind = "TurnEndingSkill";
		CardsSkillList.Add(1001, Nskill1001);

		Skill Nskill1002 = new Skill(0,0,0);
		Nskill1002.skillkind = "TurnEndingSkill";
		CardsSkillList.Add(1002, Nskill1002);

		Skill Nskill1003= new Skill(0,0,0);
		Nskill1003.skillkind = "TurnEndingSkill";
		CardsSkillList.Add(1003, Nskill1003);

		Skill Nskill1004= new Skill(0,0,0);
		Nskill1004.skillkind = "TurnEndingSkill";
		CardsSkillList.Add(1004, Nskill1004);

		Skill Nskill1005= new Skill(0,0,0);
		Nskill1005.skillkind = "TurnEndingSkill";
		CardsSkillList.Add(1005, Nskill1005);

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

		if(CardsSkillList[i.CardKind].isRemoveSelf){
			Table.Ins.Removeself(i);
		}
		
		if(CardsSkillList[i.CardKind].skillkind == ""){
			i.SectionOver();
		}else{
			Invoke(CardsSkillList[i.CardKind].skillkind, 0f);
			i.SectionOver();
		}			
	}

	public void TurnEndingSkill(){		
		//PlayerCardManager.Ins.RemoveOptionCard();

		PlayerCardManager.Ins.TTurn.EndofTheTurn();

		//runningEndingSkill = true;
		/*if(EndingSkill != null){
			EndingSkill();
		}*/			
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
