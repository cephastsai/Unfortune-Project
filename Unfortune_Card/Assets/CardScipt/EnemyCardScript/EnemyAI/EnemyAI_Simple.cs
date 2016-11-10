using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyAI_Simple : MonoBehaviour {

	private Dictionary<int, Card> WeightsCardList = new Dictionary<int, Card>();

	//Timer
	private float Timer;
	private float ranWaitingTime;
	private bool isWaitPlayCard = false;

	void Update(){

		if(EnemyCardManager.Ins.isEnemyAIOn){
			//AI
			if(!FightingManager.Ins.isPlayerTurn){
				if(EnemyCardManager.Ins.MainSectionQue.Count == 0 && EnemyCardManager.Ins.isMainSectionOver){
					if(WeightsCardList.Count != 0){

						if(ETable.Ins.ActionNumber ==0 || EHand.Ins.HandList.Count ==0){
							if(!isWaitPlayCard){
								Timer = Time.time;
								isWaitPlayCard = true;
								ranWaitingTime = 1;
							}

							if(Time.time - Timer > ranWaitingTime){
								EnemyCardManager.Ins.TurnEnd();
							}
														
						}else{
							if(!isWaitPlayCard){
								Timer = Time.time;
								isWaitPlayCard = true;
								ranWaitingTime = (float)GameManager.ran.Next(5, 15)/10f;
							}

							if(Time.time - Timer > ranWaitingTime){
								int temp = WeightsCardList.Last().Key;
								foreach(KeyValuePair<int, Card> item in WeightsCardList){
									if(item.Key > temp){
										temp = item.Key;
									}
								}
								//print("playcard:"+WeightsCardList[temp].ID);
								WeightsCardList[temp].gameObject.GetComponent<EPlayCard>().PlayingCard();
								WeightsCardList.Remove(temp);
								isWaitPlayCard = false;
							}
						}


					}


				}
			}
		}

	}



	public void SettingCard(){		
		foreach(Card i in EHand.Ins.HandList){
			if(!WeightsCardList.ContainsValue(i)){
				int tempweights =
					1+
					i.action* 	11+
					i.attack* 	4+
					i.cards* 	5;			

				AddWeightList(tempweights, i);
			}
		}			
	}

	void AddWeightList(int key, Card target){
		if(!WeightsCardList.ContainsKey(key)){
			WeightsCardList.Add(key, target);
			//print(key +","+target.ID);
		}else{
			AddWeightList(key-1, target);
		}
	}

}
