using UnityEngine;
using System.Collections;

public class CardInfoAnimation : MonoBehaviour {

	public RectTransform RT;
	private bool isAnimationStart = false;
	private bool isCIwidthOver = false;

	private float Timer;
	private bool TimerOn = false;


	public void start(){

		TimerOn  = true;
		Timer = Time.time;
		isCIwidthOver = false;

		RT.localScale = new Vector3(0, 0.1f, 1);
	}

	public void end(){
		RT.localScale = new Vector3(1, 1, 1);
		isAnimationStart = false;
		TimerOn = false;
	}


	void Update () {
		//Timer
		if(TimerOn){
			if(Time.time - Timer > 0.4f){
				isAnimationStart = true;
			}
		}

		//Animation
		if(isAnimationStart){
			if(!isCIwidthOver){
				RT.localScale += new Vector3(0.1f, 0, 0);

				if(RT.localScale.x >= 1){				
					RT.localScale = new Vector3(1, 0.06f, 1);
					isCIwidthOver = true;
				}

			}else{
				RT.localScale += new Vector3(0, 0.04f, 0);

				if(RT.localScale.y >= 1){			
					end();
				}
			}
													
		}
	}


}
