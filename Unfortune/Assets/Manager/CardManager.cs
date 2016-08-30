using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using GameUI;

namespace Manager{
	public class CardManager : MonoBehaviour {

		//Random
		public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());

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

		//temp testing
		public GameObject cardprefabs;

		/// <summary>
		/// Manager
		/// </summary>
		public void init(){
			//init funstion
			init_Positon();
			init_UI();
			//GameManager.Instance.UpdateList += Update_Draw;

			//reading card
			for(int i =0; i<12; i++){
				CreateCardtoDeck(100,true);
			}
		}			

		/// <summary>
		/// Card
		/// </summary>
		public Card FindCardwithID(int ID){
			foreach(Card i in CardList){
				if(i.ID == ID){					
					return i;
				}
			}	
			print("[CardManager]cant find card");
			return null;
		}		


		/// <summary>
		/// Creates the card.
		/// </summary>
		public void CreateCardtoDeck(int Cardkind, bool TopoftheDeck/*true:Top,false:Bottom*/){
			//Undone - if card kind not exsit
			Card Ncard = new Card(CardID, Cardkind, cardSection.Deck);
			Ncard.targetPlace = cardSection.Deck;
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

		public void CreateCardtoDeadwood(int Cardkind){
			//Undone - if card kind not exsit
			Card Ncard = new Card(CardID, Cardkind, cardSection.Deadwood);
			//Card ID
			CardID++;
			//Card List
			CardList.Add(Ncard);
			//Card Place
			Deadwood.Add(Ncard);
		}


		/// <summary>
		/// Deck Function
		/// </summary>
		public void DrawCard(int num){			
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
					Deck.First().AddCardQue(cardSection.Hand);
					//transfer
					Hand.Add(Deck.First());
					Deck.RemoveFirst();
				}

			}
		}

		public void DiscardDeck(int num){
			for(int i=0; i<num; i++){
				if(Deck.First == null){
					break;
				}
				Deck.First().AddCardQue(cardSection.Deadwood);
				//transfer
				Deadwood.Add(Deck.First());
				Deck.RemoveFirst();
			}
		}

		public void DiscardDeckAll(){
			foreach(Card i in Deck){
				Deck.First().AddCardQue(cardSection.Deadwood);
			}
			Deadwood.AddRange(Deck);
			Deck.Clear();
		}

		public void DeckRemove(){
			Deck.First().AddCardQue(cardSection.Remove);
			CardList.Remove(Deck.First());
			Deck.RemoveFirst();
		}			

		public bool isDeckCardReady(){
			bool tempflag = true;
			foreach(Card i in Deck){
				if(i.Place != cardSection.Deck){
					tempflag = false;
				}
			}

			return tempflag;
		}

		public void Update_isDeckfirstCardReady(){
			if(Deck.First().Place == cardSection.Deck){
				Deck.First().targetPlace = cardSection.Drawing;
				GameManager.Instance.UpdateList -= Update_isDeckfirstCardReady;
			}
		}

		//testing print function
		public void PrintDeckCard(){
			foreach(Card i in Deck){
				print("Deck:"+i.ID);
			}
		}
			

		/// <summary>
		/// Hand Function
		/// </summary>
		public void DiscardHand(Card DiscardCard){
			print("dis:"+ DiscardCard.ID);

			if(Hand.Find(x=> x == DiscardCard) != null){
				DiscardCard.AddCardQue(cardSection.Deadwood);

				//transfer
				Deadwood.Add(DiscardCard);

				//Hand Remove
				Hand.Remove(DiscardCard);
			}else{
				print("[CardManager]Card do not exist");
			}
		}

		public void DiscardHandAll(){
			foreach(Card i in Hand){
				i.AddCardQue(cardSection.Deadwood);
			}
			//transfer
			Deadwood.AddRange(Hand);
			Hand.Clear();
		}

		public void HandBacktoDeck(Card Backcard){
			if(Hand.Find(x=> x == Backcard) != null){
				Backcard.AddCardQue(cardSection.Deck);

				//transfer
				Deck.AddFirst(Backcard);

				//Hand Remove
				Hand.Remove(Backcard);
			}else{
				print("[CardManager]Card do not exist");
			}
		}

		public void PlayCard(Card Playcard){
			if(Hand.Find(x=> x == Playcard) != null){
				Playcard.AddCardQue(cardSection.Table);

				//remove card
				Card tempcard = new Card();
				tempcard = Playcard;

				//Hand Remove
				Hand.Remove(Playcard);

				//Table Script

			}else{
				print("[CardManager]Card do not exist");
			}			
		}

		public void HandRemove(Card Removecard){
			if(Hand.Find(x=> x == Removecard) != null){
				Removecard.AddCardQue(cardSection.Remove);
				Hand.Remove(Removecard);
			}else{
				print("[CardManager]Card do not exist");
			}
		}

		public int GetHandNumber(){
			return Hand.Count;
		}		

		public bool isHandCardReady(){
			bool tempflag = true;
			foreach(Card i in Hand){
				if(i.Place != cardSection.Hand){
					tempflag  =false;
				}
			}
			return tempflag;
		}

		//testing print function
		public void PrintHandCard(){
			foreach(Card i in Hand){
				print("Hand:"+i.ID);
			}
		}			

		/// <summary>
		/// Deadwood Function
		/// </summary>
		public void DeadwoodBacktoDeck(){
			//Shuffle
			Shuffle(Deadwood);
			//BacktoDeck
			foreach(Card i in Deadwood){
				Deck.AddFirst(i);
				i.AddCardQue(cardSection.Deck);
			}
			Deadwood.Clear();
		}

		public bool isDeadwoodCardReady(){
			bool tempflag = true;
			foreach(Card i in Deadwood){
				if(i.Place != cardSection.Deadwood){
					tempflag = false;
				}
			}
			return tempflag;
		}

		//testing print function
		public void PrintDeadwoodCard(){
			foreach(Card i in Deadwood){
				print("Deadwood:"+i.ID);
			}
		}


		/// <summary>
		/// private Function
		/// </summary>
		void Shuffle(List<Card> shuffleList){
			for(int i =0; i<shuffleList.Count; i++){
				Card temp = shuffleList[i];
				int randomIndex = ran.Next(i, shuffleList.Count);
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


