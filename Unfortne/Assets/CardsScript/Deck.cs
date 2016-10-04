using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Deck : MonoBehaviour {

	//Singleton
	private static Deck _ins = null;

	public static Deck Ins{

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


	//Card List
	public List<Card> DeckList = new List<Card>(); 

	//Position
	public float constantX = 0.05f;
	public float constantY = 0.05f;

	private Vector3 GetDeckCardPosition(){
		
		return new Vector3(
			constantX*(DeckList.Count-1),
			constantY*(DeckList.Count-1),
			0.1f*(DeckList.Count-1)
		);
	}

	public void init(GameObject Ncard){
		//transform
		Ncard.transform.SetParent(transform);
		Ncard.transform.position = new Vector3(0, 0, 0);
		Ncard.transform.localPosition = GetDeckCardPosition();
		Ncard.GetComponent<Card>().CardsUp(false);

		//list
		DeckList.Add(Ncard.GetComponent<Card>());
	}

	public void Drawing(CardManager.MainSection Tsec,int CardsNumber){		
		if(DeckList.Count>0){						
			for(int i=0;i<CardsNumber;i++){
				//section check
				DeckList.Last().isSectionOver = false;
				Tsec.CheckLsit.Add(DeckList.Last());
				DeckList.Last().Place = CardManager.cardSection.Drawing;
				DeckList.Last().Drawing();

				//remove
				DeckList.Remove(DeckList.Last());

			}
		}

	}

	public void DrawCards(int CardsNumber){
		for(int i=0; i<CardsNumber; i++){
			GameManager.Instance.Cardmanager.AddMainQue(CardManager.cardSection.Drawing);
		}
	}
}
