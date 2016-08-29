using UnityEngine;
using System.Collections;
using Manager;

public class DeckUI : MonoBehaviour {

	private Transform DeckPosition;
	private int DeckCardNumber;

	public float constantX = 0.05f;
	public float constantY = 0.05f;

	public CardManager CM = GameManager.Instance.cardmanager;

	public Vector3 GetDeckCardPosition(){
		DeckPosition = CM.DeckPosition;
		DeckCardNumber = CM.Deck.Count;

		return new Vector3(
			DeckPosition.position.x +constantX*(DeckCardNumber-1),
			DeckPosition.position.y +constantY*(DeckCardNumber-1),
			DeckPosition.position.z -0.1f*(DeckCardNumber-1));
	}
	/*
	public Vector3 GetDeckShufflePostion(Card shuffleCard){
		DeckPosition = CM.DeckPosition;
		int PositionNumber = -1;

		PositionNumber = CM.Deck.Find(shuffleCard).
	}*/
}
