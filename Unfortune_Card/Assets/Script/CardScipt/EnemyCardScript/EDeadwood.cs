using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EDeadwood : MonoBehaviour {

	//temp
	public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());

	//Singleton
	private static EDeadwood _ins = null;

	public static EDeadwood Ins{

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
	public Transform Deadwoodpos;
	public Transform DeadwoodposE;

	//Position
	public float constantX = 0.005f;
	public float constantY = 0.005f;

	//Cards List
	public List<Card> DeadwoodList = new List<Card>();

	public Vector3 GetDeadwoodCardPosition(Card target){
		target.SetCardSprtingOrderNumber(DeadwoodList.Count);	
		return new Vector3(
			-constantX*(DeadwoodList.Count),
			-constantY*(DeadwoodList.Count),
			-0.1f*(DeadwoodList.Count-1)
		);
	}

	public void Shuffling(EnemyCardManager.MainSection Tsec){
		
		for(int i=0; i<DeadwoodList.Count; i++){
			//shuffle list
			Card temp = DeadwoodList[i];
			int randomIndex = ran.Next(i, DeadwoodList.Count);
			DeadwoodList[i] = DeadwoodList[randomIndex];
			DeadwoodList[randomIndex] = temp;

			//card setting
			DeadwoodList[i].isSectionOver = false;
			Tsec.CheckLsit.Add(DeadwoodList[i]);
			DeadwoodList[i].Place = CardManager.cardSection.Shuffle;
			DeadwoodList[i].Shuffle(i);
		}

		DeadwoodList.Clear();
	}
}
