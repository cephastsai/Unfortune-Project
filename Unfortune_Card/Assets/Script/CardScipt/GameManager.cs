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
	public UIManager UImanager;
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

		//Mapmanager = GameObject.Find("MapManager").GetComponent<MapManager>();
		//Mapmanager.init();

		//UImanager = GameObject.Find("UIManager").GetComponent<UIManager>();

		//Storymanager = GameObject.Find("StoryManager").GetComponent<StoryManager>();

		GameMainSection = GameSection.Story;

		//SetGameSection(GameSection.Cards);
	}


	public void SetGameSection(GameSection GS){
		
		switch(GS){
		/*case GameSection.Map:			
			GameMainSection = GameSection.Map;
			Mapmanager.SetStoryPiont();
			Hand.Ins.isCardsCanPlay = false;
			break;*/
		case GameSection.Story:
			GameMainSection = GameSection.Story;

			UImanager.BattleOver();
			EnemyCardManager.Ins.DestroyEnemyCards();
			Hand.Ins.StoryHandBack();
			//info
			StoryManager.Ins.SIManager.SetPaperInfo();

			Hand.Ins.isCardsCanPlay = false;
			break;
		case GameSection.Cards:
			GameMainSection = GameSection.Cards;
			//set UI
			Hand.Ins.StoryHandUp();
			UImanager.BattleBegin();
			Fightingmanager.init(true);
			Hand.Ins.isCardsCanPlay = true;
			break;
		}

	}		
}
