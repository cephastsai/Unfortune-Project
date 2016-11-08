﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EHand : MonoBehaviour {
	//Singleton
	private static EHand _ins = null;

	public static EHand Ins{

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

	//position
	public Transform Handpos;
	public Transform HandposE;

	//list
	public List<Card> HandList = new List<Card>();

	//Main Variable
	public bool isCardsCanPlay = true;

	// UI Variable
	private float Spacing = -1f;
	private float Depth = -0.1f;
	public int HandCardNumber = 0;
	private int Reservations = 0;

	public Vector3 GetHandCardPosition(Card i){
		Reservations++;	
		return 
			new Vector3(
				Spacing*(Reservations-1),
				0f, 
				-Depth*(Reservations-1)
			);
	}

	public Vector3 SetHandCardPosition(Card i){
		return 
			new Vector3(Spacing*(HandList.FindIndex(x=> x ==i)), 0f, Depth*(HandList.FindIndex(x=> x ==i)));
	}

	void Update(){
		if(GameManager.GameMainSection == GameManager.GameSection.Cards){
			if(HandList.Count != HandCardNumber){
				// login hand list and position
				foreach(Card i in HandList){
					//position

					//i.transform.localPosition = SetHandCardPosition(i);
					if(i.GetComponent<GameObjectMoving>() != null){					
						i.GetComponent<GameObjectMoving>().SetTergetPostion(
							SetHandCardPosition(i),
							5f
						);
					}else{
						i.gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(
							SetHandCardPosition(i),
							5f
						);
					}


					i.transform.localRotation = Quaternion.Euler(0,0,0);
					i.SetCardSprtingOrder(i);

					//component
					if(i.GetComponent<EPlayCard>() == null){
						i.gameObject.AddComponent<EPlayCard>();
					}

					/*if(i.GetComponent<Browsing>() == null){		
						i.gameObject.AddComponent<Browsing>();
					}*/

					EnemyCardManager.Ins.gameObject.GetComponent<EnemyAI_Simple>().SettingCard();

				}					

				if(EnemyCardManager.Ins.MainSec.MSection != CardManager.cardSection.GetCard){
					HandCardNumber = HandList.Count;
					Reservations = HandList.Count;
				}

			}

			if(EnemyCardManager.Ins.Endingflag){
				if(EnemyCardManager.Ins.MainSectionQue.Count >0){
					isCardsCanPlay = false;
				}else{
					isCardsCanPlay = true;
				}
			}


			/*
			if(Table.Ins.ActionNumber ==0){
				isCardsCanPlay = false;
			}*/
		}			

	}

	public void Discard_H_All(EnemyCardManager.MainSection Tsec){				
		foreach(Card i in HandList){
			i.isSectionOver = false;
			Tsec.CheckLsit.Add(i);
			i.Place = CardManager.cardSection.Discard_H;
			i.Discard_H();
		}
		HandList.Clear();
	}

	public void HandRemove(EnemyCardManager.MainSection Tsec){
		foreach(Card i in Hand.Ins.HandList){
			if(i.CardKind == Tsec.cardkind){
				//HandList.Remove(i);
				i.isSectionOver = false;
				Tsec.CheckLsit.Add(i);
				i.Place = CardManager.cardSection.HandRemove;
				i.HandRemove();
				i.gameObject.AddComponent<StartBurn>().GetMat();
				i.transform.GetChild(0).gameObject.AddComponent<StartBurn>().GetMat();
				break;
			}
		}
	}
}
