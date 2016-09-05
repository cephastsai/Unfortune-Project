using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using GameUI;

namespace Manager{
	public class CardManager : MonoBehaviour {

		//static enum
		public enum cardSection{
			nil, 
			Deck, Hand, Deadwood, Table,
			Drawing, BacktoDeck, Playing, Shuffle,
			Discard_H, Discard_T, Discard_D,
			Remove
		};

		//card list
		public List<Card> CardList = new List<Card>();
		private int CardID = 0;

		//Que section
		private Queue<cardSection> MainSectionQue = new Queue<cardSection>();
		public cardSection MainSection;
		public bool isMainSectionOver = true;

		//card Deck
		public LinkedList<Card> Deck = new LinkedList<Card>();
		public Transform DeckPosition = null;

		//card Hand
		public List<Card> Hand = new List<Card>();
		public Transform HandPosition = null;

		//card Deadwood
		public List<Card> Deadwood = new List<Card>();
		public Transform DeadwoodPosition = null;

		//UI Script
		public DeckUI DUI;
		public HandUI HUI;
		public DeadwoodUI DwUI;

		//init
		public void init(){
			//init funstion
			init_Positon();
			init_UI();

			//update init
			GameManager.Instance.UpdateList += Update_MainSection;

			//reading card

			for(int i =0; i<12; i++){
				CreateCardtoDeck(100,true);
			}

			//start draw card
			DrawCard(5);
		}

		//Update function
		public void Update_MainSection(){
			bool tempflag = true;

			foreach(Card i in CardList){
				if(!i.isSectionOver){
					tempflag = false;
				}
			}

			isMainSectionOver = tempflag;

			if(isMainSectionOver &&MainSectionQue.Count >0){
				MainSection = MainSectionQue.Dequeue();
			}
			print(isMainSectionOver);
			print(MainSection);
		}

		/// <summary>
		/// Creates the card.
		/// </summary>
		public void CreateCardtoDeck(int Cardkind, bool TopoftheDeck/*true:Top,false:Bottom*/){
			//Undone - if card kind not exsit
			Card Ncard = new Card(CardID, Cardkind, cardSection.Deck);
			//Create GameObject
			GameObject NcardObject = Instantiate(GameManager.Instance.prefabsmanager.PrefabsList[Cardkind]);
			NcardObject.AddComponent<CardScript>().init(Ncard, NcardObject);
			//Card ID
			CardID++;
			//Card List
			CardList.Add(Ncard);
			//Card Place
			if(TopoftheDeck){
				Deck.AddFirst(Ncard);
			}else{
				Deck.AddLast(Ncard);
			}						
		}


		/// <summary>
		/// Deck Function
		/// </summary>
		public void DrawCard(int num){
			MainSectionQue.Enqueue(cardSection.Drawing);
			for(int i =0; i<num; i++){
				if(Deck.First == null){
					if(Deadwood.Count >0){
						DeadwoodBacktoDeck();
						DrawCard(num -i);
						break;
					}else{
						break;
					}

				}else{
					Deck.First().Place = cardSection.Drawing;
					//transfer
					Hand.Add(Deck.First());
					Deck.RemoveFirst();
				}

			}
		}

		/// <summary>
		/// Hand Function
		/// </summary>
		/*public void DiscardHandAll(){
			MainSectionQue.Enqueue(cardSection.Discard_H);
			foreach(Card i in Hand){
				i.Place = CardManager.cardSection.Discard_H;
			}
			//transfer
			Deadwood.AddRange(Hand);
			Hand.Clear();
		}*/



		/// <summary>
		/// Deadwood Function
		/// </summary>
		public void DeadwoodBacktoDeck(){
			MainSectionQue.Enqueue(cardSection.Shuffle);
			//Shuffle
			Shuffle(Deadwood);
			//BacktoDeck
			foreach(Card i in Deadwood){
				i.Place = cardSection.Shuffle;
				Deck.AddFirst(i);
			}
			Deadwood.Clear();
		}

		/// <summary>
		/// private Function
		/// </summary>
		void Shuffle(List<Card> shuffleList){
			for(int i =0; i<shuffleList.Count; i++){
				Card temp = shuffleList[i];
				int randomIndex = GameManager.ran.Next(i, shuffleList.Count);
				shuffleList[i] = shuffleList[randomIndex];
				shuffleList[randomIndex] = temp;
			}
		}

		void init_Positon(){			
			DeckPosition =  GameObject.Find("DeckPosition").transform;
			HandPosition =  GameObject.Find("HandPosition").transform;
			DeadwoodPosition =  GameObject.Find("DeadwoodPosition").transform;
		}	

		void init_UI(){
			DUI = gameObject.AddComponent<DeckUI>();
			HUI = gameObject.AddComponent<HandUI>();
			DwUI = gameObject.AddComponent<DeadwoodUI>();
		}
	}
}


