using UnityEngine;
using System.Collections;

public class CardInfoAnimation : MonoBehaviour {

	public RectTransform RT;
	private bool isAnimationStart = false;

	private float Timer;
	private bool isTimerOn = false;


	public void start(){		
		Timer = Time.time;
		isAnimationStart = false;
		isTimerOn = true;

		RT.localScale = new Vector3(0, 1, 1);
	}

	public void end(){
		RT.localScale = new Vector3(1, 1, 1);
		isAnimationStart = false;
		isTimerOn = false;
	}


	void Update () {
		//Timer
		//print(isAnimationStart);
		if(isTimerOn){
			if(Time.time - Timer > 0.4f){
				isAnimationStart = true;
				isTimerOn = false;
			}
		}

		

		//Animation
		if(isAnimationStart){
			
			RT.localScale += new Vector3(0.06f, 0, 0);

			if(RT.localScale.x >= 1){				
				RT.localScale = new Vector3(1, 1, 1);
				CardManager.Ins.BM.SetInfoText();
				isAnimationStart = false;
			}																
		}
	}


}
