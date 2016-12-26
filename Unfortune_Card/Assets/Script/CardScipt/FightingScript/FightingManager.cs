using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightingManager : MonoBehaviour {

	//Singleton
	private static FightingManager _ins = null;

	public static FightingManager Ins{

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

	public int Turn = 1;
	public bool isPlayerTurn = true;

	private bool isPlayerTurnEnd = false;
	private bool isEnemyTurnEnd = false;

	//fighting Variable
	public int PlayerAttack = 0;
	public int EnemyAttack = 0;
	public int Settlement = 0;

	public float PlayerHP = 10;
	public float EnemyHP = 0;
	public float PlayerHPbar = 10;
	public float EnemyHPbar = 0;
	private bool isEnemyHPbarset = false;
	//HP Image
	public Image PlayerHPImage;
	public Image EnemyHPImage;

	//HP
	public bool SetHPflag = false;
	public bool isHPSettingOver = false;
	public GameObject PHPText;
	public GameObject EHPText;

	//Fighting End GameObject
	public ChangeTurn CT;
	public GameObject VictoryMask;
	public GameObject Victory;

	//status manager
	public StatusChange SC; // FightingManager StatusChange


	public void init(bool whoturn){

		//FightingManager initialization
		Turn = 1;
		isPlayerTurn = whoturn;
		PlayerAttack = 0;
		EnemyAttack = 0;
		Settlement = 0;
		EnemyHP = 0;

		//Set Fighting UI
		PHPText.SetActive(true);
		EHPText.SetActive(true);

		//Player CardManager initialization
		PlayerCardManager.Ins.init();
		if(Hand.Ins.HandList.Count ==0){
			Deck.Ins.DrawCards(5);
		}

		//Enemy CardManager initialzation
		EnemyCardManager.Ins.gameObject.AddComponent<EnemyAI_Simple>();
		EDeck.Ins.DrawCards(5);
		EnemyHPbarStart();
		//EnemyCardManager.Ins.ETS.init();

		//Turn Start
		PlayerCardManager.Ins.TurnStart();
	}

	void Update(){


		// Ending Waiting
		if(isPlayerTurnEnd){
			if(PlayerCardManager.Ins.MainSectionQue.Count ==0){				
				if(PlayerCardManager.Ins.isMainSectionOver){
					
					EnemyCardManager.Ins.TurnStart();
					isPlayerTurnEnd = false;
				}
			}
		}

		if(isEnemyTurnEnd){
			if(EnemyCardManager.Ins.MainSectionQue.Count ==0){
				//if(EnemyCardManager.Ins.isMainSectionOver){					
					PlayerCardManager.Ins.TurnStart();
					isEnemyTurnEnd = false;
				//}
			}
		}

		/*if(SetHPflag){
			isHPSettingOver = false;
			HPSetting.Ins.Setting(Settlement);
			SetHPflag = true;
		}

		if(isHPSettingOver){
			if(isPlayerTurn){
				isEnemyTurnEnd = true;
			}else{
				isPlayerTurnEnd = true;
			}
			isHPSettingOver = false;
		}*/



		//HP setting
		if(PlayerHP != PlayerHPbar){
			if(PlayerHPbar -PlayerHP < 0.1f){
				PlayerHPbar = PlayerHP;	
			}
			PlayerHPbar -= (PlayerHPbar -PlayerHP)*Time.deltaTime;
			PlayerHPImage.fillAmount = PlayerHPbar/10;
		}

		if(EnemyHP != EnemyHPbar){
			if(EnemyHPbar -EnemyHP < 0.1f){
				EnemyHPbar = EnemyHP;	
			}
			EnemyHPbar -= (EnemyHPbar -EnemyHP)*Time.deltaTime;
			EnemyHPImage.fillAmount = EnemyHPbar/10;
		}


		//Enemy hp bar start
	
		if(isEnemyHPbarset){
			EnemyHP += 10*Time.deltaTime;

			if(EnemyHPbar >=10 ){
				EnemyHP = 10;
				isEnemyHPbarset = false;
			}
		}
	}


	public void TurnEnding(){
		//Animation
		if(isPlayerTurn){
			CT.LeftStartGetIn();
		}else{
			CT.RightStartGetIn();
		}

		//variable setting
		isPlayerTurn = !isPlayerTurn;
		if(Turn%2 == 0){
			//Turn Settlement
			Settlement = PlayerAttack -EnemyAttack;
			print("Settlement:" +Settlement);

			if(Settlement >0){
				EnemyHP -=Settlement;
			}else{
				PlayerHP +=Settlement;
			}

			//Variable initialization
			PlayerAttack = 0;
			EnemyAttack = 0;
			Settlement = 0;

		}


		//FightingEnd function
		if(EnemyHP <=0){
			EnemyHP =0;
			FightingEnd();
		}else{
			//Turn not end
			if(isPlayerTurn){
				isEnemyTurnEnd = true;
			}else{
				isPlayerTurnEnd = true;
			}

			Turn++;
		}						


	}


	//Fighting End
	public void FightingEnd(){
		print("Fighting Ending");
		//FightingManager initialization


		//Fighting Settlement
		PHPText.SetActive(false);
		EHPText.SetActive(false);

		//Set Fighting UI
		VictoryMask.SetActive(true);
		VictoryMask.AddComponent<FadeIn_Out>().StartFadeIn();
		Victory.SetActive(true);
		Victory.AddComponent<FadeIn_Out>().StartFadeIn();

		//Enemy Card setting

		//info
		StoryManager.Ins.SIManager.FightingEnd();

		GameManager.Instance.TE.TEDScreen += FightingEndTouchScreen;
	}

	public void FightingEndTouchScreen(Vector3 pos){

		//Victory
		VictoryMask.GetComponent<FadeIn_Out>().StartFadeOut();
		Victory.GetComponent<FadeIn_Out>().StartFadeOut();

		GameManager.Instance.SetGameSection(GameManager.GameSection.Story);

		GameManager.Instance.TE.TEDScreen -= FightingEndTouchScreen;
	}

	public void CardSkillChoise(){

		//play cards
		if(isPlayerTurn){
			
		}else{
			
		}
	}

	public void SetHP(Image targetHP){
		
	}

	public void EnemyHPbarStart(){
		isEnemyHPbarset = true;
	}
}
