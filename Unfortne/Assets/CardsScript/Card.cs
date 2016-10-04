﻿using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	public int ID; //create card ID
	public int CardKind; //card kind
	public CardManager.cardSection Place; //card postion
	public bool isSectionOver;

	//Card Skill
	public int action = 0;
	public int attack = 0;
	public int cards = 0;
	public BacisSkill BS;
	public Component SC;

	public void init(int cardnum, int Cardkind, CardManager.cardSection sec){
		//Bacis Variable
		ID = cardnum;
		CardKind = Cardkind;
		Place = sec;

		//CardsSkill
		CardsSkill.Skill tempskill = GameManager.Instance.Cardmanager.CardsS.CardsSkillList[CardKind];
		action = tempskill.action;
		attack = tempskill.attack;
		cards = tempskill.cards;
		SkillSetting();


		isSectionOver = true;
	}		

	public void CardsUp(bool isCardUp){//isCardUp -> true:Up,false:Back
		if(isCardUp && transform.localRotation.y == 180){
			transform.localRotation = Quaternion.Euler(0,-180,0);
		}else if(!isCardUp && transform.localRotation.y == 0){
			transform.localRotation = Quaternion.Euler(0,180,0);
		}
	}

	public void SectionOver(){
		isSectionOver = true;

		switch(Place){
		case CardManager.cardSection.Drawing:
			Hand.Ins.HandList.Add(this);
			Place = CardManager.cardSection.Hand;
			break;
		case CardManager.cardSection.Discard_H:
			Deadwood.Ins.DeadwoodList.Add(this);
			Place = CardManager.cardSection.Deadwood;
			break;
		case CardManager.cardSection.Discard_T:
			Deadwood.Ins.DeadwoodList.Add(this);
			Place = CardManager.cardSection.Deadwood;
			break;
		case CardManager.cardSection.Playing:
			Table.Ins.TableList.Add(this);
			Place = CardManager.cardSection.Table;
			break;
		}
	}

	public void Drawing(){
		transform.SetParent(Hand.Ins.transform);
		gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(
			Hand.Ins.GetHandCardPosition(this)
		);
	}

	public void Discard(){
		transform.SetParent(Deadwood.Ins.transform);
		gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(
			Deadwood.Ins.GetDeadwoodCardPosition()
		);
	}

	void SkillSetting(){
		//bacis skill
		if(action ==0 && attack ==0 && cards ==0){
			print("nothing");
		}else{
			gameObject.AddComponent<BacisSkill>().init(action, attack, cards);
			BS = gameObject.GetComponent<BacisSkill>();
		}

	}

	public void UseSkill(){
		GameManager.Instance.Cardmanager.CardsS.UseCardSkill(this);
	}
}
