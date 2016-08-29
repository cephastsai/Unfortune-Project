using UnityEngine;
using System.Collections;
using Manager;

public class PlayerActions : MonoBehaviour {

	//Drawing Variable
	private float DrawingIntervalTime = 0.1f;
	public int DrawingCardNumber = 0;
	private float DrawingTimer = 0;

	void Start(){				
		Drawing(3);
		Drawing(2);
	}

	void OnDestroy(){
		GameManager.Instance.UpdateList -= Update_DrawingTimer;
	}


	public void Drawing(int num){	
		DrawingCardNumber +=num;
		GameManager.Instance.UpdateList += Update_DrawingTimer;
		DrawingTimer = Time.time;
	}
		
	public void Playing(){
		
	}

	public void EndofTurn(){
		GameManager.Instance.cardmanager.DiscardHandAll();
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


}
