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


	//list
	public List<Card> HandList = new List<Card>();

	//Main Variable
	public bool isCardsCanPlay = true;

	// UI Variable
	private float Spacing = 10f;
	private float Depth = -0.05f;
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

		if(HandList.Count != HandCardNumber){
			// login hand list and position
			foreach(Card i in HandList){
				//position
				i.transform.localPosition = SetHandCardPosition(i);

				//component
				if(i.GetComponent<PlayCard>() == null){
					i.gameObject.AddComponent<PlayCard>();
				}

				if(i.GetComponent<Browsing>() == null){
					i.gameObject.AddComponent<Browsing>();
				}
			}

			HandCardNumber = HandList.Count;
			Reservations = HandList.Count;
		}


		if(GameManager.Instance.Cardmanager.MainSectionQue.Count >0){
			isCardsCanPlay = false;
		}else{
			isCardsCanPlay = true;
		}
	}

	public void Discard_H_All(CardManager.MainSection Tsec){				
		foreach(Card i in HandList){
			i.isSectionOver = false;
			Tsec.CheckLsit.Add(i);
			i.Place = CardManager.cardSection.Discard_H;
			i.Discard();
		}
		HandList.Clear();
	}
}
