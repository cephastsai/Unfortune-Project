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

		init();
	}

	//Random
	public static System.Random ran = new System.Random(Guid.NewGuid().GetHashCode());

	//Manager
	public CardManager Cardmanager;
	public FightingManager Fightingmanager;
	//public MapManager Mapmanager;
	public TouchEvent TE;
	//public UIManager UImanager;
	//public StoryManager Storymanager;

	//Game Variable
	public int Money = 0;

	public enum GameSection{
		Map,
		Story,
		Cards
	};
	public static GameSection GameMainSection;

	void init(){
		Cardmanager = GameObject.Find("CardManager").GetComponent<CardManager>();
		Fightingmanager = GameObject.Find("FightingManager").GetComponent<FightingManager>();
		TE = GameObject.Find("TouchManager").GetComponent<TouchEvent>();
	}

	void Start(){
		//Manager setting

		Fightingmanager.init(true);

		//Mapmanager = GameObject.Find("MapManager").GetComponent<MapManager>();
		//Mapmanager.init();

		//UImanager = GameObject.Find("UIManager").GetComponent<UIManager>();

		//Storymanager = GameObject.Find("StoryManager").GetComponent<StoryManager>();

		GameMainSection = GameSection.Cards;

		//SetGameSection(GameSection.Cards);
	}


	public void SetGameSection(GameSection GS){
		/*
		switch(GS){
		case GameSection.Map:			
			GameMainSection = GameSection.Map;
			Mapmanager.SetStoryPiont();
			print(Mapmanager.MainST.data.SPName);
			Hand.Ins.isCardsCanPlay = false;
			break;
		case GameSection.Story:
			GameMainSection = GameSection.Story;
			//set UI
			UImanager.MapToStory();
			Hand.Ins.isCardsCanPlay = false;
			break;
		case GameSection.Cards:
			GameMainSection = GameSection.Cards;
			if(Mapmanager.MainST.data.SelectCardID.Count >0){
				UImanager.StoryToChose();
				Cardmanager.select.init(Mapmanager.MainST.data.SelectCardID, Mapmanager.MainST.data.SelectNum);
			}else if(Mapmanager.MainST.data.OptionCardID.Count >0){
				UImanager.StoryToChose();
				Cardmanager.getCards.init(Mapmanager.MainST.data.OptionCardID);
			}else{
				UImanager.StoryToFight();
				Cardmanager.CardUIIN();
				Hand.Ins.isCardsCanPlay = true;
			}
			Cardmanager.Status.text = Mapmanager.MainST.data.SPtitle;
			break;
		}*/

	}		
}
