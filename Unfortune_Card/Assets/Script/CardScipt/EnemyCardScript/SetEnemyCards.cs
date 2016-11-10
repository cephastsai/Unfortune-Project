using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetEnemyCards : MonoBehaviour {

	public void SetCards(int[] Cardlist){
		
		foreach(int i in Cardlist){
			CardManager.Ins.CreateCard(i, false);
		}
	}
}
