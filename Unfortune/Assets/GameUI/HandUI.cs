using UnityEngine;
using System.Collections;
using Manager;


namespace GameUI{
	public class HandUI : MonoBehaviour {

		// UI Variable
		private float width = 400f;
		private float Hright = 100f;
		private float Spacing = 15f;
		private Transform HandPosition;
		public int HandCardNumber;

		public Vector3 GetHandCardPosition(){
			HandPosition = GameManager.Instance.cardmanager.HandPosition;
			HandCardNumber = GameManager.Instance.cardmanager.Hand.Count;
			return 
				new Vector3(
					HandPosition.position.x +Spacing*(HandCardNumber-1), 
					HandPosition.position.y, 
					HandPosition.position.z);
		}
	}
}

