using UnityEngine;
using System.Collections;
using Manager;

public class PlayerActions : MonoBehaviour {

	//Drawing Variable
	private float DrawingIntervalTime = 0.1f;
	private int DrawingCardNumber = 0;
	private float DrawingTimer = 0;

	void Start(){				
		Drawing(5);
	}

	void OnDestroy(){
		GameManager.Instance.UpdateList -= Update_DrawingTimer;
	}


	public void Drawing(int num){	
		DrawingCardNumber +=num;	
		GameManager.Instance.UpdateList += Update_isDeckCardReady;
	}
		
	public void Playing(){
		
	}

	public void EndofTurn(){
		GameManager.Instance.UpdateList += Update_isHandCardReady;
		Drawing(5);
	}

	//update function
	public void Update_DrawingTimer(){		
		if(DrawingCardNumber >0){
			if(Time.time - DrawingTimer > DrawingIntervalTime){			
				GameManager.Instance.cardmanager.DrawCard(1);
				//reset
				DrawingTimer = Time.time;
				DrawingCardNumber--;
			}
		}else{
			GameManager.Instance.UpdateList -= Update_DrawingTimer;
		}

	}

	public void Update_isHandCardReady(){
		if(GameManager.Instance.cardmanager.isHandCardReady()){
			GameManager.Instance.cardmanager.DiscardHandAll();
			GameManager.Instance.UpdateList -= Update_isHandCardReady;
		}
	}

	public void Update_isDeckCardReady(){
		if(GameManager.Instance.cardmanager.isDeckCardReady()){
			GameManager.Instance.UpdateList += Update_DrawingTimer;
			DrawingTimer = Time.time;
			GameManager.Instance.UpdateList -= Update_isDeckCardReady;
		}
	}


}
