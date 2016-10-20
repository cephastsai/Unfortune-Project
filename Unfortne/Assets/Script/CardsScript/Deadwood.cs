using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deadwood : MonoBehaviour {
	//Singleton
	private static Deadwood _ins = null;

	public static Deadwood Ins{

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

	//Cards List
	public List<Card> DeadwoodList = new List<Card>();

	public Vector3 GetDeadwoodCardPosition(){
		return new Vector3(0,0,0);
	}

	public void Shuffling(CardManager.MainSection Tsec){
		
		for(int i=0; i<DeadwoodList.Count; i++){
			//shuffle list
			Card temp = DeadwoodList[i];
			int randomIndex = GameManager.ran.Next(i, DeadwoodList.Count);
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
