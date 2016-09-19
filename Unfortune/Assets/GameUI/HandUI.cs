using UnityEngine;
using System.Collections;
using Manager;


namespace GameUI{
	public class HandUI : MonoBehaviour {

		//CardManager
		private CardManager CM = GameManager.Instance.cardmanager;

		// UI Variable
		private float width = 400f;
		private float Height = 100f;
		private float Spacing = 15f;
		private Transform HandPosition;
		public int HandCardNumber;

		public Vector3 GetHandCardPosition(Card i){
			HandPosition = GameManager.Instance.cardmanager.HandPosition;
			HandCardNumber = GameManager.Instance.cardmanager.Hand.Count;
			return 
				new Vector3(
					HandPosition.position.x +Spacing*(CM.Hand.FindIndex(x=> x ==i)), 
					HandPosition.position.y, 
					HandPosition.position.z);
		}
	}
}

