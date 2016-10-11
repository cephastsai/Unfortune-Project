using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

	//Singleton
	private static GameManager _instance = null;

	public static GameManager Instance{

		get{
			return _instance;	
		}
	}//Instance

	void Awake(){
		if(_instance == null){
			_instance = this;

			DontDestroyOnLoad(gameObject);
		}else if(_instance != this){
			Destroy(gameObject);
		}
	}

	//Random
	public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());

	//Manager
	public CardManager Cardmanager;
	public TouchEvent TE;

	public enum GameSection{
		Map,
		Story,
		Cards
	};
	public static GameSection GameMainSection;



	void Start(){
		//Manager setting
		Cardmanager = GameObject.Find("CardManager").GetComponent<CardManager>();

		TE = GameObject.Find("TouchManager").GetComponent<TouchEvent>();

		GameMainSection = GameSection.Story;
	}


	public void SetGameSection(){


	}
}
