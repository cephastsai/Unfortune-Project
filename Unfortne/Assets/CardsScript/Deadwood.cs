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

	//Cards List
	public List<Card> DeadwoodList = new List<Card>();

	public Vector3 GetDeadwoodCardPosition(){
		return new Vector3(0,0,0);
	}		
}
