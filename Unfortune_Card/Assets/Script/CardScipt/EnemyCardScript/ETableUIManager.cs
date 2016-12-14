using UnityEngine;
using System.Collections;

public class ETableUIManager : MonoBehaviour {

	private bool isHold = false;
	private float startPositionDis;
	private float startTime;

	//move
	private float startETablePosition;
	private float speed =1;

	void Start(){
		GameManager.Instance.TE.TEDObjectHL += DropETableUI;
	}

	void Update(){
		if(!isHold){			
			ETable.Ins.transform.localPosition += new Vector3(speed*Time.deltaTime, 0, 0);

			if(speed<1 && speed >-1){
				speed = 0;
			}else{
				if(speed >0){
					speed--;
				}else{
					speed++;
				}
			}

			if(ETable.Ins.transform.localPosition.x < -5.7f){
				float resistance = 10f -(5.7f +ETable.Ins.transform.localPosition.x)*10f;
				ETable.Ins.transform.localPosition += new Vector3(resistance*Time.deltaTime, 0, 0);
			}							

		}			
	}


	public void DropETableUI(Transform target){

		if(target == transform){			
			Vector3 tempVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if(!isHold){
				startPositionDis = tempVec.x - ETable.Ins.transform.localPosition.x;
				startTime = Time.time;
				startETablePosition = ETable.Ins.transform.localPosition.x;

				isHold = true;
			}

			ETable.Ins.transform.localPosition =new Vector3( tempVec.x -startPositionDis, ETable.Ins.transform.localPosition.y, 0);



		}else if(target == null){
			if(isHold){
				speed = (ETable.Ins.transform.localPosition.x - startETablePosition)/(Time.time - startTime);
				isHold = false;
			}

		}

	}
}
