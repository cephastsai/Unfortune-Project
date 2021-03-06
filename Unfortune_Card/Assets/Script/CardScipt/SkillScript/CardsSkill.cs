﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class CardsSkill : MonoBehaviour {

	//Cards Skill List 
	public Dictionary<int, string> CardsSkillList = new Dictionary<int, string>();

	//EndingSkill
	public bool isSkillEnd = false;

	//json
	private string jsonString;
	private JsonData jsonData;

	public void init(){		
		jsonString = File.ReadAllText(Application.dataPath +"/Json/CardsSkill.json");
		jsonData = JsonMapper.ToObject(jsonString);	

		for(int i=0; i<jsonData.Count; i++){
			//print(i);
			//print(int.Parse(jsonData[i]["ID"].ToString())+":"+jsonData[i]["Skill"].ToString());

			CardsSkillList.Add(int.Parse(jsonData[i]["ID"].ToString()), jsonData[i]["Skill"].ToString());
		}

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
		
		if(CardsSkillList.ContainsKey(i.CardKind)){
			
			gameObject.SendMessage(CardsSkillList[i.CardKind], i);

			//Invoke(CardsSkillList[i.CardKind].skillkind, 0f);
		}
		i.SectionOver();
	}			

	//cards skill private function
	private bool isCardinTable(int kind){
		bool iscardintable = false;

		foreach(Card i in Table.Ins.TableList){
			if(i.CardKind == kind){
				iscardintable = true;
			}
		}			

		return iscardintable;
	}

	private int CardNumberinTable(int kind){
		int cardnumber = 0;

		foreach(Card i in Table.Ins.TableList){
			if(i.CardKind == kind){
				cardnumber++;
			}
		}	

		return cardnumber;
	}


	//Cards Skill Function

	private void PlusInitCard(Card target){
		PlayerCardManager.Ins.TTurn.initCards +=1;
	}

	private void HandRemove_Hunger(Card target){
		if(isSkillEnd){
			
		}else{
			PlayerCardManager.Ins.AddMainQue(CardManager.cardSection.HandRemove, 9);
		}
	}

	private void Hunger(Card target){
		/*print("12");
		if(!runningEndingSkill){
			print("123");
			EndingSkill += Hunger;
		}
		PlayerCardManager.Ins.AddMainQue(CardManager.cardSection.HandRemove,100);
		EndingSkill -= Hunger;*/
	}

	public void AddMoney(Card target){
		GameManager.Instance.Money +=10;
	}

	public void BladeofRevenge(Card target){
		if(isSkillEnd){
			
		}else{
			int Rnum = 0;
			foreach(Card i in Table.Ins.TableList){
				if(i.CardKind == 112){
					Rnum++;
				}
			}
			PlayerCardManager.Ins.TTurn.Attack +=Rnum;
		}

	}

	public void DefendofShield(){
		//enemy
		if(isSkillEnd){
			EnemyCardManager.Ins.TTurn.Attack = PlayerCardManager.Ins.TTurn.Attack;
		}else{
			EnemyCardManager.Ins.TTurn.Attack = 0;
		}
	}

	public void Crossfire(){
		if(!isSkillEnd){
			EnemyCardManager.Ins.TTurn.Attack += CardNumberinTable(17);
		}
	}

	public void assault(){
		EnemyCardManager.Ins.TTurn.Attack += 1;
	}
}
