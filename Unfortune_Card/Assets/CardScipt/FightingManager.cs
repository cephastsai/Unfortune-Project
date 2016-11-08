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


	public void init(bool whoturn){

		//FightingManager initialization
		isPlayerTurn = whoturn;

		//Set Fighting UI 

		//Player CardManager initialization
		PlayerCardManager.Ins.init();

		//Enemy CardManager initialzation
		EnemyCardManager.Ins.gameObject.AddComponent<EnemyAI_Simple>();

		//Turn Start
		PlayerCardManager.Ins.TurnStart();

	}

	public void TurnEnding(){
		
		//variable setting
		isPlayerTurn = !isPlayerTurn;
		if(Turn%2 == 0){
			//Settlement


		}else{
			if(isPlayerTurn){
				
			}else{
				EnemyCardManager.Ins.TurnStart();
			}
		}


		//if heal <0
		//FightingEnd function


		Turn++;
	}


	public void FightingEnd(){

		//FightingManager initialization

		//Set Fighting UI

	}

}
