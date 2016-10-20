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
	public MapManager Mapmanager;
	public TouchEvent TE;
	public UIManager UImanager;
	public StoryManager Storymanager;

	public enum GameSection{
		Map,
		Story,
		Cards
	};
	public static GameSection GameMainSection;



	void Start(){
		//Manager setting
		Cardmanager = GameObject.Find("CardManager").GetComponent<CardManager>();

		Mapmanager = GameObject.Find("MapManager").GetComponent<MapManager>();
		Mapmanager.init();

		TE = GameObject.Find("TouchManager").GetComponent<TouchEvent>();

		UImanager = GameObject.Find("UIManager").GetComponent<UIManager>();

		Storymanager = GameObject.Find("StoryManager").GetComponent<StoryManager>();

		GameMainSection = GameSection.Map;

		SetGameSection(GameSection.Map);
	}


	public void SetGameSection(GameSection GS){

		switch(GS){
		case GameSection.Map:
			Mapmanager.SetStoryPiont();

			break;
		case GameSection.Story:
			//set UI
			UImanager.MapToStory();

			break;
		case GameSection.Cards:			
			if(Mapmanager.MainST.data.SelectCardID.Count >0){
				UImanager.StoryToChose();
				Hand.Ins.isCardsCanPlay = false;
				Cardmanager.select.init(Mapmanager.MainST.data.SelectCardID, Mapmanager.MainST.data.SelectNum);
			}else{
				UImanager.StoryToFight();
				Cardmanager.CardUIIN();
			}
			Cardmanager.Status.text = Mapmanager.MainST.data.SPtitle;
			break;
		}

	}
}
