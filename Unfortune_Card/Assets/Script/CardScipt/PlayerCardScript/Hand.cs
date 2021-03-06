﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour {
	//Singleton
	private static Hand _ins = null;

	public static Hand Ins{

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
	private float Spacing = 1f;
	private float Depth = -0.1f;
	public int HandCardNumber = 0;
	private int Reservations = 0;

	//Discard_T
	private Stack<Card> Discard_H_List = new Stack<Card>();
	private bool isDiscard_HStart = false;
	private float Timer = 0;
	private float preDiscardTime = 0.2f;


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
					if(i.GetComponent<PlayCard>() == null){
						i.gameObject.AddComponent<PlayCard>();
					}

					if(i.GetComponent<Browsing>() == null){		
						i.gameObject.AddComponent<Browsing>();
					}

					if(i.GetComponent<CardCanPlay>() == null){
						i.gameObject.AddComponent<CardCanPlay>().init();
					}else{
						i.GetComponent<CardCanPlay>().Cardposition = SetHandCardPosition(i);
					}																			

				}					

				if(PlayerCardManager.Ins.MainSec.MSection != CardManager.cardSection.GetCard){
					HandCardNumber = HandList.Count;
					Reservations = HandList.Count;
				}

			}

			if(PlayerCardManager.Ins.Endingflag){
				if(PlayerCardManager.Ins.MainSectionQue.Count >0){
					isCardsCanPlay = false;
				}else{
					isCardsCanPlay = true;
				}
			}


			/*
			if(Table.Ins.ActionNumber ==0){
				isCardsCanPlay = false;
			}*/

			//discard_H
			if(isDiscard_HStart){
				if(Discard_H_List.Count >0){
					if(Time.time - Timer >= preDiscardTime){					
						Card temp =  Discard_H_List.Pop();
						temp.Place =CardManager.cardSection.Discard_T;
						temp.Discard_T();

						Timer = Time.time;
					}
				}else{
					isDiscard_HStart = false;
				}
			}
		}						
	}

	public void Discard_H_All(PlayerCardManager.MainSection Tsec){				
		foreach(Card i in HandList){
			i.isSectionOver = false;
			Tsec.CheckLsit.Add(i);

			Discard_H_List.Push(i);
		}

		isDiscard_HStart = true;
		Timer = Time.time;

		HandList.Clear();
	}

	public void HandRemove(PlayerCardManager.MainSection Tsec){
		foreach(Card i in Hand.Ins.HandList){
			if(i.CardKind == Tsec.cardkind){
				//HandList.Remove(i);
				PlayerCardManager.Ins.CardList.Remove(i);
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

	public void StoryHandBack(){
		gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(transform.localPosition - new Vector3(0, 3, 0), 0.5f);
	}

	public void StoryHandUp(){
		if(transform.localPosition.y <-5){
			gameObject.AddComponent<GameObjectMoving>().SetTergetPostion(transform.localPosition + new Vector3(0, 3, 0), 0.5f);	
		}

	}
}
