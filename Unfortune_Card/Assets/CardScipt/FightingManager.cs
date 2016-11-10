using UnityEngine;
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

	//fighting Variable
	public int PlayerAttack = 0;
	public int EnemyAttack = 0;
	public int Settlement = 0;

	public int PlayerHP = 10;
	public int EnemyHP = 10;


	public void init(bool whoturn){

		//FightingManager initialization
		isPlayerTurn = whoturn;
		PlayerAttack = 0;
		EnemyAttack = 0;
		Settlement = 0;
		EnemyHP = 10;

		//Set Fighting UI 

		//Player CardManager initialization
		PlayerCardManager.Ins.init();

		//Enemy CardManager initialzation
		EnemyCardManager.Ins.gameObject.AddComponent<EnemyAI_Simple>();
		EDeck.Ins.DrawCards(5);

		//Turn Start
		PlayerCardManager.Ins.TurnStart();

	}

	public void TurnEnding(){
		
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
				PlayerCardManager.Ins.TurnStart();
			}else{
				EnemyCardManager.Ins.TurnStart();
			}

			Turn++;
		}


	}


	public void FightingEnd(){
		print("Fighting Ending");
		//FightingManager initialization
		Turn = 1;
		PlayerAttack = 0;
		EnemyAttack = 0;
		EnemyHP = 10;

		//Fighting Settlement


		//Set Fighting UI



	}

}
