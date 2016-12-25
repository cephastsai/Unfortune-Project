using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnd : MonoBehaviour {

	void Start(){
		GameManager.Instance.TE.TEDObjectCL += TurnEndButton;
	}


	public void TurnEndButton(Transform target){
		if(target == transform){
			if(FightingManager.Ins.isPlayerTurn){
				PlayerCardManager.Ins.TurnEnd();
				//FightingManager.Ins.CT.PreesTurn();
			}
		}
	}
}
